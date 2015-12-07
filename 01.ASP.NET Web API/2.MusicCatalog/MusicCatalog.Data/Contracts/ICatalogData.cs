namespace MusicCatalog.Data.Contracts
{
    using Models;
    using Repositories;

    public interface ICatalogData
    {
        IRepository<Album> Albums { get; }

        IRepository<Artist> Artists { get; }

        IRepository<Song> Songs { get; }

        void SaveChanges();
    }
}