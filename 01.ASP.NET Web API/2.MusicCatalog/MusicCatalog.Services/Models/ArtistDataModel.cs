namespace MusicCatalog.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using MusicCatalog.Models;

    public class ArtistDataModel
    {
        public static Expression<Func<Artist, ArtistDataModel>> FromStudent
        {
            get
            {
                return artist => new ArtistDataModel
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    Country = artist.Country,
                    DateOfBirth = artist.DateOfBirth,
                    Albums = artist.Albums.Select(a => new AlbumDataModel
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Producer = a.Producer
                    })
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public IEnumerable<AlbumDataModel> Albums { get; set; }
    }
}