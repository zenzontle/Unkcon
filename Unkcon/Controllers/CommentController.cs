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
using Unkcon.DAL;
using Unkcon.Models;

namespace Unkcon.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private UserManagerDecorator _userManager;

        public CommentController()
        {
            _userManager = new UserManagerDecorator(_db);
        }

        // Broser other people's comments
        public async Task<ActionResult> Browse()
        {
            ApplicationUser currentUser = await _userManager.GetCurrentUserAsync(User.Identity.GetUserId());

            if (currentUser == null)
            {
                return View();
            }
            else
            {
                return View(_db.Comments.ToList().Where(c => c.User != currentUser));
            }

        }

        // See own comments
        public async Task<ActionResult> Index()
        {
            ApplicationUser currentUser = await _userManager.GetCurrentUserAsync(User.Identity.GetUserId());

            if (currentUser == null)
            {
                return View();
            }
            else
            {
                return View(_db.Comments.ToList().Where(c => c.User == currentUser));
            }
        }

        // GET: /Comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentmodel = _db.Comments.Find(id);
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
        public async Task<ActionResult> Create([Bind(Include="ID,Comment")] CommentModel commentmodel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = await _userManager.GetCurrentUserAsync(User.Identity.GetUserId());

                commentmodel.User = currentUser;
                _db.Comments.Add(commentmodel);
                _db.SaveChanges();
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
            CommentModel commentmodel = _db.Comments.Find(id);
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
                _db.Entry(commentmodel).State = EntityState.Modified;
                _db.SaveChanges();
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
            CommentModel commentmodel = _db.Comments.Find(id);
            if (commentmodel == null)
            {
                return HttpNotFound();
            }
            return View(commentmodel);
        }

        public FileResult Download(int? id)
        {
            CommentModel commentModel = _db.Comments.Find(id);

            CommentDownloadViewModel commentToDownload = new CommentDownloadViewModel();
            commentToDownload.ID = commentModel.ID;
            commentToDownload.User = commentModel.User.UserName;
            commentToDownload.Comment = commentModel.Comment;

            string commentAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(commentToDownload);
            byte[] commentAsBytes = System.Text.Encoding.UTF8.GetBytes(commentAsJson);
            return File(commentAsBytes, System.Net.Mime.MediaTypeNames.Text.Plain, String.Format("{0}.{1}", commentModel.ID.ToString(), "json"));
        }

        // POST: /Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentModel commentModel = _db.Comments.Find(id);
            _db.Comments.Remove(commentModel);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
