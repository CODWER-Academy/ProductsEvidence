using System;
using Catalogclass;
using Producatorclass;
using Reducereclass;

namespace ExtensionMethdDemo {
    
    public static class DateTimeExtensions
{
    // This extension method checks if the given date is within the start and end dates.
    public static bool IsInRange(this DateTime date, DateTime startDate, DateTime endDate)
    {
        return date >= startDate && date <= endDate;
    }
}
}
