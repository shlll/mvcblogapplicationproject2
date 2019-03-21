using BlogApplication.Models.Blog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApplication.Models.ViewModels
{
    public class CreateEditCommentViewModel
    {
        [Required(ErrorMessage = "Comment field must be required")]
        [AllowHtml]
        public string Body { get; set; }
        public string UpdatedReason { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public virtual BlogPost Post { get; set; }
        public string PostId { get; set; }
    }
}