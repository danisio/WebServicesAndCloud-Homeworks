namespace MusicCatalog.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using Models;

    public class CatalogDbInitializer<T> : CreateDatabaseIfNotExists<CatalogDbContext>
    {
        public void Initialize()
        {
            var contex = new CatalogDbContext();
            this.Seed(contex);
        }

        protected override void Seed(CatalogDbContext context)
        {
            var album = new Album();
            album.Title = "Album1 title";
            album.Producer = "Producer name";

            var artists = new List<Artist>();
            artists.Add(new Artist() { Name = "Artist1 name", DateOfBirth = DateTime.Now, Country = "England" });
            artists.Add(new Artist() { Name = "Artist2 name", DateOfBirth = DateTime.Now, Country = "Portugal" });
            artists.Add(new Artist() { Name = "Artist3 name", DateOfBirth = DateTime.Now, Country = "Spain" });

            foreach (var art in artists)
            {
                context.Artists.AddOrUpdate(art);
                album.Artists.Add(art);
            }

            var songs = new List<Song>();
            songs.Add(new Song() { Title = "Song1 title", AlbumId = 1, Year = 2015, Genre = "pop" });
            songs.Add(new Song() { Title = "Song2 title", AlbumId = 1, Year = 2015, Genre = "rock" });
            songs.Add(new Song() { Title = "Song3 title", AlbumId = 1, Year = 2015, Genre = "pop" });
            songs.Add(new Song() { Title = "Song4 title", AlbumId = 1, Year = 2015, Genre = "rock" });
            songs.Add(new Song() { Title = "Song5 title", AlbumId = 1, Year = 2015, Genre = "pop" });
            songs.Add(new Song() { Title = "Song6 title", AlbumId = 1, Year = 2015, Genre = "rock" });

            foreach (var song in songs)
            {
                context.Songs.AddOrUpdate(song);
                album.Songs.Add(song);
            }

            context.Albums.AddOrUpdate(album);
            context.SaveChanges();

            Database.SetInitializer<CatalogDbContext>(new CatalogDbInitializer<CatalogDbContext>());
        }
    }
}
