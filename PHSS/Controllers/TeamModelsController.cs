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
    public class TeamModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TeamModels
        public ActionResult Index()
        {
            var teams = db.Teams.Include(t => t.AgeGroup).Include(t => t.Division).Include(t => t.School);
            return View(teams.ToList());
        }

        // GET: TeamModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamModel teamModel = db.Teams.Find(id);
            if (teamModel == null)
            {
                return HttpNotFound();
            }
            return View(teamModel);
        }

        // GET: TeamModels/Create
        public ActionResult Create()
        {
            ViewBag.AgeGroupId = new SelectList(db.AgeGroup, "AgeGroupID", "AgeGroupName");
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName");
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName");
            return View();
        }

        // POST: TeamModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamId,TeamName,SchoolId,DivisionId,AgeGroupId")] TeamModel teamModel)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(teamModel);
                db.SaveChanges();

                //Everytime a new team is created, a new log should automatically be created for that team
                LogModel NewLog = new LogModel();

                var Controller = DependencyResolver.Current.GetService<LogModelsController>();
                Controller.ControllerContext = new ControllerContext(this.Request.RequestContext, Controller);

                NewLog.Draw = 0;
                NewLog.GoalDifference = 0;
                NewLog.GoalsAgainst = 0;
                NewLog.GoalsFor = 0;
                NewLog.Lost = 0;
                NewLog.Played = 0;
                NewLog.Points = 0;
                NewLog.Win = 0;
                NewLog.SeasonId = db.Seasons.ToList().Last().SeasonId;
                NewLog.TeamId = db.Teams.ToList().Last().TeamId;

                Controller.CreateLog(NewLog);

                return RedirectToAction("Index");
            }

            ViewBag.AgeGroupId = new SelectList(db.AgeGroup, "AgeGroupID", "AgeGroupName", teamModel.AgeGroupId);
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName", teamModel.DivisionId);
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName", teamModel.SchoolId);
            return View(teamModel);
        }

        // GET: TeamModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamModel teamModel = db.Teams.Find(id);
            if (teamModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgeGroupId = new SelectList(db.AgeGroup, "AgeGroupID", "AgeGroupName", teamModel.AgeGroupId);
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName", teamModel.DivisionId);
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName", teamModel.SchoolId);
            return View(teamModel);
        }

        // POST: TeamModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,TeamName,SchoolId,DivisionId,AgeGroupId")] TeamModel teamModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgeGroupId = new SelectList(db.AgeGroup, "AgeGroupID", "AgeGroupName", teamModel.AgeGroupId);
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName", teamModel.DivisionId);
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName", teamModel.SchoolId);
            return View(teamModel);
        }

        // GET: TeamModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamModel teamModel = db.Teams.Find(id);
            if (teamModel == null)
            {
                return HttpNotFound();
            }
            return View(teamModel);
        }

        // POST: TeamModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamModel teamModel = db.Teams.Find(id);
            db.Teams.Remove(teamModel);
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
