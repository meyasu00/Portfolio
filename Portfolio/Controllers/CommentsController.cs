using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;
using Microsoft.AspNet.Identity;

namespace Portfolio.Controllers
{
    [RequireHttps]
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Comments - All Comments
        [Authorize(Roles = "Admin, Moderator, GuestAdmin")]
        public ActionResult Index()
        {
            return View(db.Comments.OrderBy(c => c.PostId).ToList());
        }

        // POST: Comments/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, PostId, AuthorId, Body, Created")] Comment comment)
        {
            var slug = db.Posts.Find(comment.PostId).Slug;

            if (ModelState.IsValid)
            {
                comment.created = DateTimeOffset.Now;
                comment.AuthorId = User.Identity.GetUserId();
                comment.OriginalBody = comment.Body;
                comment.Published = true;

                db.Comments.Add(comment);
                db.SaveChanges();

                return RedirectToAction("Details", "BlogPosts", new { slug = slug });
            }
            return RedirectToAction("Details", "BlogPosts", new { slug = slug });
        }

        //GET: Comments/Edit/5
        [Authorize(Roles = "Admin, Moderator, GuestAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult
                    (HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            TempData["prevComment"] = comment.Body; //allows communication between controllers
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //POST: Comments/Edit/5
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, PostId, AuthorId, Body, Created, Modified, ModifiedBy, OriginalBody, PreviousBody, Published, ReasonRemoved")]Comment comment, string Submit)
        {
            var slug = db.Posts.Find(comment.PostId).Slug;
            var previous = TempData["prevComment"].ToString();
            //cannot have two instances of comment.Property, so must use TempData (hack version) to access older variables (see GET Edit)

            if (ModelState.IsValid)
            {
                comment.Modified = DateTimeOffset.Now;
                comment.ModifiedBy = User.Identity.GetUserName();
                comment.PreviousBody = previous;

                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                if (Submit == "Save and go to moderator page")
                {
                    return RedirectToAction("Index");
                }
                else if (Submit == "Save and go to Blogpost")
                {
                    return RedirectToAction("Details", "BlogPosts", new { slug = slug });
                }
                //could also use switch statement
            }
            return View(comment);
        }

        //GET: Comments/Delete/5
        [Authorize(Roles = "Admin, Moderator, GuestAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //POST: Comments/Delete/5
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            var slug = db.Posts.Find(comment.PostId).Slug;

            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "BlogPosts", new { slug = slug });
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
