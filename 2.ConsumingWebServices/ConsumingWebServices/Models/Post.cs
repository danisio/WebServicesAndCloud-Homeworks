namespace ConsumingWebServices.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class Post
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "posts")]
        public List<Post> Posts { get; set; }
    }
}
