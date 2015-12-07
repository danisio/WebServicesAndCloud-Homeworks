namespace StudentSystem.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Data.Contracts;
    using Models;
    using StudentSystem.Models;

    public class StudentsController : ApiController
    {
        private readonly IStudentSystemData data;

        public StudentsController(IStudentSystemData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IQueryable<StudentModel> All()
        {
            return this.data.Students
                .All()
                .Select(StudentModel.FromStudent);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var student = this.data.Students
                .Search(st => st.Id == id)
                .Select(StudentModel.FromStudent);

            if (!student.Any())
            {
                return this.NotFound();
            }

            return this.Ok(student);
        }

        [HttpPut]
        public IHttpActionResult Update(StudentModel student)
        {
            if (!this.ModelState.IsValid || student == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var existingStudent = this.data.Students.FindById(student.Id);

            if (existingStudent == null)
            {
                return this.BadRequest();
            }

            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.StudentNumber = student.StudentNumber;

            this.data.SaveChanges();

            student.Id = existingStudent.Id;
            return this.Ok(student);
        }

        [HttpPost]
        public IHttpActionResult Create(StudentModel student)
        {
            if (!this.ModelState.IsValid || student == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var newStudent = new Student()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                StudentNumber = student.StudentNumber
            };

            this.data.Students.Add(newStudent);
            this.data.Students.SaveChanges();

            student.Id = newStudent.Id;
            return this.Ok(student);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var student = this.data.Students.FindById(id);
            if (student == null)
            {
                return this.NotFound();
            }

            this.data.Students.Delete(student);
            this.data.SaveChanges();

            return this.Ok(student);
        }
    }
}
