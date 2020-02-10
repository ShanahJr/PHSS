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
    public class PlayerModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlayerModels
        public ActionResult Index()
        {
            return View(db.Players.ToList());
        }

        // GET: PlayerModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerModel playerModel = db.Players.Find(id);
            if (playerModel == null)
            {
                return HttpNotFound();
            }
            return View(playerModel);
        }

        // GET: PlayerModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlayerModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerId,FirstName,LastName,ForeNames,Position,DOB")] PlayerModel playerModel)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(playerModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(playerModel);
        }

        // GET: PlayerModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerModel playerModel = db.Players.Find(id);
            if (playerModel == null)
            {
                return HttpNotFound();
            }
            return View(playerModel);
        }

        // POST: PlayerModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerId,FirstName,LastName,ForeNames,Position,DOB")] PlayerModel playerModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playerModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playerModel);
        }

        // GET: PlayerModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerModel playerModel = db.Players.Find(id);
            if (playerModel == null)
            {
                return HttpNotFound();
            }
            return View(playerModel);
        }

        // POST: PlayerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlayerModel playerModel = db.Players.Find(id);
            db.Players.Remove(playerModel);
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
