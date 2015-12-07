namespace ConsumingWebServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Models;

    public class Startup
    {
        private const string Posts = "posts";
        private const string JsonFormat = "application/json";
        private const string Url = "http://jsonplaceholder.typicode.com";
        private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri(Url) };

        public static void Main()
        {
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonFormat));

            int count;
            Console.Write("Enter count of posts: ");
            while (!int.TryParse(Console.ReadLine(), out count))
            {
                Console.Write("Enter valid number:");
            }

            Console.Write("Enter string to search (e.g. \"rerum\"): ");
            string search = Console.ReadLine();

            PrintPosts(count, search);
            Console.ReadLine();
        }

        private static async void PrintPosts(int count, string search)
        {
            var response = await Client.GetAsync(Posts);
            if (response.IsSuccessStatusCode)
            {
                var posts = response.Content.ReadAsAsync<IEnumerable<Post>>().Result;
                var result = posts.Where(p => p.Body.Contains(search) || p.Title.Contains(search)).Take(count);

                foreach (var post in result)
                {
                    Console.WriteLine("USER ID: {0}", post.UserId);
                    Console.WriteLine("TITLE: {0}", post.Title);
                    Console.WriteLine("BODY: {0}", post.Body);
                    Console.WriteLine(string.Join("-", new string[80]));
                }
            }
        }
    }
}
