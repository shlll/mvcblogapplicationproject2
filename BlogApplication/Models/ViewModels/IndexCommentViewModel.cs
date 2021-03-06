﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApplication.Models.ViewModels
{
    public class IndexCommentViewModel
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedReason { get; set; }
    }
}