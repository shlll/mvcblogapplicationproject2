using BlogApplication.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApplication.Models.Comment
{
    public class Comments
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public virtual BlogPost Post { get; set; }
        public string PostId { get; set; }
        public string UpdatedReason { get; set; }
        public Comments()
        {
            DateCreated = DateTime.Now;
        }

    }
}