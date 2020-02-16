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
    public class LogModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LogModels
        public ActionResult Index()
        {
            var logs = db.Logs.Include(l => l.Season).Include(l => l.Team);
            return View(logs.ToList());
        }

        // GET: LogModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogModel logModel = db.Logs.Find(id);
            if (logModel == null)
            {
                return HttpNotFound();
            }
            return View(logModel);
        }

        // GET: LogModels/Create
        public ActionResult Create()
        {
            ViewBag.SeasonId = new SelectList(db.Seasons, "SeasonId", "SeasonName");
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "TeamName");
            return View();
        }


        public void CreateLog(LogModel NewLog)
        {
            if (ModelState.IsValid)
            {
                db.Logs.Add(NewLog);
                db.SaveChanges();
            }
        }
        public void EditLog(int Team1Id, int Team2Id, int Team1Score, int Team2Score)
        {
            LogModel Team1 = db.Logs.Find(Team1Id);
            LogModel Team2 = db.Logs.Find(Team2Id);

            if (Team1Score > Team2Score)
            {
                Team1.Played++;
                Team1.Win++;
                Team1.GoalsAgainst = Team1.GoalsAgainst + Team2Score;
                Team1.GoalsFor = Team1.GoalsFor + Team1Score;
                Team1.GoalDifference = Team1.GoalsFor - Team1.GoalsAgainst;
                Team1.Points = Team1.Points + 3;

                Team2.Played++;
                Team2.Lost++;
                Team2.GoalsAgainst = Team2.GoalsAgainst + Team1Score;
                Team2.GoalsFor = Team2.GoalsFor + Team2Score;
                Team2.GoalDifference = Team2.GoalsFor - Team2.GoalsAgainst;

                if (ModelState.IsValid)
                {
                    db.Entry(Team1).State = EntityState.Modified;
                    db.Entry(Team2).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            if (Team1Score < Team2Score)
            {
                Team2.Played++;
                Team2.Win++;
                Team2.GoalsAgainst = Team2.GoalsAgainst + Team1Score;
                Team2.GoalsFor = Team2.GoalsFor + Team2Score;
                Team2.GoalDifference = Team2.GoalsFor - Team2.GoalsAgainst;
                Team2.Points = Team2.Points + 3;

                Team1.Played++;
                Team1.Lost++;
                Team1.GoalsAgainst = Team1.GoalsAgainst + Team2Score;
                Team1.GoalsFor = Team1.GoalsFor + Team1Score;
                Team1.GoalDifference = Team1.GoalsFor - Team1.GoalsAgainst;

                if (ModelState.IsValid)
                {
                    db.Entry(Team1).State = EntityState.Modified;
                    db.Entry(Team2).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            if (Team1Score == Team2Score)
            {
                Team1.Played++;
                Team1.Draw++;
                Team1.GoalsAgainst = Team1.GoalsAgainst + Team2Score;
                Team1.GoalsFor = Team1.GoalsFor + Team1Score;
                Team1.GoalDifference = Team1.GoalsFor - Team1.GoalsAgainst;
                Team1.Points = Team1.Points + 1;

                Team2.Played++;
                Team2.Draw++;
                Team2.GoalsAgainst = Team2.GoalsAgainst + Team1Score;
                Team2.GoalsFor = Team2.GoalsFor + Team2Score;
                Team2.GoalDifference = Team2.GoalsFor - Team2.GoalsAgainst;
                Team2.Points++;

                if (ModelState.IsValid)
                {
                    db.Entry(Team1).State = EntityState.Modified;
                    db.Entry(Team2).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }


        // POST: LogModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamId,SeasonId,Played,Win,Draw,Lost,GoalsFor,GoalsAgainst,GoalDifference,Points,LogStanding")] LogModel logModel)
        {
            if (ModelState.IsValid)
            {
                db.Logs.Add(logModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SeasonId = new SelectList(db.Seasons, "SeasonId", "SeasonName", logModel.SeasonId);
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "TeamName", logModel.TeamId);
            return View(logModel);
        }

        // GET: LogModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogModel logModel = db.Logs.Find(id);
            if (logModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.SeasonId = new SelectList(db.Seasons, "SeasonId", "SeasonName", logModel.SeasonId);
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "TeamName", logModel.TeamId);
            return View(logModel);
        }

        // POST: LogModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,SeasonId,Played,Win,Draw,Lost,GoalsFor,GoalsAgainst,GoalDifference,Points,LogStanding")] LogModel logModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SeasonId = new SelectList(db.Seasons, "SeasonId", "SeasonName", logModel.SeasonId);
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "TeamName", logModel.TeamId);
            return View(logModel);
        }

        // GET: LogModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogModel logModel = db.Logs.Find(id);
            if (logModel == null)
            {
                return HttpNotFound();
            }
            return View(logModel);
        }

        // POST: LogModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogModel logModel = db.Logs.Find(id);
            db.Logs.Remove(logModel);
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
