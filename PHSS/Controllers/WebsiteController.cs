using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHSS.Models;
using PHSS.ViewModels;
using Newtonsoft.Json;

namespace PHSS.Controllers
{
    [AllowAnonymous]
    public class WebsiteController : Controller
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        private static int MainDivisionId; //Stores division change
        private static int MainAgeGroupId; // Stores age group change

        // GET: Website
        public ActionResult Index()
        {
            //Required list of objects for the index page, these are to be added to the
            //index view model
            

            int DivisionId = db.Divisions.FirstOrDefault().DivisionId;
            int AgeGroupId = db.AgeGroup.FirstOrDefault().AgeGroupID;

            List<FixtureModel> Fixtures = db.Fixtures.Where(f => f.Team1.DivisionId == DivisionId && f.Team1.AgeGroupId == AgeGroupId && f.Executed == false || f.Matchdate < DateTime.Now).ToList();
            List<ResultModel> Results = db.Results.Where(r => r.Fixtures.Team1.DivisionId == DivisionId && r.Fixtures.Team1.AgeGroupId == AgeGroupId && r.Fixtures.Executed == false || r.Fixtures.Matchdate > DateTime.Now).ToList();
            List<LogModel> Logs = db.Logs.Where(l => l.Team.DivisionId == DivisionId && l.Team.AgeGroupId == AgeGroupId).ToList();

            //Sorting the logs according to league standards
            SortLogs(Logs);

            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName");
            ViewBag.AgeGroupId = new SelectList(db.AgeGroup, "AgeGroupId", "AgeGroupName");

            WebsiteIndexViewModel IndexPage = new WebsiteIndexViewModel(Fixtures, Results, Logs);

            return View(IndexPage);
        }

        public ActionResult Logs()
        {

            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName");
            ViewBag.AgeGroupId = new SelectList(db.AgeGroup, "AgeGroupId", "AgeGroupName");

            int AgeGroupId, DivisionId;

            //If a division has not been selected when the view all logs link has been clicked on, then use the first division in the table
            if (MainDivisionId == 0)
            {
                DivisionId = db.Divisions.FirstOrDefault().DivisionId;
            }
            else
            {
                DivisionId = MainDivisionId;
            }

            //If a division has not been selected when the view all logs link has been clicked on, then use the first division in the table
            if (MainAgeGroupId == 0)
            {
                AgeGroupId = db.AgeGroup.FirstOrDefault().AgeGroupID;
            }
            else
            {
                AgeGroupId = MainAgeGroupId;
            }

            List<LogModel> Logs = db.Logs.Where(l => l.Team.DivisionId == DivisionId && l.Team.AgeGroupId == AgeGroupId).ToList();

            return View(Logs);
        }

        [HttpGet]
        public ActionResult Fixtures()
        {
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName");
            ViewBag.AgeGroupId = new SelectList(db.AgeGroup, "AgeGroupId", "AgeGroupName");

            int AgeGroupId, DivisionId;

            //If a division has not been selected when the view all logs link has been clicked on, then use the first division in the table
            if (MainDivisionId == 0)
            {
                DivisionId = db.Divisions.FirstOrDefault().DivisionId;
            }
            else
            {
                DivisionId = MainDivisionId;
            }

            //If a division has not been selected when the view all logs link has been clicked on, then use the first division in the table
            if (MainAgeGroupId == 0)
            {
                AgeGroupId = db.AgeGroup.FirstOrDefault().AgeGroupID;
            }
            else
            {
                AgeGroupId = MainAgeGroupId;
            }

            List<FixtureModel> Fixtures = db.Fixtures.Where(f => f.Team1.DivisionId == DivisionId && f.Team1.AgeGroupId == AgeGroupId && f.Executed == false || f.Matchdate > DateTime.Now).ToList();

            return View(Fixtures);
        }


        // results page
        public ActionResult Results()
        {
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "DivisionName");
            ViewBag.AgeGroupId = new SelectList(db.AgeGroup, "AgeGroupId", "AgeGroupName");

            int AgeGroupId, DivisionId;

            //If a division has not been selected when the view all logs link has been clicked on, then use the first division in the table
            if (MainDivisionId == 0)
            {
                DivisionId = db.Divisions.FirstOrDefault().DivisionId;
            }
            else
            {
                DivisionId = MainDivisionId;
            }

            //If a division has not been selected when the view all logs link has been clicked on, then use the first division in the table
            if (MainAgeGroupId == 0)
            {
                AgeGroupId = db.AgeGroup.FirstOrDefault().AgeGroupID;
            }
            else
            {
                AgeGroupId = MainAgeGroupId;
            }

            List<ResultModel> Results = db.Results.Where(r => r.Fixtures.Team1.DivisionId == DivisionId && r.Fixtures.Team1.AgeGroupId == AgeGroupId && r.Fixtures.Executed == false || r.Fixtures.Matchdate > DateTime.Now).ToList();

            return View(Results);
        }

        // Divison Selection Change
        public JsonResult Division(int id)
        {

            int AgeGroupId;

            if (MainAgeGroupId == 0)
            {
                AgeGroupId = db.AgeGroup.FirstOrDefault().AgeGroupID;
            }
            else
            {
                AgeGroupId = MainAgeGroupId;
            }


            MainDivisionId = id;

            

            List<FixtureModel> Fixtures = db.Fixtures.Where(f => f.Team1.Division.DivisionId == id && f.Team1.AgeGroup.AgeGroupID == AgeGroupId).ToList();
            List<ResultModel> Results = db.Results.Where(r => r.Fixtures.Team1.DivisionId == id && r.Fixtures.Team1.AgeGroupId == AgeGroupId).ToList();
            List<LogModel> Logs = db.Logs.Where(l => l.Team.DivisionId == id && l.Team.AgeGroupId == AgeGroupId).ToList();
            SortLogs(Logs);
           // List<TeamModel> Logs = db.Teams.Where(t => t.DivisionId == id && t.AgeGroupId == AgeGroupId).ToList();

            ViewModels.WebsiteIndexViewModel IndexPage = new WebsiteIndexViewModel(Fixtures, Results, Logs);

            //var result = JsonConvert.SerializeObject(IndexPage.Fixtures, Formatting.Indented,
            //    new JsonSerializerSettings
            //    {
            //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //    });

            //result = result + JsonConvert.SerializeObject(IndexPage.Logs, Formatting.Indented,
            //    new JsonSerializerSettings
            //    {
            //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //    });

            //result = result + JsonConvert.SerializeObject(IndexPage.Results, Formatting.Indented,
            //    new JsonSerializerSettings
            //    {
            //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //    });

            return Json(IndexPage, JsonRequestBehavior.AllowGet);

        }

        //Age group selection chnage
        public JsonResult AgeGroup(int id)
        {

            if (MainDivisionId == 0)
            {
                MainDivisionId = db.Divisions.FirstOrDefault().DivisionId;
            }

            MainAgeGroupId = id;

            List<FixtureModel> Fixtures = db.Fixtures.Where(f => f.Team1.Division.DivisionId == MainDivisionId && f.Team1.AgeGroup.AgeGroupID == id).ToList();
            List<ResultModel> Results = db.Results.Where(r => r.Fixtures.Team1.DivisionId == MainDivisionId && r.Fixtures.Team1.AgeGroupId == id).ToList();
            List<LogModel> Logs = db.Logs.Where(l => l.Team.DivisionId == MainDivisionId && l.Team.AgeGroupId == id).ToList();

            SortLogs(Logs);

            WebsiteIndexViewModel IndexPage = new WebsiteIndexViewModel(Fixtures, Results, Logs);

            return Json(IndexPage, JsonRequestBehavior.AllowGet);
        }

        //User Defined Function
        //Sorts the logs according to Points and Goal Difference
        public void SortLogs(List<LogModel> Logs)
        {

            //int Temp;
            LogModel Temp;

            //Sorting according to points
            for (int i = 0; i < Logs.Count() - 1; i++)
            {
                for (int k = i + 1; k < Logs.Count(); k++)
                {
                    if (Logs[i].Points < Logs[k].Points)
                    {

                        Temp = Logs[k];
                        Logs[k] = Logs[i];
                        Logs[i] = Temp;

                        //Temp = Logs[k].LogStanding;
                        //Logs[k].LogStanding = Logs[i].LogStanding;
                        //Logs[i].LogStanding = Temp;

                        //if (ModelState.IsValid)
                        //{
                        //    db.Entry(Logs[i]).State = EntityState.Modified;
                        //    db.Entry(Logs[k]).State = EntityState.Modified;
                        //    db.SaveChanges();
                        //}
                    }
                }
            }

            //if points are the same then sort according to goal difference

            for (int i = 0; i < Logs.Count() - 1; i++)
            {
                for (int k = i + 1; k < Logs.Count(); k++)
                {
                    if (Logs[i].Points == Logs[k].Points)
                    {
                        if (Logs[i].GoalDifference < Logs[k].GoalDifference)
                        {
                            Temp = Logs[k];
                            Logs[k] = Logs[i];
                            Logs[i] = Temp;

                            //Temp = Logs[i + 1].LogStanding;
                            //Logs[i + 1].LogStanding = Logs[i].LogStanding;
                            //Logs[i].LogStanding = Temp;
                        }

                        //if (ModelState.IsValid)
                        //{
                        //    db.Entry(Logs[i]).State = EntityState.Modified;
                        //    db.Entry(Logs[i + 1]).State = EntityState.Modified;
                        //    db.SaveChanges();
                        //}
                    }
                }

            }
        }// End of SortLogs
    }
}