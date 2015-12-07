/*Create a console client for the WCF service above.
Use the svcutil.exe tool to generate the proxy classes.*/

//// Press F5 to start
namespace Strings.ServiceClient
{
    using System;

    public class Startup
    {
        public static void Main()
        {
            var client = new StringsServiceClient();
            client.Open();

            string text = "HOHOHOHOHOHOHO hohoh ho ho!";
            string subString = "Hoh";
            int result = client.FindSubstrings(text, subString);

            Console.WriteLine("The text contains substring \"{0}\" times.", result);
            Console.ReadLine();
            client.Close();
        }
    }
}
