namespace StudentSystem.Data.Contracts
{
    using Models;

    public interface IStudentSystemData
    {
        IRepository<Course> Courses { get; }

        IRepository<Student> Students { get; }

        IRepository<Homework> Homeworks { get; }

        int SaveChanges();
    }
}