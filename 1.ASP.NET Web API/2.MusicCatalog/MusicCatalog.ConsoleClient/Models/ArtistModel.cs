namespace MusicCatalog.ConsoleClient.Models
{
    using System;
    using System.Collections.Generic;

    public class ArtistModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public ICollection<AlbumModel> Albums { get; set; }
    }
}
