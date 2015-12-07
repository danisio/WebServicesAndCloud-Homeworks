// Non-generic methods for homework only :)
namespace MusicCatalog.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Data;
    using Models;

    public class Startup
    {
        private const string ServerUri = "http://localhost:5000/";
        private const string Artists = "api/Artists/";
        private const string Songs = "api/Songs/";
        private const string Albums = "api/albums";
        private const string Create = "Create";
        private const string Update = "Update/";

        private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri(ServerUri) };

        public static void Main()
        {
            var catalog = new CatalogDbInitializer<CatalogDbContext>();
            catalog.Initialize();

            Console.WriteLine("Database was created successfully!");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();

            PrintAlbums();
            Console.ReadLine();
            PrintArtists();
            Console.ReadLine();
            AddNewSong();
            Console.ReadLine();
            UpdateFirstArtist();
        }

        private static async void PrintArtists()
        {
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var response = await Client.GetAsync(Artists);
            Console.WriteLine("Artists:");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Console.WriteLine("..Press any key to continue..");
        }

        private static async void UpdateFirstArtist()
        {
            Console.WriteLine("Updating artist...");
            var artist = new ArtistModel
            {
                Id = 1,
                Name = "new name from console"
            };

            HttpResponseMessage response = await Client.PutAsJsonAsync(Artists + Update, artist);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artist updated!");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        private static async void AddNewSong()
        {
            {
                Console.WriteLine("Adding new song...");
                var song = new SongModel
                {
                    Title = "Song from console",
                    Genre = "jazz",
                    Year = 2000,
                    AlbumId = 1
                };

                HttpResponseMessage response = await Client.PostAsJsonAsync(Songs + Create, song);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Song added!");
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }

                Console.WriteLine("..Press any key to continue..");
            }
        }

        private static async void PrintAlbums()
        {
            Console.WriteLine("Albums:");
            var response = await Client.GetAsync(Albums);
            if (response.IsSuccessStatusCode)
            {
                var albums = response.Content.ReadAsAsync<IEnumerable<AlbumModel>>().Result;

                foreach (var album in albums)
                {
                    Console.WriteLine("Id : {0}, Title: {1}, Producer: {2}", album.Id, album.Title, album.Producer);
                    Console.WriteLine("\tSongs:");
                    foreach (var song in album.Songs)
                    {
                        Console.WriteLine("\t\t-{0}, {1}, {2}", song.Title, song.Genre, song.Year);
                    }

                    Console.WriteLine("\tArtists:");
                    foreach (var art in album.Artists)
                    {
                        Console.WriteLine("\t\t-{0}, {1}", art.Name, art.Country);
                    }
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine("..Press any key to continue..");
        }
    }
}
