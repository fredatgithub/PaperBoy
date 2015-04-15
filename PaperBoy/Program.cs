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
using System.Net;

namespace PaperBoy
{
  class Program
  {
    static void Main()
    {
      Action<string> Display = s => Console.WriteLine(s);
      Display("Getting Direct Matin electronic PDF newspaper");
      // http://kiosque.directmatin.fr/Pdf.aspx?edition=NEP&date=20150415
      string url = "http://kiosque.directmatin.fr/Pdf.aspx?edition=NEP&date=";
      string dateEnglish = DateTime.Now.Year.ToString() +
        ToTwoDigits(DateTime.Now.Month) +
        ToTwoDigits(DateTime.Now.Day);
      url += dateEnglish;
      string fileName = "DirectMatin-" + dateEnglish + ".pdf";
      if (GetWebClientBinaries(url, fileName))
      {
        Display("download ok and file saved");
      }
      else
      {
        Display("error while downloading");
      }

      Display("Press a key to exit:");
      Console.ReadKey();
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
        Console.WriteLine(we.Message + "\n" + we.Status.ToString());
        result = false;
      }
      catch (NotSupportedException ne)
      {
        Console.WriteLine(ne.Message);
        result = false;
      }

      return result;
    }

    private static string GetWebClientString(string url = "http://www.google.com/")
    {
      // create a new instance of WebClient
      WebClient client = new WebClient();
      string result = string.Empty;
      // set the user agent to IE6
      client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)");
      try
      {
        // actually execute the GET request
        string ret = client.DownloadString(url);

        // ret now contains the contents of the webpage
        Console.WriteLine("First 256 bytes of response: " + ret.Substring(0, 265));
        result = ret;
      }
      catch (WebException we)
      {
        // WebException.Status holds useful information
        Console.WriteLine(we.Message + "\n" + we.Status.ToString());
      }
      catch (NotSupportedException ne)
      {
        // other errors
        Console.WriteLine(ne.Message);
      }

      return result;
    }
  }
}