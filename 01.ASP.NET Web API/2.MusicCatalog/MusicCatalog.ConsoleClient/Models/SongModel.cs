namespace MusicCatalog.ConsoleClient.Models
{
    public class SongModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public int? Year { get; set; }

        public string Genre { get; set; }
        
        public int AlbumId { get; set; }
    }
}
