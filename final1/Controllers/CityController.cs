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
    public class CityController : Controller
    {
        private ImageContext db = new ImageContext();

        // GET: /City/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.CityImages.ToList());
        }

        // GET: /City/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityImage cityimage = db.CityImages.Find(id);
            if (cityimage == null)
            {
                return HttpNotFound();
            }
            return View(cityimage);
        }

        // GET: /City/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,Annotation,UploadTime,Uploader")] CityImage cityimage)
        {
            if (ModelState.IsValid)
            {
                db.CityImages.Add(cityimage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cityimage);
        }

        // GET: /City/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityImage cityimage = db.CityImages.Find(id);
            if (cityimage == null)
            {
                return HttpNotFound();
            }
            return View(cityimage);
        }

        // POST: /City/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,Annotation,UploadTime,Uploader")] CityImage cityimage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cityimage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cityimage);
        }

        // GET: /City/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityImage cityimage = db.CityImages.Find(id);
            if (cityimage == null)
            {
                return HttpNotFound();
            }
            return View(cityimage);
        }

        // POST: /City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CityImage cityimage = db.CityImages.Find(id);
            db.CityImages.Remove(cityimage);
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
