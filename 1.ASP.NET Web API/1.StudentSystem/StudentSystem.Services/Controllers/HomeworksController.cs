namespace StudentSystem.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Data.Contracts;
    using Models;
    using StudentSystem.Models;

    public class HomeworksController : ApiController
    {
        private readonly IStudentSystemData data;

        public HomeworksController(IStudentSystemData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IQueryable<HomeworkModel> All()
        {
            return this.data.Homeworks
                .All()
                .Select(HomeworkModel.FromHomework);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var homework = this.data.Homeworks
                .Search(hwk => hwk.Id == id)
                .Select(HomeworkModel.FromHomework);

            if (!homework.Any())
            {
                return this.NotFound();
            }

            return this.Ok(homework);
        }

        [HttpPut]
        public IHttpActionResult Update(HomeworkModel homework)
        {
            if (!this.ModelState.IsValid || homework == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var existingHomework = this.data.Homeworks.FindById(homework.Id);

            if (existingHomework == null)
            {
                return this.BadRequest();
            }

            existingHomework.Content = homework.Content;

            this.data.SaveChanges();

            homework.Id = existingHomework.Id;
            return this.Ok(homework);
        }

        [HttpPost]
        public IHttpActionResult Create(HomeworkModel homework)
        {
            if (!this.ModelState.IsValid || homework == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var newHomework = new Homework()
            {
                Content = homework.Content
            };

            this.data.Homeworks.Add(newHomework);
            this.data.Homeworks.SaveChanges();

            homework.Id = newHomework.Id;
            return this.Ok(homework);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var homework = this.data.Homeworks.FindById(id);
            if (homework == null)
            {
                return this.NotFound();
            }

            this.data.Homeworks.Delete(homework);
            this.data.SaveChanges();

            return this.Ok(homework);
        }
    }
}
