namespace MusicCatalog.ConsoleClient.Models
{
    using System.Collections.Generic;

    public class AlbumModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Producer { get; set; }

        public ICollection<SongModel> Songs { get; set; }

        public ICollection<ArtistModel> Artists { get; set; }
    }
}
