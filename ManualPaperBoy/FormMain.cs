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

    private void LoadComboBox(ComboBox cb)
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

      //DateTime selectedDateTime = dateTimePicker1.MaxDate;

    }

    private void DisplayMessageOk(string message, string title, MessageBoxButtons buttons)
    {
      MessageBox.Show(this, message, title, buttons);
    }
  }
}