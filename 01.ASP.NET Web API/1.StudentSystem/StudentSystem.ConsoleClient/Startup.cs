namespace StudentSystem.ConsoleClient
{
    using System;
    using Data;
    using Data.UnitOfWork;
    using Models;

    public class Startup
    {
        public static void Main()
        {
            var db = new StudentSystemDbContext();
            var studentData = new StudentSystemData(db);

            studentData.Courses.Add(new Course() { Name = "DSA" });
            studentData.Courses.Add(new Course() { Name = "WebServices" });
            studentData.Courses.Add(new Course() { Name = "HQC" });
            studentData.Courses.Add(new Course() { Name = "Databases" });

            studentData.SaveChanges();

            var student1 = new Student() { FirstName = "Ivaylo", LastName = "Kenov", StudentNumber = "SN54353463" };
            studentData.Students.Add(student1);
            studentData.Students.Add(new Student() { FirstName = "Niki", LastName = "Kostov", StudentNumber = "SN654654765" });
            studentData.Students.Add(new Student() { FirstName = "Doncho", LastName = "Minkov", StudentNumber = "SN6575465" });

            student1.Homeworks.Add(new Homework()
            {
                Content = "Homework content"
            });

            studentData.SaveChanges();
            Console.WriteLine("Successfully added!");
        }
    }
}
