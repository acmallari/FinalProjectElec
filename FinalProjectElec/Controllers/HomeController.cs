using FinalProjectElec.Models;
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

        public JsonResult loadPositions()
        {
            using(var db = new votingDbContext())
            {
                var positionsData = db.tblpositions.Select(p => new
                {
                    position_id = p.position_id,
                    position_name = p.position_name
                }).ToList();

                return Json(positionsData, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult loadCandidates()
        {
            using (var db = new votingDbContext())
            {
                var candidatesData = (from c in db.tblcandidates
                                     join party in db.tblparties on c.candidate_partyid equals party.party_id
                                     join pos in db.tblpositions on c.candidate_positionid equals pos.position_id
                                     select new
                                     {
                                         c_fname = c.candidate_fname,
                                         c_lname = c.candidate_lname,
                                         c_partyid = c.candidate_partyid,
                                         c_posid = c.candidate_positionid,
                                         party_name = party.party_name,
                                         party_campaign = party.party_campaign,
                                         pos_id = pos.position_id
                                     }).ToList();

                return Json(candidatesData, JsonRequestBehavior.AllowGet);
            }
        }
    }
}