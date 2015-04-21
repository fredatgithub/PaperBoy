using System;

namespace ManualPaperBoy
{
  public class Newspaper
  {
    public string Name { get; set; }
    public string Url { get; set; }
    public DateTime PublishDate { get; set; }
    public bool IsPrintedOnWeekEnd { get; set; }
    public DateTime FirstPrintDate { get; set; }

    public Newspaper(string name, string url, DateTime publishDate, bool isPrintedonWeekEnd = false)
    {
      Name = name;
      Url = url;
      PublishDate = publishDate;
      IsPrintedOnWeekEnd = IsPrintedOnWeekEnd;
    }
  }
}