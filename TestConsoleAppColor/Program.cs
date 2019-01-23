using System;

namespace TestConsoleAppColor
{
  internal class Program
  {
    private static void Main()
    {
      WriteTextInColor(ConsoleColor.DarkGreen);
      WriteTextInColor(ConsoleColor.White);
      WriteTextInColor(ConsoleColor.Gray);
      WriteTextInColor(ConsoleColor.Cyan);
      WriteTextInColor(ConsoleColor.DarkBlue);
      WriteTextInColor(ConsoleColor.DarkMagenta);
      WriteTextInColor(ConsoleColor.DarkYellow);
      WriteTextInColor(ConsoleColor.Blue);
      WriteTextInColor(ConsoleColor.DarkGray);
      WriteTextInColor(ConsoleColor.Magenta);
      WriteTextInColor(ConsoleColor.Yellow);
      WriteTextInColor(ConsoleColor.Red);
      WriteTextInColor(ConsoleColor.Green);

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
