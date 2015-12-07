/*Create a simple WCF service. It should have a method that accepts a DateTime parameter and returns the day of week (in Bulgarian) as string. Test it with the integrated WCF client.*/

//// Press F5 to start WCF test Client and test method returns day of week.
namespace DateTime.Service
{
    using System;
    using System.Globalization;
    using System.Threading;

    public class DateTimeService : IDateTimeService
    {
        public string FindDayOfWeek(DateTime date)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("bg-BG");

            return date.ToString("dddd");
        }
    }
}
