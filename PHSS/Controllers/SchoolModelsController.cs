using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PHSS.Models;

namespace PHSS.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SchoolModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SchoolModels
        public ActionResult Index()
        {
            return View(db.Schools.ToList());
        }

        // GET: SchoolModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolModel schoolModel = db.Schools.Find(id);
            if (schoolModel == null)
            {
                return HttpNotFound();
            }
            return View(schoolModel);
        }

        // GET: SchoolModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SchoolId,SchoolName")] SchoolModel schoolModel)
        {
            if (ModelState.IsValid)
            {
                db.Schools.Add(schoolModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schoolModel);
        }

        // GET: SchoolModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolModel schoolModel = db.Schools.Find(id);
            if (schoolModel == null)
            {
                return HttpNotFound();
            }
            return View(schoolModel);
        }

        // POST: SchoolModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SchoolId,SchoolName")] SchoolModel schoolModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schoolModel);
        }

        // GET: SchoolModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolModel schoolModel = db.Schools.Find(id);
            if (schoolModel == null)
            {
                return HttpNotFound();
            }
            return View(schoolModel);
        }

        // POST: SchoolModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolModel schoolModel = db.Schools.Find(id);
            db.Schools.Remove(schoolModel);
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
