using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using final1.Models;

namespace final1.Controllers
{
    [Authorize(Roles="admin")]
    public class ImageController : Controller
    {
        private ImageContext db = new ImageContext();

        // GET: /Image/
        public ActionResult Index()
        {
            return View(db.Images.ToList());
        }
        [AllowAnonymous]
        public ActionResult UserIndex()
        {
            return View(db.Images.ToList());
        }
        [AllowAnonymous]
        // GET: /Image/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: /Image/Create
        public ActionResult Create()
        {
            ViewBag.LandmarkId = new SelectList(db.Landmarks, "LandmarkId", "LandmarkName");
            return View();
        }

        // POST: /Image/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ImageId,Name,Annotation,UploadTime,Uploader,landmarks")] Image image)
        {
            image.landmarks = db.Landmarks.Find(new object[] { image.landmarks.LandmarkId });
            if (ModelState.IsValid)
            {
                db.Images.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LandmarkId = new SelectList(db.Landmarks, "LandmarkId", "LandmarkName");
            return View();
        }

        // GET: /Image/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            } 
            ViewBag.LandmarkId = new SelectList(db.Landmarks, "LandmarkId", "LandmarkName");
            return View(image);
        }

        // POST: /Image/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageId,Name,Annotation,UploadTime,Uploader,landmarks")] Image image)
        {
            Image temp = db.Images.Find(new object[] { image.ImageId });
            image.landmarks = db.Landmarks.Find(new object[] { image.landmarks.LandmarkId });

            if (ModelState.IsValid)
            {
                db.Images.Remove(temp);
                db.Images.Add(image);
                //db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LandmarkId = new SelectList(db.Landmarks, "LandmarkId", "LandmarkName");
            return View(image);
        }

        // GET: /Image/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: /Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
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
