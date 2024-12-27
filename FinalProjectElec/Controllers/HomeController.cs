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
        public ActionResult Students()
        {
            return View();
        }
        public ActionResult Tallies()
        {
            return View();
        }
        public ActionResult Votes()
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

        public JsonResult initVoteSet()
        {
            using(var db = new votingDbContext()) 
            {
                var setData = db.tblvotesets.OrderByDescending(s => s.set_num).FirstOrDefault();

                return Json(setData, JsonRequestBehavior.AllowGet);
            }   
            
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
                var candidatesData = (from cs in db.tblcandidatesets 
                                     join c in db.tblcandidates on cs.candiset_candidateid equals c.candidate_id
                                     join party in db.tblparties on c.candidate_partyid equals party.party_id
                                     join pos in db.tblpositions on c.candidate_positionid equals pos.position_id
                                     join vs in db.tblvotesets on cs.candiset_votesetid equals vs.set_id
                                     select new
                                     {
                                         c_id = c.candidate_id,
                                         c_fname = c.candidate_fname,
                                         c_lname = c.candidate_lname,
                                         c_partyid = c.candidate_partyid,
                                         c_posid = c.candidate_positionid,
                                         party_name = party.party_name,
                                         party_campaign = party.party_campaign,
                                         pos_id = pos.position_id,
                                         vs_num = vs.set_num
                                     }).ToList();

                return Json(candidatesData, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getStud(int uStudNum)
        {
            using (var db = new votingDbContext())
            {
                var studData = db.tblstudents.Where(s => s.student_num.Equals(uStudNum))
                .Select(s => new
                {
                    studId = s.student_id,
                    studNum = s.student_num
                }).FirstOrDefault();

                return Json(studData, JsonRequestBehavior.AllowGet);
            }

        }

        public int studLogin(int uStudNum)
        {
            using (var db = new votingDbContext())
            {
                var exists = db.tblstudents.Where(x => x.student_num.Equals(uStudNum)).FirstOrDefault();
                if(exists == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                };
            }
        }

        public int studValidate(int uStudId, int setNum)
        {
            using (var db = new votingDbContext())
            {
                var exists = db.tblvotes.Where(x => x.vote_studid.Equals(uStudId) && x.vote_votesetid.Equals(setNum)).FirstOrDefault();
                if (exists != null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public int adminLogin(string uEmail, string uPass)
        {
            using (var db = new votingDbContext())
            {
                var exists = db.tblaccounts.Where(x => x.account_email == uEmail && x.account_pass == uPass).FirstOrDefault();
                if (exists == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                };
            }
        }

        public void submitVote(int uStudId, int setNum, String voteData)
        {
            using (var db = new votingDbContext())
            {
                var addVote = new tblvotesModel
                {
                    vote_studid = uStudId,
                    vote_votesetid = setNum,
                    vote_value = voteData,
                    created_on = DateTime.Now,
                    updated_on = DateTime.Now
                };
                db.tblvotes.Add(addVote);
                db.SaveChanges();
            }
        }

        public void submitTally(int setNum, List<tallyDataModel> tallyData)
        {
            if (tallyData == null || !tallyData.Any())
            {
                throw new ArgumentNullException(nameof(tallyData), "tallyData is null or empty.");
            }

            using (var db = new votingDbContext())
            {
                foreach (var tally in tallyData)
                {
                    var exists = db.tbltallies.Where(x => x.tally_candidateid.Equals(tally.candidate_id) && x.tally_votesetid.Equals(setNum)).FirstOrDefault();
                    if (exists == null)
                    {
                        var addTally = new tbltalliesModel
                        {
                            tally_candidateid = tally.candidate_id,
                            tally_votesetid = setNum,
                            tally_value = 1,
                            created_on= DateTime.Now,
                            updated_on = DateTime.Now
                        };
                        db.tbltallies.Add(addTally);
                        db.SaveChanges();
                    }
                    else
                    {
                        exists.tally_value += 1;
                        exists.updated_on = DateTime.Now;
                        db.SaveChanges();
                    };
                }
            }
        }

        public void submitStudLog(int uStudNum, String lAction)
        {
            using (var db = new votingDbContext())
            {
                var addLog = new tbllogsModel
                {
                    log_studnum = uStudNum,
                    log_action = lAction,
                    created_on = DateTime.Now,
                    updated_on = DateTime.Now
                };
                db.tbllogs.Add(addLog);
                db.SaveChanges();
            }
        }

        public JsonResult loadAccountDash()
        {
            using (var db = new votingDbContext())
            {
                var accountsData = db.tblaccounts.Where(a => a.archive_status.Equals(0))
                    .Select(a => new
                {
                    account_email = a.account_email,
                    account_pass = a.account_pass,
                    archive_status = a.archive_status,
                    created_on = a.created_on,
                    updated_on = a.updated_on
                }).ToList();

                return Json(accountsData, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult loadLogDash()
        {
            using (var db = new votingDbContext())
            {
                var logsData = db.tbllogs.Where(l => l.archive_status.Equals(0))
                    .Select(l => new
                {
                    log_email = l.log_email,
                    log_studNum = l.log_studnum,
                    log_action = l.log_action,
                    archive_status = l.archive_status,
                    created_on = l.created_on,
                    updated_on = l.updated_on
                }).ToList();

                return Json(logsData, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult loadCandiDash()
        {
            using (var db = new votingDbContext())
            {
                var candisData = (from c in db.tblcandidates
                                  join party in db.tblparties on c.candidate_partyid equals party.party_id
                                  join pos in db.tblpositions on c.candidate_positionid equals pos.position_id
                                  where c.archive_status == 0
                                  select new
                                  {
                                      c_fname = c.candidate_fname,
                                      c_lname = c.candidate_lname,
                                      party_name = party.party_name,
                                      position_name = pos.position_name,
                                      archive_status = c.archive_status,
                                      created_on = c.created_on,
                                      updated_on = c.updated_on
                                  }).ToList();


                return Json(candisData, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult loadPartyDash()
        {
            using (var db = new votingDbContext())
            {
                var partiesData = db.tblparties.Where(p => p.archive_status.Equals(0))
                    .Select(p => new
                    {
                        party_name = p.party_name,
                        party_campaign = p.party_campaign,
                        archive_status = p.archive_status,
                        created_on = p.created_on,
                        updated_on = p.updated_on
                    }).ToList();

                return Json(partiesData, JsonRequestBehavior.AllowGet);
            }
        }
        
        public JsonResult loadStudDash()
        {
            using (var db = new votingDbContext())
            {
                var studsData = db.tblstudents.Where(s => s.archive_status.Equals(0))
                    .Select(s => new
                    {
                        stud_num = s.student_num,
                        archive_status = s.archive_status,
                        created_on = s.created_on,
                        updated_on = s.updated_on
                    }).ToList();

                return Json(studsData, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult loadVoteDash()
        {
            using (var db = new votingDbContext())
            {
                var votesData = (from v in db.tblvotes
                                  join s in db.tblstudents on v.vote_studid equals s.student_id
                                  join vs in db.tblvotesets on v.vote_votesetid equals vs.set_id
                                  where v.archive_status == 0
                                  select new
                                  {
                                      stud_num = s.student_num,
                                      set_num = vs.set_num,
                                      vote_value = v.vote_value,
                                      archive_status = v.archive_status,
                                      created_on = v.created_on,
                                      updated_on = v.updated_on
                                  }).ToList();


                return Json(votesData, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult loadPieData()
        //{
        //    using(var db = new votingDbContext())
        //    {
        //        var getPos = db.tblpositions.Select(p => p).ToList();
        //        List<int> posCount = new List<int>();

        //        foreach(var pos in getPos)
        //        {
        //            var tallyCount = (from t in db.tbltallies
        //                              join c in db.tblcandidates on t.tally_candidateid equals c.candidate_id
        //                              where c.candidate_positionid == pos.position_id
        //                              select new
        //                              {
        //                                  tally_value = t.tally_value
        //                              });

        //        }
        //    }
        //}
    }
}