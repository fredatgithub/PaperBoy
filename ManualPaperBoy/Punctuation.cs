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
using System.Text;

namespace Tools
{
  public static class Punctuation
  {
    public const string Comma = ",";
    public const string Colon = ":";
    public const string SemiColon = ";";
    public const string OneSpace = " ";
    public const string UnderScore = "_";
    public const string SignAt = "@";
    public const string Ampersand = "&";
    public const string SignSharp = "#";
    public const string Period = ".";
    public const string Backslash = "\\";
    public const string Slash = "/";
    public const string OpenParenthesis = "(";
    public const string CloseParenthesis = ")";
    public const string OpenCurlyBrace = "{";
    public const string CloseCurlyBrace = "}";
    public const string OpeningBracket = "[";
    public const string ClosingBracket = "]";
    public const string LessThan = "<";
    public const string GreaterThan = ">";
    public const string DoubleQuote = "\"";
    public const string SimpleQuote = "'";
    public const string Tilde = "~";
    public const string Pipe = "|";
    public const string Plus = "+";
    public const string Minus = "-";
    public const string Dash = "-";
    public const string Multiply = "*";
    public const string Divide = "/";
    public const string Equal = "=";
    public const string Dollar = "$";
    public const string Pound = "£";
    public const string Percent = "%";
    public const string QuestionMark = "?";
    public const string ExclamationPoint = "!";
    public const string Chapter = "§";
    public const string Micro = "µ";
    public const string Copyright = "®";
    public static string CrLf = Environment.NewLine;

    public static string Tabulate(ushort numberOfTabulation = 1)
    {
      string result = string.Empty;
      for (int number = 0; number < numberOfTabulation; number++)
      {
        result += " ";
      }

      return result;
    }

    public static string CreateSentence(params string[] listOfCharacters)
    {
      StringBuilder result = new StringBuilder();
      foreach (string character in listOfCharacters)
      {
        result.Append(character);
      }

      return result.ToString();
    }

    public static string CreateSentence(params PunctuationChar[] listOfCharacters)
    {
      StringBuilder result = new StringBuilder();
      foreach (var character in listOfCharacters)
      {
        result.Append(character);
      }

      return result.ToString();
    }

    public enum PunctuationChar
    {
      // values according to ASCII table
      Comma = 44,
      Colon = 58,
      SemiColon = 59,
      OneSpace = 32,
      UnderScore = 95,
      SignAt = 64,
      Ampersand = 38,
      SignSharp = 35,
      SignNumber = Pound,
      Period = 46,
      Backslash = 92,
      OpenParenthesis = 40,
      CloseParenthesis = 41,
      OpenCurlyBrace = 123,
      OpeningBrace = OpenCurlyBrace,
      CloseCurlyBrace = 125,
      ClosingBrace = CloseCurlyBrace,
      OpeningBracket = 91,
      ClosingBracket = 93,
      LessThan = 60,
      GreaterThan = 62,
      DoubleQuote = 34,
      SimpleQuote = 39,
      Tilde = 126,
      Caret = 94,
      Circumflex = Caret,
      Pipe = 124,
      VerticalBar = Pipe,
      Plus = 43,
      Dash = 45,
      Minus = Dash,
      Multiply = 42,
      SignMultiply = 215,
      Slash = 47,
      Divide = Slash,
      SignDivision = 247,
      Equal = 61,
      Dollar = 36,
      Pound = 163,
      SignYen = 165,
      Percent = 37,
      QuestionMark = 63,
      ExclamationPoint = 33,
      Chapter = 167,
      Section = Chapter,
      Copyright = 169,
      Micro = 181,
      LeftDoubleAngleQuotes = 171,
      RightDoubleAngleQuotes = 187,
      LeftSingleQuotationMark = 8216,
      RightSingleQuotationMark = 8217,
      LeftDoubleQuotationMark = 8220,
      RightdoubleQuotationMark = 8221,
      SignTradeMark = 8482,
      SignEuro = 8364,
      CrLf = 0,
      NewLine = CrLf
    }
  }
}