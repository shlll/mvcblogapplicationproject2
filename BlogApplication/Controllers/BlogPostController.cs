using BlogApplication.Models;
using BlogApplication.Models.Blog;
using BlogApplication.Models.Comment;
using BlogApplication.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApplication.Controllers
{
    public class BlogPostController : Controller
    {
        private ApplicationDbContext DbContext = new ApplicationDbContext();
        // GET: BlogPost
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var blog = DbContext.Blogs.Where(p => p.UserId == userId)
                .Select(p => new IndexBlogViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Body = p.Body,
                    Published = p.Published,
                    DateCreated = p.DateCreated,
                    DateUpdated = p.DateUpdated,
                    Slug = p.Slug,
                }).ToList();
            return View(blog);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(CreateEditBlogViewModel data)
        {
            return SaveBlogPost(null, data);
        }
        public ActionResult SaveBlogPost(int? id, CreateEditBlogViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var userId = User.Identity.GetUserId();
            string fileExtension;
            if (data.Picture != null)
            {
                fileExtension = Path.GetExtension(data.Picture.FileName);
                
                if (!Constants.AllowedFileExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("", "File extension is not allowed.");
                    return View();
                }
            }
            BlogPost blogPost;
            if (!id.HasValue)
            {
                blogPost = new BlogPost();
                blogPost.UserId = userId;
                DbContext.Blogs.Add(blogPost);
            }
            else
            {
                blogPost = DbContext.Blogs.FirstOrDefault(p => p.Id == id);
                if (blogPost == null)
                {
                    return RedirectToAction(nameof(BlogPostController.Index));
                }
            }
            blogPost.Title = data.BlogTitle;
            blogPost.Body = data.Body;
            blogPost.Slug = data.Slug;
            blogPost.Published = data.Published;
            data.DateCreated = DateTime.Now;
            if (data.Picture != null)
            {
                if (!Directory.Exists(Constants.MappedUploadFolder))
                {
                    Directory.CreateDirectory(Constants.MappedUploadFolder);
                }
                var fileName = data.Picture.FileName;
                var fullPathWithName = Constants.MappedUploadFolder + fileName;
                data.Picture.SaveAs(fullPathWithName);
                blogPost.PictureUrl = Constants.UploadFolder + fileName;
            }
            DbContext.SaveChanges();
            return RedirectToAction(nameof(BlogPostController.Index));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(BlogPostController.Index));
            }
            var userId = User.Identity.GetUserId();
            var blogModel = DbContext.Blogs.FirstOrDefault(
                p => p.Id == id && p.UserId == userId);
            if (blogModel == null)
            {
                return RedirectToAction(nameof(BlogPostController.Index));
            }
            var model = new CreateEditBlogViewModel();
            model.BlogTitle = blogModel.Title;
            model.Body = blogModel.Body;
            model.Slug = blogModel.Slug;
            model.Published = blogModel.Published;
            blogModel.DateUpdated = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, CreateEditBlogViewModel data)
        {
            return SaveBlogPost(id, data);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(BlogPostController.Index));
            }
            var userId = User.Identity.GetUserId();
            var blogModel = DbContext.Blogs.FirstOrDefault(p => p.Id == id && p.UserId == userId);
            if (blogModel != null)
            {
                DbContext.Blogs.Remove(blogModel);
                DbContext.SaveChanges();
            }
            return RedirectToAction(nameof(BlogPostController.Index));
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(BlogPostController.Index));
            }
            var blogPost = DbContext.Blogs.FirstOrDefault(P => P.Id == id.Value);
            if(blogPost == null)
            {
                return RedirectToAction(nameof(BlogPostController.Index));
            }
            var post = new DetailsBlogViewModel();
            post.Title = blogPost.Title;
            post.Body = blogPost.Body;
            post.Published = blogPost.Published;
            post.DateCreated = blogPost.DateCreated;
            post.DateUpdated = blogPost.DateUpdated;
            post.PictureUrl = blogPost.PictureUrl;
            return View(post);
        }
        [HttpGet]
        [Authorize]
        public ActionResult CreateComment()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult CreateComment(DetailsBlogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var newComment = new Comments();
            newComment.UserId = User.Identity.GetUserId();
            newComment.DateCreated = DateTime.Now;
            newComment.Body = model.Body;
            DbContext.BlogComments.Add(newComment);
            DbContext.SaveChanges();
            return RedirectToAction(nameof(BlogPostController.Details));
        }
    }
}