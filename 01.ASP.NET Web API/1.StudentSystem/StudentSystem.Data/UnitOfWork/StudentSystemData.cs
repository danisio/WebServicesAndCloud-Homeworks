namespace StudentSystem.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Models;
    using Repositories;

    public class StudentSystemData : IStudentSystemData
    {
        private readonly IStudentSystemDbContext context;
        private readonly IDictionary<Type, object> repositories;

        public StudentSystemData(IStudentSystemDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Course> Courses
        {
            get { return this.GetRepository<Course>(); }
        }

        public IRepository<Homework> Homeworks
        {
            get { return this.GetRepository<Homework>(); }
        }

        public IRepository<Student> Students
        {
            get { return this.GetRepository<Student>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
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
