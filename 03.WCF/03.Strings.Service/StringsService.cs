/*Create a Web service library which accepts two string as parameters. It should return the number of times the second string contains the first string. Test it with the integrated WCF client.*/

/*Press F5 to start WCF test Client and test the method
var text = "HOHOHOHOHOHOHO hohoh ho ho!";
var subString = "Hoh";
*/
namespace Strings.Service
{
    public class StringsService : IStringsService
    {
        public int FindSubstrings(string text, string substring)
        {
            text = text.ToLower();
            substring = substring.ToLower();

            int count = 0;
            var index = text.IndexOf(substring);

            while (index != -1)
            {
                count++;
                index = text.IndexOf(substring, index + 1);
            }

            return count;
        }
    }
}
