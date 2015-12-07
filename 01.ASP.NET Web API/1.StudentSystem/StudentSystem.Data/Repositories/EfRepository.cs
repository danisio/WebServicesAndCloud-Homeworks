namespace StudentSystem.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Contracts;

    public class EfRepository<T> : IRepository<T>
        where T : class
    {
        private readonly IStudentSystemDbContext contex;
        private readonly IDbSet<T> set;

        public EfRepository(IStudentSystemDbContext contex)
        {
            this.contex = contex;
            this.set = contex.Set<T>();
        }

        public IDbSet<T> Set
        {
            get { return this.set; }
        }

        public IQueryable<T> All()
        {
            return this.Set.AsQueryable();
        }

        public T FindById(int id)
        {
            return this.Set.Find(id);
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> condition)
        {
            return this.All().Where(condition);
        }

        public void Add(T entity)
        {
            this.ChangeState(entity, EntityState.Added);
        }

        public void Update(T entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        public void Delete(T entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public int SaveChanges()
        {
            return this.contex.SaveChanges();
        }

        private void ChangeState(T entity, EntityState state)
        {
            var entry = this.contex.Entry(entity);
            entry.State = state;
        }
    }
}
