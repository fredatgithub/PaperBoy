using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace PaperBoy
{
  internal static class Program
  {
    private static void Main(string[] arguments)
    {
      Action<string> display = Console.WriteLine;
      string saveFilePath = string.Empty;
      string editionRequested = string.Empty;
      if (arguments.Length != 0)
      {
        if (arguments[0].Contains("help") || arguments[0].Contains("?") ||
          arguments[0].Contains("Help") || arguments[0].Contains("HELP"))
        {
          Usage();
          return;
        }

        //saveFilePath = Directory.Exists(arguments[0].Substring(6)) ? arguments[0].Substring(6) : Properties.Settings.Default.saveFilePath;
        saveFilePath = Properties.Settings.Default.saveFilePath;

        if (arguments.Length >= 2)
        {

          editionRequested = LoadEditioncodes().ContainsValue(arguments[1].Substring(9)) ? arguments[1].Substring(9) : "NEP";
        }
        else
        {
          editionRequested = "NEP";
        }
      }
      else
      {
        // default values
        saveFilePath = Properties.Settings.Default.saveFilePath;
        editionRequested = "NEP";
      }

      //Checking if save path is correct
      string myDocumentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      // if copy is not easy then get my documents directory as default
      //saveFilePath = myDocumentsDirectory;
      while (!Directory.Exists(saveFilePath))
      {
        display("The save file path variable is not correct: " + saveFilePath);
        display("Please enter the path to save the file:");
        saveFilePath = Console.ReadLine();
        Properties.Settings.Default.saveFilePath = saveFilePath;
        Properties.Settings.Default.Save();
      }

      display("Getting Direct Matin electronic PDF newspaper");
      // http://kiosque.directmatin.fr/Pdf.aspx?edition=NEP&date=20150415
      // change of address on 14-06-2018
      // rss.cnews.fr/pdf/NEP/20180614
      // old address string url = "http://kiosque.directmatin.fr/Pdf.aspx?edition=";
      // new url address starting on 14-06-2018
      string url = "http://rss.cnews.fr/pdf";
      string dateEnglish = DateTime.Now.Year +
        ToTwoDigits(DateTime.Now.Month) +
        ToTwoDigits(DateTime.Now.Day);
      url += $"/{editionRequested}/";
      url += dateEnglish;
      string editionName = string.Empty;
      editionName = GetEditionName(editionRequested).Replace(" ", "_").Replace("'", "_");
      editionName = ReplaceWindowsForbiddenCharacters(editionName);
      string fileName = Path.Combine(saveFilePath, editionName + "-" + dateEnglish + ".pdf");
      bool fileDeleted = false;
      // test internet connexion
      bool internetConnexion = false;
      while (!internetConnexion)
      {
        int numberOfLoop = 1;
        if (IsInternetConnected())
        {
          internetConnexion = true;
          display($"{Environment.NewLine}Now connected to Internet.");
        }
        else
        {
          display("Waiting for Internet connexion ...");
          numberOfLoop  = numberOfLoop >= int.MaxValue ? 1 : numberOfLoop++;
          Thread.Sleep(numberOfLoop++ * 1000);
        }
      }

      // check if file already downloaded
      string result = string.Empty;
      if (File.Exists(fileName) && FileGetSize(fileName) >= 99)
      {
        result = $"The file:{Environment.NewLine}{fileName}{Environment.NewLine}has already been downloaded.";
      }
      else       // exclude week-end
      {
        if (OutsideWeekEnd())
        {
          result = GetWebClientBinaries(url, fileName) ? string.Format("Download ok{2}{2}File saved in the following directory:{2}{0}{2}{2}The size of the file is {1:n0} bytes.", fileName,
             FileGetSize(fileName), Environment.NewLine) : "error while downloading";
        }
        else
        {
          result = "No magazine during the weekend.";
        }
      }

      Thread.Sleep(5000);
      long fileSize = FileGetSize(fileName);
      if (fileSize == 0)
      {
        File.Delete(fileName);
        fileDeleted = true;
      }

      display(string.Empty);
      display(fileDeleted ? "The download file has a size of zero byte so it has been deleted" : result);
      display(string.Empty);
      display($"The url is {url}");
      display(string.Empty);
      display($"The  filename is {fileName}");
      display(string.Empty);
      display($"{Environment.NewLine}Press a key to exit:");
      Console.ReadKey();
    }

    public static bool OutsideWeekEnd()
    {
      return (DateTime.Now.DayOfWeek != DayOfWeek.Sunday) && (DateTime.Now.DayOfWeek != DayOfWeek.Saturday);
    }

    private static bool IsInternetConnected()
    {
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.google.fr");

      try
      {
        // variable response not used but useful to check for exception
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    private static void Usage()
    {
      Action<string> display = Console.WriteLine;
      display("Paperboy is an application to get your electronic newspaper automatically");
      display("Application created by Freddy juhel in April 2015. Copyright (c) 2015 MIT");
      display(string.Empty);
      display("Usage:");
      display(string.Empty);
      display("paperboy -help");
      display("paperboy -?");
      display("paperboy -Help");
      display("This help");
      display(string.Empty);
      display("paperboy -path=<path to save the PDF file.> -edition=<edition code>");
      display("Example: ");
      display(string.Empty);
      display("paperboy -path=c:\\temp -edition=NEP");
      display(string.Empty);
      display("Direct Matin editions Code in UPPERCASE:");
      display("----------------------------------------");
      display("Direct Matin Edition Nationale is the default edition");
      display(string.Empty);
      display("Direct Matin Edition Nationale NEP");
      display("Direct Matin Bordeaux          BDX");
      display("Direct Matin Lille             LIL");
      display("Direct Matin Lyon              LYO");
      display("Direct Matin Provence          PRO");
      display("Direct Matin Montpellier       MTP");
      display("Direct Matin Grand ouest       VP1");
      display("Direct Matin Côte-d'azur       NP");
      display("Direct Matin Strasbourg        SP");
      display("Direct Matin Toulouse          TP");
      display(string.Empty);
      display("Press a key to exit:");
      Console.ReadKey();
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

    private static string ReplaceWindowsForbiddenCharacters(string input, string charToBeReplaced = "")
    {
      string result = input;
      string[] forbiddenWindowsFilenameCharacters = { "\\", "/", ":", "*", "?", "\"", "<", ">", "|" };
      foreach (var item in forbiddenWindowsFilenameCharacters)
      {
        result = result.Replace(item, charToBeReplaced);
      }

      return result;
    }

    private static void DisplayMessageOk(string message)
    {
      Console.WriteLine(message);
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

    private static string ToTwoDigits(int number)
    {
      return number < 10 ? "0" + number : number.ToString();
    }

    private static bool GetWebClientBinaries(string url = "http://www.google.fr/",
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
      catch (WebException we)
      {
        Console.WriteLine(we.Message + "\n" + we.Status);
        result = false;
      }
      catch (NotSupportedException ne)
      {
        Console.WriteLine(ne.Message);
        result = false;
      }

      return result;
    }
  }
}