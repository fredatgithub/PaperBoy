using System;

namespace TestConsoleAppColor
{
  internal class Program
  {
    private static void Main()
    {
      WriteTextInColor(ConsoleColor.Red);
      WriteTextInColor(ConsoleColor.Green);
      WriteTextInColor(ConsoleColor.DarkGreen);
      WriteTextInColor(ConsoleColor.White);

      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("Press any key to exit:");
      Console.ReadKey();
    }

    private static void WriteTextInColor(ConsoleColor color)
    {
      Console.ForegroundColor = color;
      Console.WriteLine($"text in {color} color");
    }
  }
}
