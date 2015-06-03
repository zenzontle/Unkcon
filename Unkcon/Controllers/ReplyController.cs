using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Unkcon.Authentication;
using Unkcon.Models;

namespace Unkcon.Controllers
{
    public class ReplyController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private UserManagerDecorator _userManager;

        public ReplyController()
        {
            _userManager = new UserManagerDecorator(_db);
        }

        // GET: Reply
        public ActionResult Index(int commentId)
        {
            CommentModel comment = _db.Comments.Find(commentId);
            return View(_db.Replies.ToList().Where(t => t.Comment == comment));
        }

        // GET: Reply/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReplyModels replyModels = _db.Replies.Find(id);
            if (replyModels == null)
            {
                return HttpNotFound();
            }
            return View(replyModels);
        }

        // GET: Reply/Create
        public ActionResult Create(int? commentId)
        {
            if (commentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: Reply/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Reply")] ReplyModels replyModels, int? commentId)
        {
            if (commentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                CommentModel comment = _db.Comments.Find(commentId);
                replyModels.Comment = comment;

                ApplicationUser currentUser = await _userManager.GetCurrentUserAsync(User.Identity.GetUserId());

                replyModels.User = currentUser;
                _db.Replies.Add(replyModels);
                _db.SaveChanges();
                return RedirectToAction("ViewPotentialMatch", new { commentId = commentId, replyId = replyModels.ID });
            }

            return View(replyModels);
        }

        //NOTE Commented out while I figure out how to handle Edits
        //// GET: Reply/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ReplyModels replyModels = db.Replies.Find(id);
        //    if (replyModels == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(replyModels);
        //}

        //// POST: Reply/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Reply")] ReplyModels replyModels)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(replyModels).State = EntityState.Modified;
        //        db.SaveChanges();

        //        return View(replyModels);
        //    }
        //    return View(replyModels);
        //}

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReplyModels replyModels = _db.Replies.Find(id);
            if (replyModels == null)
            {
                return HttpNotFound();
            }
            return View(replyModels);
        }

        // POST: Reply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReplyModels replyModels = _db.Replies.Find(id);

            // Get comment ID of reply before deleting it so we can redirect to it.
            int commentId = replyModels.Comment.ID;
            
            _db.Replies.Remove(replyModels);
            _db.SaveChanges();
            return RedirectToAction("Index", new { commentId = commentId });
        }

        public ActionResult ViewPotentialMatch(int? commentId, int? replyId)
        {
            if (commentId == null || replyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CommentModel commentModel = _db.Comments.Find(commentId);
            if (commentModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ReplyModels replyModel = _db.Replies.Find(replyId);
            if (replyModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PotentialMatchViewModel potentialMatchViewModel = new PotentialMatchViewModel();
            potentialMatchViewModel.Comment = commentModel.Comment;
            potentialMatchViewModel.Reply = replyModel.Reply;

            return View("PotentialMatch", potentialMatchViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
