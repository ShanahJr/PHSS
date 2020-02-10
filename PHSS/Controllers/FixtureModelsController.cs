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
    public class FixtureModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Stores the selected Divison and Age Group from the fixture create page
        public static int MainDivisionId, MainAgeGroupId;

        // GET: FixtureModels
        public ActionResult Index()
        {
            var fixtures = db.Fixtures.Include(f => f.Team1).Include(f => f.Team2);
            return View(fixtures.ToList());
        }

        // GET: FixtureModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixtureModel fixtureModel = db.Fixtures.Find(id);
            if (fixtureModel == null)
            {
                return HttpNotFound();
            }
            return View(fixtureModel);
        }

        // GET: FixtureModels/Create
        public ActionResult Create()
        {
            int DivisionId, AgeGroupId;

            if (MainDivisionId == 0)
            {
                DivisionId = db.Divisions.FirstOrDefault().DivisionId;
            }
            else
            {
                DivisionId = MainDivisionId;
            }

            if (MainAgeGroupId == 0)
            {
                AgeGroupId = db.AgeGroup.FirstOrDefault().AgeGroupID;
            }
            else
            {
                AgeGroupId = MainAgeGroupId;
            }

            //These two viewbags are for the selection of the desired Division and Age group when setting up fixtures
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName");
            ViewBag.UnderId = new SelectList(db.AgeGroup, "AgeGroupId", "AgeGroupName");

            //The list of teams that the user can choose will be based on the division and AgeGroup's selected
            ViewBag.TeamModel1Id = new SelectList(db.Teams.Where(t => t.DivisionId == DivisionId && t.AgeGroupId == AgeGroupId), "Team1", "TeamName");
            ViewBag.TeamModel2Id = new SelectList(db.Teams.Where(t => t.DivisionId == DivisionId && t.AgeGroupId == AgeGroupId), "Team2", "TeamName");
            return View();
        }

        //Ajax call when Division is changed
        public JsonResult DivisionSelect(int DivisionId)
        {
            if (DivisionId != 0)
            {
                MainDivisionId = DivisionId;
            }

            if (MainAgeGroupId == 0)
            {
                MainAgeGroupId = db.AgeGroup.FirstOrDefault().AgeGroupID;
            }

            List<TeamModel> CurrentTeams = db.Teams.Where(t => t.DivisionId == MainDivisionId && t.AgeGroupId == MainAgeGroupId).ToList();

            return Json(CurrentTeams, JsonRequestBehavior.AllowGet);
        }

        //Ajax call when Age Group is changed
        public JsonResult AgeGroupSelect(int AgeGroupId)
        {
            if (AgeGroupId != 0)
            {
                MainAgeGroupId = AgeGroupId;
            }

            if (MainDivisionId == 0)
            {
                MainDivisionId = db.Divisions.FirstOrDefault().DivisionId;
            }

            List<TeamModel> CurrentTeams = db.Teams.Where(t => t.DivisionId == MainDivisionId && t.AgeGroupId == MainAgeGroupId).ToList();

            return Json(CurrentTeams, JsonRequestBehavior.AllowGet);
        }

        // POST: FixtureModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FixtureId,FixtureName,Team1Id,Team2Id,Matchdate,Executed")] FixtureModel fixtureModel)
        {
            if (ModelState.IsValid)
            {
                db.Fixtures.Add(fixtureModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Team1Id = new SelectList(db.Teams, "TeamId", "TeamName", fixtureModel.Team1Id);
            ViewBag.Team2Id = new SelectList(db.Teams, "TeamId", "TeamName", fixtureModel.Team2Id);
            return View(fixtureModel);
        }

        // GET: FixtureModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixtureModel fixtureModel = db.Fixtures.Find(id);
            if (fixtureModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Team1Id = new SelectList(db.Teams, "TeamId", "TeamName", fixtureModel.Team1Id);
            ViewBag.Team2Id = new SelectList(db.Teams, "TeamId", "TeamName", fixtureModel.Team2Id);
            return View(fixtureModel);
        }

        // POST: FixtureModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FixtureId,FixtureName,Team1Id,Team2Id,Matchdate,Executed")] FixtureModel fixtureModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fixtureModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Team1Id = new SelectList(db.Teams, "TeamId", "TeamName", fixtureModel.Team1Id);
            ViewBag.Team2Id = new SelectList(db.Teams, "TeamId", "TeamName", fixtureModel.Team2Id);
            return View(fixtureModel);
        }

        // GET: FixtureModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixtureModel fixtureModel = db.Fixtures.Find(id);
            if (fixtureModel == null)
            {
                return HttpNotFound();
            }
            return View(fixtureModel);
        }

        // POST: FixtureModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FixtureModel fixtureModel = db.Fixtures.Find(id);
            db.Fixtures.Remove(fixtureModel);
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
