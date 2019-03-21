using BlogApplication.Models;
using BlogApplication.Models.Comment;
using BlogApplication.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApplication.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext DbContext = new ApplicationDbContext();
        // GET: Comment
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var comment = DbContext.BlogComments
                .Where(p => p.UserId == userId)
                .Select(p => new IndexCommentViewModel
                {
                    Id = p.Id,
                    Body = p.Body,
                    DateCreated = p.DateCreated,
                    DateUpdated = p.DateUpdated,
                    UpdatedReason = p.UpdatedReason
                }).ToList();
            return View(comment);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Create(CreateEditCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var userId = User.Identity.GetUserId();
            var comments = new Comments();
            comments.UserId = userId;
            comments.Body = model.Body;
            DbContext.BlogComments.Add(comments);
            DbContext.SaveChanges();
            return RedirectToAction(nameof(CommentController.Index));
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            var comments = DbContext.BlogComments.FirstOrDefault(p => p.Id == id.Value);
            if (comments == null)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            var newComment = new CreateEditCommentViewModel();
            newComment.Body = comments.Body;
            newComment.UpdatedReason = comments.UpdatedReason;
            return View(newComment);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Edit(int? id,CreateEditCommentViewModel model)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            var comment = DbContext.BlogComments.FirstOrDefault(p => p.Id == id.Value);
            comment.Body = model.Body;
            comment.UpdatedReason = model.UpdatedReason;
            comment.DateUpdated = DateTime.Now;
            DbContext.SaveChanges();
            return RedirectToAction(nameof(CommentController.Index));
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            var comment = DbContext.BlogComments.FirstOrDefault(p => p.Id == id.Value);
            if (comment == null)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            DbContext.BlogComments.Remove(comment);
            DbContext.SaveChanges();
            return RedirectToAction(nameof(CommentController.Index));
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            var comment = DbContext.BlogComments.FirstOrDefault(p => p.Id == id.Value);
            if (comment == null)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            var newComment = new DetailsCommentViewModel();
            newComment.Body = comment.Body;
            newComment.DateCreated = comment.DateCreated;
            newComment.DateUpdated = comment.DateUpdated;
            newComment.UpdatedReason = comment.UpdatedReason;
            return View(newComment);
        }
    }
}