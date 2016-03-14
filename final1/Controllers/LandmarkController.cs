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
    public class LandmarkController : Controller
    {
        private ImageContext db = new ImageContext();

        // GET: /Landmark/
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.Landmarks.ToList());
        }
        public ActionResult GoogleMap()
        {
            return View();
        }
        public ActionResult Catalog()
        {
            ((List<Landmark>)Session["ShoppingCart"]).Clear();
            return View(db.Landmarks.ToList());
        }

        // GET: /AddToCart

        public ActionResult AddToCart(int id = 0)
        {
            Landmark landmark = db.Landmarks.Find(id);
            if (landmark == null)
            {
                return HttpNotFound();
            }
            ((List<Landmark>)(HttpContext.Session["ShoppingCart"])).Add(landmark);
            //if (((List<Landmark>)(HttpContext.Session["ShoppingCart"])).Count() == 2)
            //{
            //    return View("CheckOut");
            //}
            //return RedirectToAction("Catalog");
            return View("Catalog", db.Landmarks.ToList());
        }

        public ActionResult CheckOut()
        {
            return View();
        }
        // GET: /Landmark/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Landmark landmark = db.Landmarks.Find(id);
            if (landmark == null)
            {
                return HttpNotFound();
            }
            return View(landmark);
        }

        // GET: /Landmark/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Landmark/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include="LandmarkId,LandmarkName,EditTime,Editor,Description,Direction")] Landmark landmark)
        {
            if (ModelState.IsValid)
            {
                db.Landmarks.Add(landmark);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(landmark);
        }

        // GET: /Landmark/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Landmark landmark = db.Landmarks.Find(id);
            if (landmark == null)
            {
                return HttpNotFound();
            }
            return View(landmark);
        }

        // POST: /Landmark/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include="LandmarkId,LandmarkName,EditTime,Editor,Description,Direction")] Landmark landmark)
        {
            if (ModelState.IsValid)
            {
                db.Entry(landmark).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(landmark);
        }
        public ActionResult Join(int? id)
        {
            //Landmark landmark = db.Landmarks.Find(id);
            var query =
                from imag in db.Images
                where imag.landmarks.LandmarkId == id
                join land in db.Landmarks on imag.landmarks.LandmarkId equals land.LandmarkId
                select imag;
            ViewBag.ImagesOfLandmark = query.ToList();
            return View();
        }
        // GET: /Landmark/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Landmark landmark = db.Landmarks.Find(id);
            if (landmark == null)
            {
                return HttpNotFound();
            }
            return View(landmark);
        }

        // POST: /Landmark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Landmark landmark = db.Landmarks.Find(id);
            db.Landmarks.Remove(landmark);
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
