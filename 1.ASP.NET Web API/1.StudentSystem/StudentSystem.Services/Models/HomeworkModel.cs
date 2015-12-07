namespace StudentSystem.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using StudentSystem.Models;

    public class HomeworkModel
    {
        public static Expression<Func<Homework, HomeworkModel>> FromHomework
        {
            get
            {
                return homework => new HomeworkModel
                {
                    Id = homework.Id,
                    Content = homework.Content
                };
            }
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Content { get; set; }
    }
}