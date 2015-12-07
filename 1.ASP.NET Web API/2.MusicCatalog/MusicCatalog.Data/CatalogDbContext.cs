namespace MusicCatalog.Data
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using Models;

    public class CatalogDbContext : DbContext, ICatalogDbContext
    {
        public CatalogDbContext()
            : base("MusicCatalog")
        {
        }

        public IDbSet<Album> Albums { get; set; }

        public IDbSet<Artist> Artists { get; set; }

        public IDbSet<Song> Songs { get; set; }

        public new IDbSet<T> Set<T>()
            where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
