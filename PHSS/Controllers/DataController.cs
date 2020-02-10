using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHSS.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index(string School = "", string Under = "", string Team = "", string Result = "", string Division = "", string Fixture = "", string Log = "", string Season = "", string Location = "")
        {
            if (School != "")
            {
                return RedirectToAction("Index", "SchoolModels");
            }

            if (Under != "")
            {
                return RedirectToAction("Index", "AgeGroupModels");
            }

            if (Team != "")
            {
                return RedirectToAction("Index", "TeamModels");
            }

            if (Result != "")
            {
                return RedirectToAction("Index", "ResultModels");
            }

            if (Division != "")
            {
                return RedirectToAction("Index", "DivisionModels");
            }

            if (Fixture != "")
            {
                return RedirectToAction("Index", "FixtureModels");
            }

            if (Log != "")
            {
                return RedirectToAction("Index", "LogModels");
            }

            if (Season != "")
            {
                return RedirectToAction("Index", "SeasonModels");
            }

            if (Location != "")
            {
                return RedirectToAction("Index", "LocationModels");
            }

            return View();
        }
    }
}