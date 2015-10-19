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
using System.Threading;
using System.Xml.Linq;
using System.Linq;
using Tools;

namespace ManualPaperBoy
{
  public partial class FormMain : Form
  {
    public FormMain()
    {
      InitializeComponent();
    }

    readonly Dictionary<string, string> _languageDicoEn = new Dictionary<string, string>();
    readonly Dictionary<string, string> _languageDicoFr = new Dictionary<string, string>();
    private bool _editionDuringWeekEnd;
    private string _currentLanguage;

    private static string Space(int number = 1)
    {
      string result = string.Empty;
      for (int i = 0; i < number; i++)
      {
        result += " ";
      }

      return result;
    }

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
      LoadNewspaper(comboBoxSelectNewspaper);
      SetSingleDateOutOfWeekEnd();
      dateTimePickerFromDate.MinDate = new DateTime(2013, 1, 1); // first edition
      dateTimePickerFromDate.MaxDate = DateTime.Today;
      dateTimePickerEndDate.MinDate = new DateTime(2013, 1, 2);
      dateTimePickerEndDate.MaxDate = DateTime.Today;
      LoadLanguages();
      SetLanguage(Settings.Default.LastLanguageUsed);
    }

    private static void LoadNewspaper(ComboBox cb)
    {
      // TODO should be an XML file
      cb.Items.Clear();
      cb.Items.Add("Direct Matin");
      cb.Items.Add("20 minutes");
      cb.SelectedIndex = 0;
    }

    private static void LoadComboBox(ComboBox cb)
    {
      // TODO should be an XML file
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
      _currentLanguage = Settings.Default.LastLanguageUsed;
      radioButtoSingleDate.Checked = Settings.Default.radioButtoSingleDate;
      radioButtonSeveralDates.Checked = !Settings.Default.radioButtoSingleDate;
    }

    private void SaveWindowValue()
    {
      Settings.Default.WindowHeight = Height;
      Settings.Default.WindowWidth = Width;
      Settings.Default.WindowLeft = Left;
      Settings.Default.WindowTop = Top;
      Settings.Default.TextSaveFilePath = textBoxSaveFilePath.Text;
      Settings.Default.LastLanguageUsed = _currentLanguage;
      Settings.Default.radioButtoSingleDate = radioButtoSingleDate.Checked;
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

      UpdateButtons();
    }

    private void buttonPickDirectory_Click(object sender, EventArgs e)
    {
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        textBoxSaveFilePath.Text = folderBrowserDialog1.SelectedPath;
      }
    }

    private static bool OutsideWeekEnd(DateTime dt)
    {
      return (dt.DayOfWeek != DayOfWeek.Sunday) && (dt.DayOfWeek != DayOfWeek.Saturday);
    }

    private static bool IsWeekEnd(DateTime dt)
    {
      return !OutsideWeekEnd(dt);
    }

    private static bool OutsideWeekEnd()
    {
      return (DateTime.Now.DayOfWeek != DayOfWeek.Sunday) && (DateTime.Now.DayOfWeek != DayOfWeek.Saturday);
    }

    private static bool IsWeekEnd()
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
        DisplayMessage(Translate("The save file path cannot be empty"),
          Translate("Empty field"), MessageBoxButtons.OK);
        return;
      }

      if (listBoxSelectedEdition.Items.Count == 0)
      {
        DisplayMessage(Translate("You have not selected any edition"),
          Translate("Empty selection"), MessageBoxButtons.OK);
        return;
      }

      if (!Directory.Exists(textBoxSaveFilePath.Text))
      {
        DisplayMessage(Translate("The directory doesn't exist"),
          Translate("Directory not correct"), MessageBoxButtons.OK);
        return;
      }

      if (radioButtonSeveralDates.Checked && dateTimePickerFromDate.Value > dateTimePickerEndDate.Value)
      {
        DisplayMessage(Translate("The end date is earlier than the start date"),
          Translate("End date too early"), MessageBoxButtons.OK);
        return;
      }

      // test if today is a weekend, if so, move to the last Friday if no weekend print
      if (!_editionDuringWeekEnd)
      {
        if (IsWeekEnd(dateTimePickerSelectDate.Value))
        {
          SetSingleDateOutOfWeekEnd();
        }
      }

      // TODO should display waiting window
      //FormWait formWait = new FormWait();
      //formWait.ShowDialog();
      bool fileDeleted = false;
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
          else if (_editionDuringWeekEnd)
          {
            listOfDates.Add(selectedDate);
          }

          selectedDate = selectedDate.Add(new TimeSpan(1, 0, 0, 0));
        } while (selectedDate < dateTimePickerEndDate.Value);
      }

      string result = string.Empty;
      if (listOfDates.Count == 0)
      {
        DisplayMessage(Translate("There is no selected day in the period"),
          Translate("No selection"), MessageBoxButtons.OK);
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
          result = GetWebClientBinaries(url, Path.Combine(textBoxSaveFilePath.Text, fileName)) ? Translate("download ok and file saved") : Translate("error while downloading");
          Application.DoEvents();
          Thread.Sleep(5000);
          long fileSize = FileGetSize(Path.Combine(textBoxSaveFilePath.Text, fileName));
          if (fileSize == 0)
          {
            File.Delete(Path.Combine(textBoxSaveFilePath.Text, fileName));
            fileDeleted = true;
          }
          numberOfdownloadedFile++;
          Application.DoEvents();
        }
      }

      //formWait.Close();
      if (fileDeleted)
      {
        DisplayMessage(Translate("The download file has a size of zero byte so it has been deleted"),
          Translate("File deleted"), MessageBoxButtons.OK);
      }
      else
      {
        string tmpMsg0 = Translate("The") + FrenchPlural(numberOfdownloadedFile, _currentLanguage );
        string tmpMsg1 = Translate("download") + Plural(numberOfdownloadedFile, Translate("download"));
        const string tmpMsg2 = Punctuation.OneSpace;
        string tmpMsg3 = Plural(numberOfdownloadedFile, Translate("is"));
        string tmpMsg4 = Translate("done") + FrenchPlural(numberOfdownloadedFile, _currentLanguage);
        string message = string.Format("{0}{2}{1}{2}{3}{2}{4}", tmpMsg0, tmpMsg1, tmpMsg2, tmpMsg3, tmpMsg4);
        DisplayMessage(message, Translate("Download is over"), MessageBoxButtons.OK);
      }
    }

    private static string FrenchPlural(int number, string currentLanguage = "english")
    {
      return (number > 1 && currentLanguage.ToLower() == "french") ? "s" : string.Empty;
    }

    private static long FileGetSize(string filePath)
    {
      try
      {
        return File.Exists(filePath) ? new FileInfo(filePath).Length : 0;
      }
      catch (Exception)
      {
        return -1;
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
      // TODO could be an XML file or property settings
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

    private void DisplayMessage(string message, string title, MessageBoxButtons buttons)
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
        DisplayMessage(Translate("There is no element in the list") + Punctuation.Period + 
          Punctuation.CrLf +
          Translate("Please select an edition first") + Punctuation.Period,
          Translate("No element to choose from"), MessageBoxButtons.OK);
        return;
      }

      if (listBoxSelectedEdition.SelectedIndex == -1)
      {
        DisplayMessage(Translate("No edition has been selected") + Punctuation.Period,
          Translate("No selection"), MessageBoxButtons.OK);
        return;
      }

      listBoxSelectedEdition.Items.Remove(listBoxSelectedEdition.SelectedItem);
      if (listBoxSelectedEdition.Items.Count != 0)
      {
        listBoxSelectedEdition.SelectedIndex = 0;
      }

      UpdateButtons();
    }

    private void buttonLaunchSelectedEdition_Click(object sender, EventArgs e)
    {
      if (listBoxSelectedEdition.Items.Count == 0)
      {
        DisplayMessage(Translate("There is no element in the list") + Punctuation.Period + 
          Punctuation.CrLf +
          Translate("Please select an edition first"), Translate("No element to choose from"),
          MessageBoxButtons.OK);
        return;
      }

      if (listBoxSelectedEdition.SelectedIndex == -1 && listBoxSelectedEdition.Items.Count == 1)
      {
        listBoxSelectedEdition.SelectedIndex = 0;
      }

      if (listBoxSelectedEdition.SelectedIndex == -1)
      {
        DisplayMessage(Translate("No edition has been selected") + Punctuation.Period,
          Translate("No selection"), MessageBoxButtons.OK);
        return;
      }

      try
      {
        string dateEnglish = GetEnglishDate(dateTimePickerSelectDate.Value);
        string fileName = GetEditionFileName(listBoxSelectedEdition.SelectedItem.ToString(), dateEnglish);
        if (Directory.Exists(textBoxSaveFilePath.Text))
        {
          if (File.Exists(Path.Combine(textBoxSaveFilePath.Text, fileName)))
          {
            Process.Start(Path.Combine(textBoxSaveFilePath.Text, fileName));
          }
          else
          {
            DisplayMessage(Translate("The file") + Space() + Path.Combine(textBoxSaveFilePath.Text, fileName) +
              Space() + Translate("doesn't exist"), Translate("No file"), MessageBoxButtons.OK);
          }
        }
        else
        {
          DisplayMessage(Translate("The directory") + Space() + textBoxSaveFilePath.Text +
              Space() + Translate("doesn't exist"), Translate("No directory"), MessageBoxButtons.OK);
        }
      }
      catch (Exception exception)
      {
        DisplayMessage(Translate("There was an error while trying to open the selected edition") +
          exception.Message,
          Translate("Error"), MessageBoxButtons.OK);
      }
    }

    private void checkBoxEditionDuringWeekEnd_CheckedChanged(object sender, EventArgs e)
    {
      _editionDuringWeekEnd = checkBoxEditionDuringWeekEnd.Checked;
    }

    private static string Plural(int number, string irregularNoun = "")
    {
      switch (irregularNoun)
      {
        case "":
          return number > 1 ? "s" : string.Empty;
        case "est":
          return number > 1 ? "sont" : "est";
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
        case " is":
          return number > 1 ? "s are" : " is"; // with a space before
        case "is":
          return number > 1 ? "are" : "is"; // without a space before
        case "The":
          return "The"; // CAPITAL usefull for french plural which is different le les
        case "the":
          return "the"; // lower case usefull for french plural which is different le les
        default:
          return number > 1 ? "s" : string.Empty;
      }
    }

    private void cutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CutToClipboard(textBoxSaveFilePath, Translate("no text"));
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CopytToClipboard(textBoxSaveFilePath, Translate("no text"));
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
        DisplayMessage(Translate("There is nothing to copy"), message, MessageBoxButtons.OK);
        return;
      }

      Clipboard.SetText(tb.SelectedText);
    }

    private void CutToClipboard(TextBoxBase tb, string errorMessage = "Nothing")
    {
      if (tb != ActiveControl) return;
      if (tb.Text == string.Empty)
      {
        DisplayMessage(Translate("There is") + Punctuation.OneSpace + errorMessage + 
          Punctuation.OneSpace + Translate("to cut") + Punctuation.OneSpace, 
          errorMessage, MessageBoxButtons.OK);
        return;
      }

      if (tb.SelectedText == string.Empty)
      {
        DisplayMessage(Translate("No text has been selected"), errorMessage, MessageBoxButtons.OK);
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
      if (Directory.Exists(textBoxSaveFilePath.Text))
      {
        Process.Start(textBoxSaveFilePath.Text);
      }
    }

    private enum Language
    {
      French,
      English
    }

    private void LoadLanguages()
    {
      if (!File.Exists(Settings.Default.LanguageFileName))
      {
        CreateLanguageFile();
      }

      // read the translation file and feed the language
      XDocument xDoc;
      try
      {
        xDoc = XDocument.Load(Settings.Default.LanguageFileName);
      }
      catch (Exception exception)
      {
        MessageBox.Show("Error while loading xml file " + exception.Message);
        CreateLanguageFile();
        return;
      }
      var result = from node in xDoc.Descendants("term")
                   where node.HasElements
                   let xElementName = node.Element("name")
                   where xElementName != null
                   let xElementEnglish = node.Element("englishValue")
                   where xElementEnglish != null
                   let xElementFrench = node.Element("frenchValue")
                   where xElementFrench != null
                   select new
                   {
                     name = xElementName.Value,
                     englishValue = xElementEnglish.Value,
                     frenchValue = xElementFrench.Value
                   };
      foreach (var i in result)
      {
        //_languageDicoEn.Add(i.name, i.englishValue); // keep to search this line in all my previous projects
        if (!_languageDicoEn.ContainsKey(i.name))
        {
          _languageDicoEn.Add(i.name, i.englishValue);
        }
        else
        {
          MessageBox.Show("Your xml file has duplicate like: " + i.name);
        }

        if (!_languageDicoFr.ContainsKey(i.name))
        {
          _languageDicoFr.Add(i.name, i.frenchValue);
        }
        else
        {
          MessageBox.Show("Your xml file has duplicate like: " + i.name);
        }
      }
    }

    private static void CreateLanguageFile()
    {
      List<string> minimumVersion = new List<string>
      {
        "<?xml version=\"1.0\" encoding=\"utf-8\" ?>",
        "<terms>",
         "<term>",
        "<name>MenuFile</name>",
        "<englishValue>File</englishValue>",
        "<frenchValue>Fichier</frenchValue>",
        "</term>",
        "  </terms>"
      };
      StreamWriter sw = new StreamWriter(Settings.Default.LanguageFileName);
      foreach (string item in minimumVersion)
      {
        sw.WriteLine(item);
      }

      sw.Close();
    }

    private void SetLanguage(string myLanguage)
    {
      switch (myLanguage.ToLower())
      {
        case "english":
          frenchToolStripMenuItem.Checked = false;
          englishToolStripMenuItem.Checked = true;
          fileToolStripMenuItem.Text = _languageDicoEn["MenuFile"];
          newToolStripMenuItem.Text = _languageDicoEn["MenuFileNew"];
          openToolStripMenuItem.Text = _languageDicoEn["MenuFileOpen"];
          saveToolStripMenuItem.Text = _languageDicoEn["MenuFileSave"];
          saveasToolStripMenuItem.Text = _languageDicoEn["MenuFileSaveAs"];
          printPreviewToolStripMenuItem.Text = _languageDicoEn["MenuFilePrint"];
          printPreviewToolStripMenuItem.Text = _languageDicoEn["MenufilePageSetup"];
          quitToolStripMenuItem.Text = _languageDicoEn["MenufileQuit"];
          editToolStripMenuItem.Text = _languageDicoEn["MenuEdit"];
          cancelToolStripMenuItem.Text = _languageDicoEn["MenuEditCancel"];
          redoToolStripMenuItem.Text = _languageDicoEn["MenuEditRedo"];
          cutToolStripMenuItem.Text = _languageDicoEn["MenuEditCut"];
          copyToolStripMenuItem.Text = _languageDicoEn["MenuEditCopy"];
          pasteToolStripMenuItem.Text = _languageDicoEn["MenuEditPaste"];
          selectAllToolStripMenuItem.Text = _languageDicoEn["MenuEditSelectAll"];
          toolsToolStripMenuItem.Text = _languageDicoEn["MenuTools"];
          personalizeToolStripMenuItem.Text = _languageDicoEn["MenuToolsCustomize"];
          optionsToolStripMenuItem.Text = _languageDicoEn["MenuToolsOptions"];
          languagetoolStripMenuItem.Text = _languageDicoEn["MenuLanguage"];
          englishToolStripMenuItem.Text = _languageDicoEn["MenuLanguageEnglish"];
          frenchToolStripMenuItem.Text = _languageDicoEn["MenuLanguageFrench"];
          helpToolStripMenuItem.Text = _languageDicoEn["MenuHelp"];
          summaryToolStripMenuItem.Text = _languageDicoEn["MenuHelpSummary"];
          indexToolStripMenuItem.Text = _languageDicoEn["MenuHelpIndex"];
          searchToolStripMenuItem.Text = _languageDicoEn["MenuHelpSearch"];
          aboutToolStripMenuItem.Text = _languageDicoEn["MenuHelpAbout"];
          labelSaveFilePath.Text = _languageDicoEn["Save File Path:"];
          labelSelectNewspaper.Text = _languageDicoEn["Select Newspaper:"];
          labelSelectEdition.Text = _languageDicoEn["Select Edition:"];
          buttonSelectEdition.Text = _languageDicoEn["Select ->"];
          labelSelectDate.Text = _languageDicoEn["Select date:"];
          buttonRemove.Text = _languageDicoEn["Remove"];
          checkBoxEditionDuringWeekEnd.Text = _languageDicoEn["Edition during the weekend"];
          groupBoxMultiSelectionDate.Text = _languageDicoEn["Time period"];
          radioButtoSingleDate.Text = _languageDicoEn["Single selected date"];
          radioButtonSeveralDates.Text = _languageDicoEn["Several dates"];
          labelFromDate.Text = _languageDicoEn["Start date:"];
          labelEndDate.Text = _languageDicoEn["End date:"];
          buttonDownloadEditions.Text = _languageDicoEn["Download selected editions"];
          buttonLaunchSelectedEdition.Text = _languageDicoEn["Launch selected editions"];
          buttonLaunchTargetDirectory.Text = _languageDicoEn["Launch target directory"];
          _currentLanguage = "english";
          break;
        case "french":
          frenchToolStripMenuItem.Checked = true;
          englishToolStripMenuItem.Checked = false;
          fileToolStripMenuItem.Text = _languageDicoFr["MenuFile"];
          newToolStripMenuItem.Text = _languageDicoFr["MenuFileNew"];
          openToolStripMenuItem.Text = _languageDicoFr["MenuFileOpen"];
          saveToolStripMenuItem.Text = _languageDicoFr["MenuFileSave"];
          saveasToolStripMenuItem.Text = _languageDicoFr["MenuFileSaveAs"];
          printPreviewToolStripMenuItem.Text = _languageDicoFr["MenuFilePrint"];
          printPreviewToolStripMenuItem.Text = _languageDicoFr["MenufilePageSetup"];
          quitToolStripMenuItem.Text = _languageDicoFr["MenufileQuit"];
          editToolStripMenuItem.Text = _languageDicoFr["MenuEdit"];
          cancelToolStripMenuItem.Text = _languageDicoFr["MenuEditCancel"];
          redoToolStripMenuItem.Text = _languageDicoFr["MenuEditRedo"];
          cutToolStripMenuItem.Text = _languageDicoFr["MenuEditCut"];
          copyToolStripMenuItem.Text = _languageDicoFr["MenuEditCopy"];
          pasteToolStripMenuItem.Text = _languageDicoFr["MenuEditPaste"];
          selectAllToolStripMenuItem.Text = _languageDicoFr["MenuEditSelectAll"];
          toolsToolStripMenuItem.Text = _languageDicoFr["MenuTools"];
          personalizeToolStripMenuItem.Text = _languageDicoFr["MenuToolsCustomize"];
          optionsToolStripMenuItem.Text = _languageDicoFr["MenuToolsOptions"];
          languagetoolStripMenuItem.Text = _languageDicoFr["MenuLanguage"];
          englishToolStripMenuItem.Text = _languageDicoFr["MenuLanguageEnglish"];
          frenchToolStripMenuItem.Text = _languageDicoFr["MenuLanguageFrench"];
          helpToolStripMenuItem.Text = _languageDicoFr["MenuHelp"];
          summaryToolStripMenuItem.Text = _languageDicoFr["MenuHelpSummary"];
          indexToolStripMenuItem.Text = _languageDicoFr["MenuHelpIndex"];
          searchToolStripMenuItem.Text = _languageDicoFr["MenuHelpSearch"];
          aboutToolStripMenuItem.Text = _languageDicoFr["MenuHelpAbout"];
          labelSaveFilePath.Text = _languageDicoFr["Save File Path:"];
          labelSelectNewspaper.Text = _languageDicoFr["Select Newspaper:"];
          labelSelectEdition.Text = _languageDicoFr["Select Edition:"];
          buttonSelectEdition.Text = _languageDicoFr["Select ->"];
          labelSelectDate.Text = _languageDicoFr["Select date:"];
          buttonRemove.Text = _languageDicoFr["Remove"];
          checkBoxEditionDuringWeekEnd.Text = _languageDicoFr["Edition during the weekend"];
          groupBoxMultiSelectionDate.Text = _languageDicoFr["Time period"];
          radioButtoSingleDate.Text = _languageDicoFr["Single selected date"];
          radioButtonSeveralDates.Text = _languageDicoFr["Several dates"];
          labelFromDate.Text = _languageDicoFr["Start date:"];
          labelEndDate.Text = _languageDicoFr["End date:"];
          buttonDownloadEditions.Text = _languageDicoFr["Download selected editions"];
          buttonLaunchSelectedEdition.Text = _languageDicoFr["Launch selected editions"];
          buttonLaunchTargetDirectory.Text = _languageDicoFr["Launch target directory"];
          _currentLanguage = "french";
          break;
      }
    }

    private void frenchToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      SetLanguage(Language.French.ToString());
    }

    private void englishToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      SetLanguage(Language.English.ToString());
    }

    private void listBoxSelectedEdition_SizeChanged(object sender, EventArgs e)
    {
      UpdateButtons();
    }

    private void UpdateButtons()
    {
      buttonLaunchSelectedEdition.Enabled = listBoxSelectedEdition.Items.Count != 0;
    }

    private void listBoxSelectedEdition_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateButtons();
    }

    private string Translate(string index)
    {
      string result = string.Empty;
      string language = frenchToolStripMenuItem.Checked ? "french" : "english";

      switch (language.ToLower())
      {
        case "english":
          result = _languageDicoEn.ContainsKey(index) ? _languageDicoEn[index] :
           Translate("the term") + Punctuation.Colon + Punctuation.DoubleQuote + index +
           Punctuation.DoubleQuote + Punctuation.OneSpace + 
           Translate("has not been translated yet") + Punctuation.Period + Punctuation.CrLf +
           Translate("Please add an entry in the translation XML file or tell the developer to translate this term");
          break;
        case "french":
          result = _languageDicoFr.ContainsKey(index) ? _languageDicoFr[index] :
            Translate("the term") + Punctuation.Colon + Punctuation.DoubleQuote + index +
           Punctuation.DoubleQuote +
            Punctuation.OneSpace + Translate("has not been translated yet") + Punctuation.Period + 
            Punctuation.CrLf +
           Translate("Please add an entry in the translation XML file or tell the developer to translate this term");
          break;
      }

      return result;
    }
  }
}