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
    public class SeasonModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SeasonModels
        public ActionResult Index()
        {
            return View(db.Seasons.ToList());
        }

        // GET: SeasonModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeasonModel seasonModel = db.Seasons.Find(id);
            if (seasonModel == null)
            {
                return HttpNotFound();
            }
            return View(seasonModel);
        }

        // GET: SeasonModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeasonModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SeasonId,SeasonName")] SeasonModel seasonModel)
        {
            if (ModelState.IsValid)
            {
                db.Seasons.Add(seasonModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seasonModel);
        }

        // GET: SeasonModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeasonModel seasonModel = db.Seasons.Find(id);
            if (seasonModel == null)
            {
                return HttpNotFound();
            }
            return View(seasonModel);
        }

        // POST: SeasonModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeasonId,SeasonName")] SeasonModel seasonModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seasonModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seasonModel);
        }

        // GET: SeasonModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeasonModel seasonModel = db.Seasons.Find(id);
            if (seasonModel == null)
            {
                return HttpNotFound();
            }
            return View(seasonModel);
        }

        // POST: SeasonModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SeasonModel seasonModel = db.Seasons.Find(id);
            db.Seasons.Remove(seasonModel);
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
