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
    [Authorize]
    public class DivisionModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DivisionModels
        public ActionResult Index()
        {
            return View(db.Divisions.ToList());
        }

        // GET: DivisionModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DivisionModel divisionModel = db.Divisions.Find(id);
            if (divisionModel == null)
            {
                return HttpNotFound();
            }
            return View(divisionModel);
        }

        // GET: DivisionModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DivisionModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DivisionId,DivisionName,Description")] DivisionModel divisionModel)
        {
            if (ModelState.IsValid)
            {
                db.Divisions.Add(divisionModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(divisionModel);
        }

        // GET: DivisionModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DivisionModel divisionModel = db.Divisions.Find(id);
            if (divisionModel == null)
            {
                return HttpNotFound();
            }
            return View(divisionModel);
        }

        // POST: DivisionModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DivisionId,DivisionName,Description")] DivisionModel divisionModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(divisionModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(divisionModel);
        }

        // GET: DivisionModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DivisionModel divisionModel = db.Divisions.Find(id);
            if (divisionModel == null)
            {
                return HttpNotFound();
            }
            return View(divisionModel);
        }

        // POST: DivisionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DivisionModel divisionModel = db.Divisions.Find(id);
            db.Divisions.Remove(divisionModel);
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
