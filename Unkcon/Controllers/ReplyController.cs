using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Unkcon.Models;

namespace Unkcon.Controllers
{
    public class ReplyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private UserManager<ApplicationUser> userManager;

        public ReplyController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Reply
        public ActionResult Index(int commentId)
        {
            CommentModel comment = db.Comments.Find(commentId);
            return View(db.Replies.ToList().Where(t => t.Comment == comment));
        }

        // GET: Reply/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReplyModels replyModels = db.Replies.Find(id);
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
        public ActionResult Create([Bind(Include = "ID,Reply")] ReplyModels replyModels, int? commentId)
        {
            if (commentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                CommentModel comment = db.Comments.Find(commentId);
                replyModels.Comment = comment;

                string userId = User.Identity.GetUserId();
                ApplicationUser currentUser = userManager.FindById<ApplicationUser>(userId);

                replyModels.User = currentUser;
                db.Replies.Add(replyModels);
                db.SaveChanges();
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
            ReplyModels replyModels = db.Replies.Find(id);
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
            ReplyModels replyModels = db.Replies.Find(id);
            db.Replies.Remove(replyModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ViewPotentialMatch(int? commentId, int? replyId)
        {
            if (commentId == null || replyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CommentModel commentModel = db.Comments.Find(commentId);
            if (commentModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ReplyModels replyModel = db.Replies.Find(replyId);
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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
