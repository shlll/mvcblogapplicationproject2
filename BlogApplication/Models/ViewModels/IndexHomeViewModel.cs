using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApplication.Models.ViewModels
{
    public class IndexHomeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string PictureUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}