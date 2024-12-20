using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectElec.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Homepage()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Accounts()
        {
            return View();
        }
        public ActionResult Logs()
        {
            return View();
        }
        public ActionResult Candidates()
        {
            return View();
        }
        public ActionResult Parties()
        {
            return View();
        }
        public ActionResult Positions()
        {
            return View();
        }
        public ActionResult Tallies()
        {
            return View();
        }
        public ActionResult CandidateStud()
        {
            return View();
        }
        public ActionResult LoginStud()
        {
            return View();
        }
        public ActionResult RegisterStud()
        {
            return View();
        }
        public ActionResult LoginAdmin()
        {
            return View();
        }
        public ActionResult Votingpage()
        {
            return View();
        }

        
    }
}