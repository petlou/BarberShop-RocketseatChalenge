namespace BarberShop.Application.Utils;
internal static class GetMonthRange
{
    internal static (DateTime start, DateTime end) Run(DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1, hour: 0, minute: 0, second: 0, DateTimeKind.Utc);

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59, DateTimeKind.Utc);
        
        return (startDate, endDate);
    }
}
