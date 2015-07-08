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

namespace ManualPaperBoy
{
  public partial class FormMain : Form
  {
    public FormMain()
    {
      InitializeComponent();
    }

    readonly Dictionary<string, string> languageDicoEn = new Dictionary<string, string>();
    readonly Dictionary<string, string> languageDicoFr = new Dictionary<string, string>();

    private bool editionDuringWeekEnd;
    private const string OneSpace = " ";

    private string Space(int number = 1)
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
      cb.Items.Clear();
      cb.Items.Add("Direct Matin");
      cb.Items.Add("20 minutes");
      cb.SelectedIndex = 0;
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
      Settings.Default.TextSaveFilePath = textBoxSaveFilePath.Text;
      Settings.Default.LastLanguageUsed = frenchToolStripMenuItem.Checked ? "French" : "English";
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
        DisplayMessageOk(GetTranslatedString("The save file path cannot be empty"),
          GetTranslatedString("Empty field"), MessageBoxButtons.OK);
        return;
      }

      if (listBoxSelectedEdition.Items.Count == 0)
      {
        DisplayMessageOk(GetTranslatedString("You have not selected any edition"), 
          GetTranslatedString("Empty selection"), MessageBoxButtons.OK);
        return;
      }

      if (!Directory.Exists(textBoxSaveFilePath.Text))
      {
        DisplayMessageOk(GetTranslatedString("The directory doesn't exist"),
          GetTranslatedString("Directory not correct"), MessageBoxButtons.OK);
        return;
      }

      if (radioButtonSeveralDates.Checked && dateTimePickerFromDate.Value > dateTimePickerEndDate.Value)
      {
        DisplayMessageOk(GetTranslatedString("The end date is earlier than the start date"),
          GetTranslatedString("End date too early"), MessageBoxButtons.OK);
        return;
      }

      // test if today is a weekend, if so, move to the last Friday if no weekend print
      if (!editionDuringWeekEnd)
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
          else if (editionDuringWeekEnd)
          {
            listOfDates.Add(selectedDate);
          }

          selectedDate = selectedDate.Add(new TimeSpan(1, 0, 0, 0));
        } while (selectedDate < dateTimePickerEndDate.Value);
      }

      string result = string.Empty;
      if (listOfDates.Count == 0)
      {
        DisplayMessageOk(GetTranslatedString("There is no selected day in the period"),
          GetTranslatedString("No selection"), MessageBoxButtons.OK);
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
          result = GetWebClientBinaries(url, Path.Combine(textBoxSaveFilePath.Text, fileName)) ? GetTranslatedString("download ok and file saved") : GetTranslatedString("error while downloading");
          Thread.Sleep(5000);
          long fileSize = FileGetSize(Path.Combine(textBoxSaveFilePath.Text, fileName));
          if (fileSize == 0)
          {
            File.Delete(Path.Combine(textBoxSaveFilePath.Text, fileName));
            fileDeleted = true;
          }
          numberOfdownloadedFile++;
        }
      }

      //formWait.Close();
      if (fileDeleted)
      {
        DisplayMessageOk(GetTranslatedString("The download file has a size of zero byte so it has been deleted"),
          GetTranslatedString("File deleted"), MessageBoxButtons.OK);
      }
      else
      {
        string tmpMsg0 = Plural(numberOfdownloadedFile, GetTranslatedString("The"));
        string tmpMsg1 = Plural(numberOfdownloadedFile, GetTranslatedString("download"));
        string tmpMsg2 = Space();
        string tmpMsg3 = Plural(numberOfdownloadedFile, GetTranslatedString("is"));
        string tmpMsg4 = GetTranslatedString("done");
        string message = string.Format("{0}{2}{1}{2}{3}{2}{4}", tmpMsg0,tmpMsg1, tmpMsg2, tmpMsg3, tmpMsg4);
        DisplayMessageOk(message, GetTranslatedString("Download is over"), MessageBoxButtons.OK);
      }
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

      UpdateButtons();
    }

    private void buttonLaunchSelectedEdition_Click(object sender, EventArgs e)
    {
      if (listBoxSelectedEdition.Items.Count == 0)
      {
        DisplayMessageOk(GetTranslatedString("There is no element in the list") + ".\n" +
          GetTranslatedString("Please select an edition first"), GetTranslatedString("No element to choose from"),
          MessageBoxButtons.OK);
        return;
      }

      if (listBoxSelectedEdition.SelectedIndex == -1 && listBoxSelectedEdition.Items.Count == 1)
      {
        listBoxSelectedEdition.SelectedIndex = 0;
      }

      if (listBoxSelectedEdition.SelectedIndex == -1)
      {
        DisplayMessageOk(GetTranslatedString("No edition has been selected") + ".", GetTranslatedString("No selection"),
          MessageBoxButtons.OK);
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
            DisplayMessageOk(GetTranslatedString("The file") + Space() + Path.Combine(textBoxSaveFilePath.Text, fileName) +
              Space() + GetTranslatedString("doesn't exist"), GetTranslatedString("No file"), MessageBoxButtons.OK);
          }
        }
        else
        {
          DisplayMessageOk(GetTranslatedString("The directory") + Space() + textBoxSaveFilePath.Text +
              Space() + GetTranslatedString("doesn't exist"), GetTranslatedString("No directory"), MessageBoxButtons.OK);
        }
      }
      catch (Exception exception)
      {
        DisplayMessageOk(GetTranslatedString("There was an error while trying to open the selected edition") +
          exception.Message,
          GetTranslatedString("Error"), MessageBoxButtons.OK);
      }
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
          return number > 1 ? "The" : "The"; // CAPITAL
        case "the":
          return number > 1 ? "the" : "the"; // lower case
        default:
          return number > 1 ? "s" : string.Empty;
      }
    }

    private void cutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CutToClipboard(textBoxSaveFilePath, GetTranslatedString("no text"));
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CopytToClipboard(textBoxSaveFilePath, GetTranslatedString("no text"));
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
      if (Directory.Exists(textBoxSaveFilePath.Text))
      {
        Process.Start(textBoxSaveFilePath.Text);
      }
    }

    public enum Language
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
      XDocument xDoc = XDocument.Load(Settings.Default.LanguageFileName);
      var result = from node in xDoc.Descendants("term")
                   where node.HasElements
                   select new
                   {
                     name = node.Element("name").Value,
                     englishValue = node.Element("englishValue").Value,
                     frenchValue = node.Element("frenchValue").Value
                   };
      foreach (var i in result)
      {
        languageDicoEn.Add(i.name, i.englishValue);
        languageDicoFr.Add(i.name, i.frenchValue);
      }
    }

    private static void CreateLanguageFile()
    {
      List<string> minimumVersion = new List<string>
      {
        "<?xml version=\"1.0\" encoding=\"utf - 8\" ?>",
        "<Document>",
        "<DocumentVersion>",
        "<version> 1.0 </version>",
        "</DocumentVersion>",
        "<terms>",
         "<term>",
        "<name>MenuFile</name>",
        "<englishValue>File</englishValue>",
        "<frenchValue>Fichier</frenchValue>",
        "</term>",
        "  </terms>",
        "</Document>"
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
      switch (myLanguage)
      {
        case "English":
          frenchToolStripMenuItem.Checked = false;
          englishToolStripMenuItem.Checked = true;
          fileToolStripMenuItem.Text = languageDicoEn["MenuFile"];
          newToolStripMenuItem.Text = languageDicoEn["MenuFileNew"];
          openToolStripMenuItem.Text = languageDicoEn["MenuFileOpen"];
          saveToolStripMenuItem.Text = languageDicoEn["MenuFileSave"];
          saveasToolStripMenuItem.Text = languageDicoEn["MenuFileSaveAs"];
          printPreviewToolStripMenuItem.Text = languageDicoEn["MenuFilePrint"];
          printPreviewToolStripMenuItem.Text = languageDicoEn["MenufilePageSetup"];
          quitToolStripMenuItem.Text = languageDicoEn["MenufileQuit"];
          editToolStripMenuItem.Text = languageDicoEn["MenuEdit"];
          cancelToolStripMenuItem.Text = languageDicoEn["MenuEditCancel"];
          redoToolStripMenuItem.Text = languageDicoEn["MenuEditRedo"];
          cutToolStripMenuItem.Text = languageDicoEn["MenuEditCut"];
          copyToolStripMenuItem.Text = languageDicoEn["MenuEditCopy"];
          pasteToolStripMenuItem.Text = languageDicoEn["MenuEditPaste"];
          selectAllToolStripMenuItem.Text = languageDicoEn["MenuEditSelectAll"];
          toolsToolStripMenuItem.Text = languageDicoEn["MenuTools"];
          personalizeToolStripMenuItem.Text = languageDicoEn["MenuToolsCustomize"];
          optionsToolStripMenuItem.Text = languageDicoEn["MenuToolsOptions"];
          languagetoolStripMenuItem.Text = languageDicoEn["MenuLanguage"];
          englishToolStripMenuItem.Text = languageDicoEn["MenuLanguageEnglish"];
          frenchToolStripMenuItem.Text = languageDicoEn["MenuLanguageFrench"];
          helpToolStripMenuItem.Text = languageDicoEn["MenuHelp"];
          summaryToolStripMenuItem.Text = languageDicoEn["MenuHelpSummary"];
          indexToolStripMenuItem.Text = languageDicoEn["MenuHelpIndex"];
          searchToolStripMenuItem.Text = languageDicoEn["MenuHelpSearch"];
          aboutToolStripMenuItem.Text = languageDicoEn["MenuHelpAbout"];

          labelSaveFilePath.Text = languageDicoEn["Save File Path:"];
          labelSelectNewspaper.Text = languageDicoEn["Select Newspaper:"];
          labelSelectEdition.Text = languageDicoEn["Select Edition:"];
          buttonSelectEdition.Text = languageDicoEn["Select ->"];
          labelSelectDate.Text = languageDicoEn["Select date:"];
          buttonRemove.Text = languageDicoEn["Remove"];
          checkBoxEditionDuringWeekEnd.Text = languageDicoEn["Edition during the weekend"];
          groupBoxMultiSelectionDate.Text = languageDicoEn["Time period"];
          radioButtoSingleDate.Text = languageDicoEn["Single selected date"];
          radioButtonSeveralDates.Text = languageDicoEn["Several dates"];
          labelFromDate.Text = languageDicoEn["Start date:"];
          labelEndDate.Text = languageDicoEn["End date:"];
          buttonDownloadEditions.Text = languageDicoEn["Download selected editions"];
          buttonLaunchSelectedEdition.Text = languageDicoEn["Launch selected editions"];
          buttonLaunchTargetDirectory.Text = languageDicoEn["Launch target directory"];
          break;
        case "French":
          frenchToolStripMenuItem.Checked = true;
          englishToolStripMenuItem.Checked = false;
          fileToolStripMenuItem.Text = languageDicoFr["MenuFile"];
          newToolStripMenuItem.Text = languageDicoFr["MenuFileNew"];
          openToolStripMenuItem.Text = languageDicoFr["MenuFileOpen"];
          saveToolStripMenuItem.Text = languageDicoFr["MenuFileSave"];
          saveasToolStripMenuItem.Text = languageDicoFr["MenuFileSaveAs"];
          printPreviewToolStripMenuItem.Text = languageDicoFr["MenuFilePrint"];
          printPreviewToolStripMenuItem.Text = languageDicoFr["MenufilePageSetup"];
          quitToolStripMenuItem.Text = languageDicoFr["MenufileQuit"];
          editToolStripMenuItem.Text = languageDicoFr["MenuEdit"];
          cancelToolStripMenuItem.Text = languageDicoFr["MenuEditCancel"];
          redoToolStripMenuItem.Text = languageDicoFr["MenuEditRedo"];
          cutToolStripMenuItem.Text = languageDicoFr["MenuEditCut"];
          copyToolStripMenuItem.Text = languageDicoFr["MenuEditCopy"];
          pasteToolStripMenuItem.Text = languageDicoFr["MenuEditPaste"];
          selectAllToolStripMenuItem.Text = languageDicoFr["MenuEditSelectAll"];
          toolsToolStripMenuItem.Text = languageDicoFr["MenuTools"];
          personalizeToolStripMenuItem.Text = languageDicoFr["MenuToolsCustomize"];
          optionsToolStripMenuItem.Text = languageDicoFr["MenuToolsOptions"];
          languagetoolStripMenuItem.Text = languageDicoFr["MenuLanguage"];
          englishToolStripMenuItem.Text = languageDicoFr["MenuLanguageEnglish"];
          frenchToolStripMenuItem.Text = languageDicoFr["MenuLanguageFrench"];
          helpToolStripMenuItem.Text = languageDicoFr["MenuHelp"];
          summaryToolStripMenuItem.Text = languageDicoFr["MenuHelpSummary"];
          indexToolStripMenuItem.Text = languageDicoFr["MenuHelpIndex"];
          searchToolStripMenuItem.Text = languageDicoFr["MenuHelpSearch"];
          aboutToolStripMenuItem.Text = languageDicoFr["MenuHelpAbout"];

          labelSaveFilePath.Text = languageDicoFr["Save File Path:"];
          labelSelectNewspaper.Text = languageDicoFr["Select Newspaper:"];
          labelSelectEdition.Text = languageDicoFr["Select Edition:"];
          buttonSelectEdition.Text = languageDicoFr["Select ->"];
          labelSelectDate.Text = languageDicoFr["Select date:"];
          buttonRemove.Text = languageDicoFr["Remove"];
          checkBoxEditionDuringWeekEnd.Text = languageDicoFr["Edition during the weekend"];
          groupBoxMultiSelectionDate.Text = languageDicoFr["Time period"];
          radioButtoSingleDate.Text = languageDicoFr["Single selected date"];
          radioButtonSeveralDates.Text = languageDicoFr["Several dates"];
          labelFromDate.Text = languageDicoFr["Start date:"];
          labelEndDate.Text = languageDicoFr["End date:"];
          buttonDownloadEditions.Text = languageDicoFr["Download selected editions"];
          buttonLaunchSelectedEdition.Text = languageDicoFr["Launch selected editions"];
          buttonLaunchTargetDirectory.Text = languageDicoFr["Launch target directory"];
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
      if (listBoxSelectedEdition.Items.Count == 0)
      {
        buttonLaunchSelectedEdition.Enabled = false;
      }
      else
      {
        buttonLaunchSelectedEdition.Enabled = true;
      }
    }

    private void listBoxSelectedEdition_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateButtons();
    }

    private string GetTranslatedString(string index)
    {
      string result = string.Empty;
      string language = frenchToolStripMenuItem.Checked ? "french" : "english";

      switch (language.ToLower())
      {
        case "english":
          result = languageDicoEn.ContainsKey(index) ? languageDicoEn[index] :
           "the term: \"" + index + "\" has not been translated yet.\nPlease tell the developer to translate this term";
          break;
        case "french":
          result = languageDicoFr.ContainsKey(index) ? languageDicoFr[index] :
            "the term: \"" + index + "\" has not been translated yet.\nPlease tell the developer to translate this term";
          break;
      }

      return result;
    }
  }
}