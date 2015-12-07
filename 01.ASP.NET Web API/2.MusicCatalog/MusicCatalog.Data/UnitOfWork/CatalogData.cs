namespace MusicCatalog.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Models;
    using Repositories;

    public class CatalogData : ICatalogData
    {
        private readonly ICatalogDbContext context;
        private readonly IDictionary<Type, object> repositories;

        public CatalogData()
            : this(new CatalogDbContext())
        {
        }

        public CatalogData(ICatalogDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Album> Albums
        {
            get { return this.GetRepository<Album>(); }
        }

        public IRepository<Artist> Artists
        {
            get { return this.GetRepository<Artist>(); }
        }

        public IRepository<Song> Songs
        {
            get { return this.GetRepository<Song>(); }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            Type typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var newRepository = Activator.CreateInstance(typeof(EfRepository<T>), this.context);
                this.repositories.Add(typeOfModel, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
