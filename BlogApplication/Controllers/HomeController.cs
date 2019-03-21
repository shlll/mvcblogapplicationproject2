using BlogApplication.Models;
using BlogApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApplication.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext DbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var blog = DbContext.Blogs.Where(p => p.Published == true)
           .Select(p => new IndexHomeViewModel
           {
               Id = p.Id,
               Title = p.Title,
               Body = p.Body,
               PictureUrl = p.PictureUrl,
               DateCreated = p.DateCreated,
               DateUpdated = p.DateUpdated
           }).ToList();
            return View(blog);
        }
        [HttpPost]
        public ActionResult Input(string input)
        {
            var post = DbContext.Blogs.Where(p => p.Published == true && p.Title.Contains(input) || p.Body.Contains(input))
          .Select(p => new IndexHomeViewModel
          {
              Id = p.Id,
              Title = p.Title,
              Body = p.Body,
              PictureUrl = p.PictureUrl,
              DateCreated = p.DateCreated,
              DateUpdated = p.DateUpdated
          }).ToList();
          
            return View("Index", post);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}