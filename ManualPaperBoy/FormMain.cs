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

    private bool editionDuringWeekEnd;

    private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SaveWindowValue();
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
      SetSingleDateOutOfWeekEnd();
      dateTimePickerFromDate.MinDate = new DateTime(2013, 1, 1); // first edition
      dateTimePickerFromDate.MaxDate = DateTime.Today;
      dateTimePickerEndDate.MinDate = new DateTime(2013, 1, 2);
      dateTimePickerEndDate.MaxDate = DateTime.Today;
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
      textBoxSaveFilePath.Text = Settings.Default.TextSaveFilePath;
    }

    private void SaveWindowValue()
    {
      Settings.Default.WindowHeight = Height;
      Settings.Default.WindowWidth = Width;
      Settings.Default.WindowLeft = Left;
      Settings.Default.WindowTop = Top;
      Settings.Default.Save();
      Settings.Default.TextSaveFilePath = textBoxSaveFilePath.Text;
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

    public static bool OutsideWeekEnd(DateTime dt)
    {
      return (dt.DayOfWeek != DayOfWeek.Sunday) && (dt.DayOfWeek != DayOfWeek.Saturday);
    }

    public static bool IsWeekEnd(DateTime dt)
    {
      return !OutsideWeekEnd(dt);
    }

    public static bool OutsideWeekEnd()
    {
      return (DateTime.Now.DayOfWeek != DayOfWeek.Sunday) && (DateTime.Now.DayOfWeek != DayOfWeek.Saturday);
    }

    public static bool IsWeekEnd()
    {
      return !OutsideWeekEnd();
    }

    private void SetSingleDateOutOfWeekEnd()
    {
      if (!IsWeekEnd()) return;
      dateTimePickerSelectDate.Value = dateTimePickerSelectDate.Value.Add(dateTimePickerSelectDate.Value.DayOfWeek == DayOfWeek.Saturday ? new TimeSpan(-1, 0, 0, 0) : new TimeSpan(-2, 0, 0, 0));
    }

    private static string GetEditionFileName(string selectedEditionInListBox, string dateEnglish)
    {
      // Remove Windows forbidden characters
      string result = selectedEditionInListBox.Replace(" ", "_").Replace("'", "_") + "-" + dateEnglish + ".pdf";
      char[] forbiddenWindowsFilenameCharacters = { '\\', '/', ':', '*', '?', '\"', '<', '>', '|' };
      // return forbiddenWindowsFilenameCharacters.Aggregate(result, (current, item) => current.Replace(item, '_'));
      foreach (char item in forbiddenWindowsFilenameCharacters)
      {
        result = result.Replace(item, '_');
      }

      return result;
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

      if (radioButtonSeveralDates.Checked && dateTimePickerFromDate.Value > dateTimePickerEndDate.Value)
      {
        DisplayMessageOk("The end date is earlier than the start date", "End date too early", MessageBoxButtons.OK);
        return;
      }

      // test if today is a weekend, if so move to the last Friday
      if (!editionDuringWeekEnd)
      {
        if (IsWeekEnd(dateTimePickerSelectDate.Value))
        {
          SetSingleDateOutOfWeekEnd();
        }
      }

      //FormWait formWait = new FormWait();
      //formWait.ShowDialog();
      int numberOfdownloadedFile = 0;
      List<DateTime> listOfDates = new List<DateTime>();
      if (radioButtoSingleDate.Checked)
      {
        listOfDates.Add(dateTimePickerSelectDate.Value);
      }
      else
      {
        DateTime selectedDate = dateTimePickerFromDate.Value;
        do
        {
          if (!IsWeekEnd(selectedDate))
          {
            listOfDates.Add(selectedDate);
          }
          else if(editionDuringWeekEnd)  
          {
            listOfDates.Add(selectedDate);
          }

          selectedDate = selectedDate.Add(new TimeSpan(1, 0, 0, 0));
        } while (selectedDate < dateTimePickerEndDate.Value);
      }

      string result = string.Empty;
      if (listOfDates.Count == 0)
      {
        DisplayMessageOk("There is no selected day in the period", "No selection", MessageBoxButtons.OK);
        return;
      }

      foreach (DateTime dateTimeInRange in listOfDates)
      {
        foreach (string selectedEditionInListBox in listBoxSelectedEdition.Items)
        {
          // http://kiosque.directmatin.fr/Pdf.aspx?edition=NEP&date=20150416
          string url = "http://kiosque.directmatin.fr/Pdf.aspx?edition=";
          string dateEnglish = GetEnglishDate(dateTimeInRange);

          string fileName = GetEditionFileName(selectedEditionInListBox, dateEnglish);
          url = AddEditionToUrl(url, GetEditionCode(selectedEditionInListBox));
          url = AddDateToUrl(url, dateTimeInRange);
          result = GetWebClientBinaries(url, Path.Combine(textBoxSaveFilePath.Text, fileName)) ? "download ok and file saved" : "error while downloading";
          // TODO remove file if length is zero.
          numberOfdownloadedFile++;
        }
      }

      //formWait.Close();
      DisplayMessageOk("The download" + Plural(numberOfdownloadedFile) + " " +
        Plural(numberOfdownloadedFile, "is") + " done", "Download is over", MessageBoxButtons.OK);
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

    private void buttonRemove_Click(object sender, EventArgs e)
    {
      if (listBoxSelectedEdition.Items.Count == 0)
      {
        DisplayMessageOk("There is no element in the list.\nPlease select an edition first.", "No element to choose from", MessageBoxButtons.OK);
        return;
      }

      if (listBoxSelectedEdition.SelectedIndex == -1)
      {
        DisplayMessageOk("No edition has been selected.", "No selection", MessageBoxButtons.OK);
        return;
      }

      listBoxSelectedEdition.Items.Remove(listBoxSelectedEdition.SelectedItem);
      if (listBoxSelectedEdition.Items.Count != 0)
      {
        listBoxSelectedEdition.SelectedIndex = 0;
      }
    }

    private void buttonLaunchSelectedEdition_Click(object sender, EventArgs e)
    {
      if (listBoxSelectedEdition.Items.Count == 0)
      {
        DisplayMessageOk("There is no element in the list.\nPlease select an edition first.", "No element to choose from", MessageBoxButtons.OK);
        return;
      }

      if (listBoxSelectedEdition.SelectedIndex == -1)
      {
        DisplayMessageOk("No edition has been selected.", "No selection",
          MessageBoxButtons.OK);
        return;
      }

      try
      {

      }
      catch (Exception exception)
      {
        DisplayMessageOk("There was an error while trying to open the selected edition\n" +
          exception.Message,
          "Error", MessageBoxButtons.OK);
      }

      string dateEnglish = GetEnglishDate(dateTimePickerSelectDate.Value);
      string fileName = GetEditionFileName(listBoxSelectedEdition.SelectedItem.ToString(), dateEnglish);
      Process.Start(Path.Combine(textBoxSaveFilePath.Text, fileName));
    }

    private void checkBoxEditionDuringWeekEnd_CheckedChanged(object sender, EventArgs e)
    {
      editionDuringWeekEnd = checkBoxEditionDuringWeekEnd.Checked;
    }

    public static string Plural(int number, string irregularNoun = "")
    {
      switch (irregularNoun)
      {
        case "":
          return number > 1 ? "s" : string.Empty;
        case "al":
          return number > 1 ? "aux" : "al";
        case "au":
          return number > 1 ? "aux" : "au";
        case "eau":
          return number > 1 ? "eaux" : "eau";
        case "eu":
          return number > 1 ? "eux" : "eu";
        case "landau":
          return number > 1 ? "landaus" : "landau";
        case "sarrau":
          return number > 1 ? "sarraus" : "sarrau";
        case "bleu":
          return number > 1 ? "bleus" : "bleu";
        case "émeu":
          return number > 1 ? "émeus" : "émeu";
        case "lieu":
          return number > 1 ? "lieux" : "lieu";
        case "pneu":
          return number > 1 ? "pneus" : "pneu";
        case "aval":
          return number > 1 ? "avals" : "aval";
        case "bal":
          return number > 1 ? "bals" : "bal";
        case "chacal":
          return number > 1 ? "chacals" : "chacal";
        case "carnaval":
          return number > 1 ? "carnavals" : "carnaval";
        case "festival":
          return number > 1 ? "festivals" : "festival";
        case "récital":
          return number > 1 ? "récitals" : "récital";
        case "régal":
          return number > 1 ? "régals" : "régal";
        case "cal":
          return number > 1 ? "cals" : "cal";
        case "serval":
          return number > 1 ? "servals" : "serval";
        case "choral":
          return number > 1 ? "chorals" : "choral";
        case "narval":
          return number > 1 ? "narvals" : "narval";
        case "bail":
          return number > 1 ? "baux" : "bail";
        case "corail":
          return number > 1 ? "coraux" : "corail";
        case "émail":
          return number > 1 ? "émaux" : "émail";
        case "soupirail":
          return number > 1 ? "soupiraux" : "soupirail";
        case "travail":
          return number > 1 ? "travaux" : "travail";
        case "vantail":
          return number > 1 ? "vantaux" : "vantail";
        case "vitrail":
          return number > 1 ? "vitraux" : "vitrail";
        case "bijou":
          return number > 1 ? "bijoux" : "bijou";
        case "caillou":
          return number > 1 ? "cailloux" : "caillou";
        case "chou":
          return number > 1 ? "choux" : "chou";
        case "genou":
          return number > 1 ? "genoux" : "genou";
        case "hibou":
          return number > 1 ? "hiboux" : "hibou";
        case "joujou":
          return number > 1 ? "joujoux" : "joujou";
        case "pou":
          return number > 1 ? "poux" : "pou";
          
        // English
        case "is":
          return number > 1 ? "are" : "is";

        default:
          return number > 1 ? "s" : string.Empty;
      }
    }

    private void cutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CutToClipboard(textBoxSaveFilePath, "no text");
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CopytToClipboard(textBoxSaveFilePath, "no text");
    }

    private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      PasteFromClipboard(textBoxSaveFilePath);
    }

    private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (textBoxSaveFilePath == ActiveControl)
      {
        textBoxSaveFilePath.SelectAll();
      }
    }

    private void CopytToClipboard(TextBoxBase tb, string message = "Nothing")
    {
      if (tb != ActiveControl) return;
      if (tb.Text == string.Empty)
      {
        DisplayMessageOk("There is nothing to copy ", message, MessageBoxButtons.OK);
        return;
      }

      Clipboard.SetText(tb.SelectedText);
    }

    private void CutToClipboard(TextBoxBase tb, string errorMessage = "Nothing")
    {
      if (tb != ActiveControl) return;
      if (tb.Text == string.Empty)
      {
        DisplayMessageOk("There is " + errorMessage + " to cut ", errorMessage, MessageBoxButtons.OK);
        return;
      }

      if (tb.SelectedText == string.Empty)
      {
        DisplayMessageOk("No text has been selected", errorMessage, MessageBoxButtons.OK);
        return;
      }

      Clipboard.SetText(tb.SelectedText);
      tb.SelectedText = string.Empty;
    }

    private void PasteFromClipboard(TextBoxBase tb)
    {
      if (tb != ActiveControl) return;
      var selectionIndex = tb.SelectionStart;
      tb.Text = tb.Text.Insert(selectionIndex, Clipboard.GetText());
      tb.SelectionStart = selectionIndex + Clipboard.GetText().Length;
    }

    private void buttonLaunchTargetDirectory_Click(object sender, EventArgs e)
    {
      // TODO to be implemented

    }
  }
}