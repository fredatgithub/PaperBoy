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
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace PaperBoy
{
  class Program
  {
    static void Main(string[] arguments)
    {
      Action<string> display = Console.WriteLine;
      string saveFilePath = string.Empty;
      string editionRequested = string.Empty;
      if (arguments.Length != 0)
      {
        if (arguments[0].Contains("help") || arguments[0].Contains("?") ||
          arguments[0].Contains("Help") || arguments[0].Contains("HELP") )
        {
          Usage();
          return;
        }

        if (Directory.Exists(arguments[0].Substring(6)))
        {
          saveFilePath = arguments[0].Substring(6);
        }
        else
        {
          saveFilePath = Properties.Settings.Default.saveFilePath;
        }

        if (arguments.Length >= 2)
        {

          if (LoadEditioncodes().ContainsValue(arguments[1].Substring(9)))
          {
            editionRequested = arguments[1].Substring(9);
          }
          else
          {
            editionRequested = "NEP";
          }
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
      while (!Directory.Exists(saveFilePath))
      {
        display("The save file path variable is not correct: " + saveFilePath);
        display("Please enter the path to save the file:");
        saveFilePath = Console.ReadLine();
        Properties.Settings.Default.saveFilePath = saveFilePath;
      }
      
      display("Getting Direct Matin electronic PDF newspaper");
      // http://kiosque.directmatin.fr/Pdf.aspx?edition=NEP&date=20150415
      string url = "http://kiosque.directmatin.fr/Pdf.aspx?edition=";
      string dateEnglish = DateTime.Now.Year +
        ToTwoDigits(DateTime.Now.Month) +
        ToTwoDigits(DateTime.Now.Day);
      url += editionRequested + "&date=";
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
        if (IsInternetConnected())
        {
          internetConnexion = true;
          display("Now connected to Internet.");
        }
        else
        {
          display("Waiting for Internet connexion ...");
          Thread.Sleep(1000);
        }
      }

      // check if file already downloaded
      string result = string.Empty;
      if (File.Exists(fileName) && FileGetSize(fileName) != 0)
      {
        result = "The file " + fileName + " has already been downloaded.";
      }
      else
      {
        result = GetWebClientBinaries(url, fileName) ? "download ok and file saved in " + fileName : "error while downloading";
      }
      
      Thread.Sleep(5000);
      long fileSize = FileGetSize(fileName);
      if (fileSize == 0)
      {
        File.Delete(fileName);
        fileDeleted = true;
      }

      display(fileDeleted ? "The download file has a size of zero byte so it has been deleted" : result);

      display("Press a key to exit:");
      Console.ReadKey();
    }

    private static bool IsInternetConnected()
    {
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.google.fr");

      try
      {
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
      display("paperboy is an application to get your electronic newspaper automatically");
      display("Application created by Freddy juhel in April 2015. Copyright (c) 2015 MIT");
      display("");
      display("Usage:");
      display("");
      display("paperboy -help");
      display("paperboy -?");
      display("paperboy -Help");
      display("This help");
      display("");
      display("paperboy -path=<path to save the PDF file.> -edition=<edition code>");
      display("Example: ");
      display("");
      display("paperboy -path=c:\\temp -edition=NEP");
      display("");
      display("Direct Matin editions Code in UPPERCASE:");
      display("----------------------------------------");
      display("Direct Matin Edition Nationale is the default edition");
      display("");
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
      display("");
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

    private void DisplayMessageOk(string message)
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