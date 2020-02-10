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
    public class ResultModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ResultModels
        public ActionResult Index()
        {
            var results = db.Results.Include(r => r.Fixtures);
            return View(results.ToList());
        }

        // GET: ResultModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultModel resultModel = db.Results.Find(id);
            if (resultModel == null)
            {
                return HttpNotFound();
            }
            return View(resultModel);
        }

        // GET: ResultModels/Create
        public ActionResult Create()
        {
            ViewBag.FixtureId = new SelectList(db.Fixtures, "FixtureId", "FixtureName");
            return View();
        }

        // POST: ResultModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResultId,FixtureId,Team1Score,Team2Score")] ResultModel resultModel)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(resultModel);

                var Controller = DependencyResolver.Current.GetService<LogModelsController>();
                Controller.ControllerContext = new ControllerContext(this.Request.RequestContext, Controller);

                FixtureModel CurrentFixture = db.Fixtures.Find(resultModel.FixtureId);
                int Team1Id = CurrentFixture.Team1Id;
                int Team2Id = CurrentFixture.Team2Id;

                Controller.EditLog(Team1Id, Team2Id, resultModel.Team1Score, resultModel.Team2Score);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FixtureId = new SelectList(db.Fixtures, "FixtureId", "FixtureName", resultModel.FixtureId);
            return View(resultModel);
        }

        // GET: ResultModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultModel resultModel = db.Results.Find(id);
            if (resultModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.FixtureId = new SelectList(db.Fixtures, "FixtureId", "FixtureName", resultModel.FixtureId);
            return View(resultModel);
        }

        // POST: ResultModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResultId,FixtureId,Team1Score,Team2Score")] ResultModel resultModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resultModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FixtureId = new SelectList(db.Fixtures, "FixtureId", "FixtureName", resultModel.FixtureId);
            return View(resultModel);
        }

        // GET: ResultModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultModel resultModel = db.Results.Find(id);
            if (resultModel == null)
            {
                return HttpNotFound();
            }
            return View(resultModel);
        }

        // POST: ResultModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResultModel resultModel = db.Results.Find(id);
            db.Results.Remove(resultModel);
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
