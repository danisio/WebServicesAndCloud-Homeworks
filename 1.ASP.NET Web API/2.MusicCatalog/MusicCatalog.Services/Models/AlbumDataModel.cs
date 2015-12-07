namespace MusicCatalog.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using MusicCatalog.Models;

    public class AlbumDataModel
    {
        public static Expression<Func<Album, AlbumDataModel>> FromAlbum
        {
            get
            {
                return album => new AlbumDataModel
                {
                    Id = album.Id,
                    Title = album.Title,
                    Producer = album.Producer,
                    Songs = album.Songs.Select(s => new SongDataModel
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Year = s.Year,
                        Genre = s.Genre,
                        AlbumId = s.AlbumId
                    }),
                    Artists = album.Artists.Select(a => new ArtistDataModel
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Country = a.Country,
                        DateOfBirth = a.DateOfBirth
                    })
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Producer { get; set; } 

        public IEnumerable<SongDataModel> Songs { get; set; }

        public IEnumerable<ArtistDataModel> Artists { get; set; }
    }
}