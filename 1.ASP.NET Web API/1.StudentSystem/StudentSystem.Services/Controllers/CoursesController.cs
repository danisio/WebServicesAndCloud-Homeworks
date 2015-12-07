namespace StudentSystem.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Data.Contracts;
    using Models;
    using StudentSystem.Models;

    public class CoursesController : ApiController
    {
        private readonly IStudentSystemData data;

        public CoursesController(IStudentSystemData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IQueryable<CourseModel> All()
        {
            return this.data.Courses
                .All()
                .Select(CourseModel.FromCourse);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var course = this.data.Courses
                .Search(c => c.Id == id)
                .Select(CourseModel.FromCourse);

            if (!course.Any())
            {
                return this.NotFound();
            }

            return this.Ok(course);
        }

        [HttpPut]
        public IHttpActionResult Update(CourseModel course)
        {
            if (!this.ModelState.IsValid || course == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var existingCourse = this.data.Courses.FindById(course.Id);

            if (existingCourse == null)
            {
                return this.BadRequest();
            }

            existingCourse.Name = course.Name;
            existingCourse.Description = course.Description;

            this.data.SaveChanges();

            course.Id = existingCourse.Id;
            return this.Ok(course);
        }

        [HttpPost]
        public IHttpActionResult Create(CourseModel course)
        {
            if (!this.ModelState.IsValid || course == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var newCourse = new Course()
            {
                Name = course.Name,
                Description = course.Description
            };

            this.data.Courses.Add(newCourse);
            this.data.Courses.SaveChanges();

            course.Id = newCourse.Id;
            return this.Ok(course);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var course = this.data.Courses.FindById(id);
            if (course == null)
            {
                return this.NotFound();
            }

            this.data.Courses.Delete(course);
            this.data.SaveChanges();

            return this.Ok(course);
        }
    }
}
