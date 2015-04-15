/*
The MIT License(MIT)
Copyright(c) 2015 Freddy Juhel
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using ManualPaperBoy.Properties;
using System.Net;
using System.Collections.Generic;
using System.IO;

namespace ManualPaperBoy
{
  public partial class FormMain : Form
  {
    public FormMain()
    {
      InitializeComponent();
    }

    private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AboutBoxApplication aboutBoxApplication = new AboutBoxApplication();
      aboutBoxApplication.ShowDialog();
    }

    private void DisplayTitle()
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
      Text += string.Format(" V{0}.{1}.{2}.{3}", fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart, fvi.FilePrivatePart);
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
      DisplayTitle();
      GetWindowValue();
      comboBoxSelectEdition.Items.Clear();
      LoadComboBox(comboBoxSelectEdition);
    }

    private static void LoadComboBox(ComboBox cb)
    {
      cb.Items.Clear();
      cb.Items.Add("Direct Matin Edition Nationale");
      cb.Items.Add("Direct Matin Bordeaux");
      cb.Items.Add("Direct Matin Lille");
      cb.Items.Add("Direct Matin Lyon");
      cb.Items.Add("Direct Matin Provence");
      cb.Items.Add("Direct Matin Montpellier");
      cb.Items.Add("Direct Matin Grand ouest");
      cb.Items.Add("Direct Matin Côte-d'azur");
      cb.Items.Add("Direct Matin Strasbourg");
      cb.Items.Add("Direct Matin Toulouse");
      cb.SelectedIndex = 0;
    }

    private void GetWindowValue()
    {
      Width = Settings.Default.WindowWidth;
      Height = Settings.Default.WindowHeight;
      Top = Settings.Default.WindowTop < 0 ? 0 : Settings.Default.WindowTop;
      Left = Settings.Default.WindowLeft < 0 ? 0 : Settings.Default.WindowLeft;
    }

    private void SaveWindowValue()
    {
      Settings.Default.WindowHeight = Height;
      Settings.Default.WindowWidth = Width;
      Settings.Default.WindowLeft = Left;
      Settings.Default.WindowTop = Top;
      Settings.Default.Save();
    }

    private void FormMainFormClosing(object sender, FormClosingEventArgs e)
    {
      SaveWindowValue();
    }

    private void buttonSelectEdition_Click(object sender, EventArgs e)
    {
      if (!listBoxSelectedEdition.Items.Contains(comboBoxSelectEdition.SelectedItem))
      {
        listBoxSelectedEdition.Items.Add(comboBoxSelectEdition.SelectedItem);
      }

      if (comboBoxSelectEdition.SelectedIndex < comboBoxSelectEdition.Items.Count - 1)
      {
        comboBoxSelectEdition.SelectedIndex = comboBoxSelectEdition.SelectedIndex + 1;
      }

    }

    private void buttonPickDirectory_Click(object sender, EventArgs e)
    {
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        textBoxSaveFilePath.Text = folderBrowserDialog1.SelectedPath;
      }
    }

    private void buttonDownloadEditions_Click(object sender, EventArgs e)
    {
      if (textBoxSaveFilePath.Text == string.Empty)
      {
        DisplayMessageOk("The save file path cannot be empty", "Empty field", MessageBoxButtons.OK);
        return;
      }

      if (listBoxSelectedEdition.Items.Count == 0)
      {
        DisplayMessageOk("You have not selected any edition", "Empty selection", MessageBoxButtons.OK);
        return;
      }

      if (!Directory.Exists(textBoxSaveFilePath.Text))
      {
        DisplayMessageOk("The directory doesn't exist", "Directory not correct", MessageBoxButtons.OK);
        return;
      }

      DateTime selectedDateTime = dateTimePicker1.Value;
      foreach (string selectedEditionInLb in listBoxSelectedEdition.Items)
      {
        string result = string.Empty;
        // http://kiosque.directmatin.fr/Pdf.aspx?edition=NEP&date=
        string url = "http://kiosque.directmatin.fr/Pdf.aspx?edition=";
        string dateEnglish = GetEnglishDate(dateTimePicker1.Value);

        string fileName = "DirectMatin-" +
          GetEditionName(selectedEditionInLb).Replace("Direct Matin ", "") +
          "-" +
          dateEnglish + ".pdf";
        url = AddEditionToUrl(url, GetEditionCode(selectedEditionInLb));
        url = AddDateToUrl(url, dateTimePicker1.Value);
        if (GetWebClientBinaries(url, Path.Combine(textBoxSaveFilePath.Text, fileName)))
        {
          result = "download ok and file saved";
        }
        else
        {
          result = "error while downloading";
        }

        //DisplayMessageOk(result, "result", MessageBoxButtons.OK);
      }
    }

    private static string AddEditionToUrl(string url, string edition)
    {
      return url += edition;
    }

    private static string AddDateToUrl(string url, DateTime dt)
    {
      return url += "&date=" + GetEnglishDate(dt);
    }

    private static string GetEnglishDate(DateTime dt)
    {
      return dt.Year + ToTwoDigits(dt.Month) + ToTwoDigits(dt.Day);
    }

    private static string GetEditionCode(string editionName)
    {
      string result = string.Empty;
      foreach (var item in LoadEditioncodes())
      {
        if (item.Key == editionName)
        {
          result = item.Value;
          break;
        }
      }

      return result;
    }

    private static string GetEditionName(string editionCode)
    {
      string result = string.Empty;
      foreach (var item in LoadEditioncodes())
      {
        if (item.Value == editionCode)
        {
          result = item.Key;
          break;
        }
      }

      return result;
    }

    private static Dictionary<string, string> LoadEditioncodes()
    {
      // TODO could be an XML file
      var result = new Dictionary<string, string>
      {
        {"Direct Matin Edition Nationale", "NEP"},
        {"Direct Matin Bordeaux", "BDX"},
        {"Direct Matin Lille", "LIL"},
        {"Direct Matin Lyon", "LYO"},
        {"Direct Matin Provence", "PRO"},
        {"Direct Matin Montpellier", "MTP"},
        {"Direct Matin Grand ouest", "VP1"},
        {"Direct Matin Côte-d'azur", "NP"},
        {"Direct Matin Strasbourg", "SP"},
        {"Direct Matin Toulouse", "TP"}
      };

      return result;
    }

    private void DisplayMessageOk(string message, string title, MessageBoxButtons buttons)
    {
      MessageBox.Show(this, message, title, buttons);
    }

    private static string ToTwoDigits(int number)
    {
      return number < 10 ? "0" + number : number.ToString();
    }

    private static bool GetWebClientBinaries(string url = "http://www.google.com/",
      string fileName = "untitled-file.pdf")
    {
      WebClient client = new WebClient();
      bool result = false;
      // set the user agent to IE6
      client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)");
      try
      {
        client.DownloadFile(url, fileName);
        result = true;
      }
      catch (WebException)
      {
        result = false;
      }
      catch (NotSupportedException)
      {
        result = false;
      }

      return result;
    }
  }
}