using BlogApplication.Models.Comment;
using BlogApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApplication.Models.Blog
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public bool Published { get; set; }
        public string PictureUrl { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public virtual List<Comments> Comment { get; set; }
        public BlogPost()
        {
            DateCreated = DateTime.Now;
            Comment = new List<Comments>();
        }
    }
}