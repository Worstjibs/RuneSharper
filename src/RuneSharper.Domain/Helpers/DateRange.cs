namespace RuneSharper.Domain.Helpers;

public class DateRange
{
    public DateRange(DateTime dateFrom, DateTime dateTo)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
    }

    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}
