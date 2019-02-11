using System;

namespace TestDownloadClient
{
  internal class Program
  {
    private static void Main()
    {
      Action<string> display = Console.WriteLine;
      const string url = "https://rss.cnews.fr/pdf/NEP/20190211";
      PaperBoy.Program.GetWebClientBinaries(url, "test.pdf");


      Console.ForegroundColor = ConsoleColor.Green;
      display($"{Environment.NewLine}Press a key to exit:");
      Console.ReadKey();
    }
  }
}