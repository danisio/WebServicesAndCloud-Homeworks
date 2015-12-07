/*Create a console-based client for the WCF service above. Use the "Add Service Reference" in Visual Studio.*/

// Press F5 to start
namespace DateTime.ServiceClient
{
    using System;
    using ServiceReference;

    public class Startup
    {
        public static void Main()
        {
            var client = new DateTimeServiceClient();

            var testDate = new DateTime(2015, 3, 3);
            var result = client.FindDayOfWeek(testDate);

            Console.WriteLine("Day of week --> {0}", result);
            Console.ReadLine();
        }
    }
}
