using BlogApplication.Models.Blog;
using BlogApplication.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApplication.Models.ViewModels
{
    public class DetailsBlogViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Published { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public virtual BlogPost Post { get; set; }
        public string PostId { get; set; }
        public virtual List<Comments> Comment { get; set; }
        public string Comments { get; set; }
        public string UpdatedReason { get; set; }
        public string Slug { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string PictureUrl { get; set; }
        public DetailsBlogViewModel()
        {
            Comment = new List<Comments>();
        }
  
}
}