﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Unkcon.Models;
using Unkcon.DAL;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Unkcon.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private UserManager<ApplicationUser> userManager;

        public CommentController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: /Comment/
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        // GET: /Comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentmodel = db.Comments.Find(id);
            if (commentmodel == null)
            {
                return HttpNotFound();
            }
            return View(commentmodel);
        }

        // GET: /Comment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Comment")] CommentModel commentmodel)
        {
            if (ModelState.IsValid)
            {
                // Get current logged in user. Could be made async.
                string userId = User.Identity.GetUserId();
                ApplicationUser currentUser = userManager.FindById<ApplicationUser>(userId);

                commentmodel.User = currentUser;
                db.Comments.Add(commentmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commentmodel);
        }

        // GET: /Comment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentmodel = db.Comments.Find(id);
            if (commentmodel == null)
            {
                return HttpNotFound();
            }
            return View(commentmodel);
        }

        // POST: /Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Comment")] CommentModel commentmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commentmodel);
        }

        // GET: /Comment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentmodel = db.Comments.Find(id);
            if (commentmodel == null)
            {
                return HttpNotFound();
            }
            return View(commentmodel);
        }

        // POST: /Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentModel commentmodel = db.Comments.Find(id);
            db.Comments.Remove(commentmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
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
