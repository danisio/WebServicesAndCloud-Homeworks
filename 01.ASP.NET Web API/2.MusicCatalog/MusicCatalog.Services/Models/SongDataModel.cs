namespace MusicCatalog.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using MusicCatalog.Models;

    public class SongDataModel
    {
        public static Expression<Func<Song, SongDataModel>> FromSong
        {
            get
            {
                return song => new SongDataModel
                {
                    Id = song.Id,
                    Title = song.Title,
                    Year = song.Year,
                    Genre = song.Genre,
                    AlbumId = song.AlbumId
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int? Year { get; set; }

        public string Genre { get; set; }

        [Required]
        public int AlbumId { get; set; }
    }
}