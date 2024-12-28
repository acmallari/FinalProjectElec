using FinalProjectElec.Models;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;

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

        public JsonResult loadParties()
        {
            using (var db = new votingDbContext())
            {
                var partiesData = db.tblparties.Select(p => new
                {
                    partyid = p.party_id,
                    party_name = p.party_name
                }).ToList();

                return Json(partiesData, JsonRequestBehavior.AllowGet);
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
                    account_id = a.account_id,
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
                                      cid = c.candidate_id,
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
                        partyid = p.party_id,
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
                        studid = s.student_id,
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

        public JsonResult loadPiePres(int setNum)
        {
            using (var db = new votingDbContext())
            {
                var getPres = (from t in db.tbltallies
                               join c in db.tblcandidates on t.tally_candidateid equals c.candidate_id
                               join p in db.tblpositions on c.candidate_positionid equals p.position_id
                               join vs in db.tblvotesets on t.tally_votesetid equals vs.set_id
                               where p.position_id.Equals(1) && vs.set_id.Equals(setNum)
                               select new
                               {
                                   candidate_name = c.candidate_fname +" "+ c.candidate_lname,
                                   tally_value = t.tally_value
                               }).ToList();


                return Json(getPres, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult loadPieVP(int setNum)
        {
            using (var db = new votingDbContext())
            {
                var getVP = (from t in db.tbltallies
                               join c in db.tblcandidates on t.tally_candidateid equals c.candidate_id
                               join p in db.tblpositions on c.candidate_positionid equals p.position_id
                               join vs in db.tblvotesets on t.tally_votesetid equals vs.set_id
                               where p.position_id.Equals(2) && vs.set_id.Equals(setNum)
                               select new
                               {
                                   candidate_name = c.candidate_fname + " " + c.candidate_lname,
                                   tally_value = t.tally_value
                               }).ToList();


                return Json(getVP, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult loadPieSec(int setNum)
        {
            using (var db = new votingDbContext())
            {
                var getSec = (from t in db.tbltallies
                               join c in db.tblcandidates on t.tally_candidateid equals c.candidate_id
                               join p in db.tblpositions on c.candidate_positionid equals p.position_id
                               join vs in db.tblvotesets on t.tally_votesetid equals vs.set_id
                               where p.position_id.Equals(3) && vs.set_id.Equals(setNum)
                               select new
                               {
                                   candidate_name = c.candidate_fname + " " + c.candidate_lname,
                                   tally_value = t.tally_value
                               }).ToList();


                return Json(getSec, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult loadPieTrea(int setNum)
        {
            using (var db = new votingDbContext())
            {
                var getTrea = (from t in db.tbltallies
                               join c in db.tblcandidates on t.tally_candidateid equals c.candidate_id
                               join p in db.tblpositions on c.candidate_positionid equals p.position_id
                               join vs in db.tblvotesets on t.tally_votesetid equals vs.set_id
                               where p.position_id.Equals(4) && vs.set_id.Equals(setNum)
                               select new
                               {
                                   candidate_name = c.candidate_fname + " " + c.candidate_lname,
                                   tally_value = t.tally_value
                               }).ToList();


                return Json(getTrea, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult loadPieAud(int setNum)
        {
            using (var db = new votingDbContext())
            {
                var getAud = (from t in db.tbltallies
                               join c in db.tblcandidates on t.tally_candidateid equals c.candidate_id
                               join p in db.tblpositions on c.candidate_positionid equals p.position_id
                               join vs in db.tblvotesets on t.tally_votesetid equals vs.set_id
                               where p.position_id.Equals(5) && vs.set_id.Equals(setNum)
                               select new
                               {
                                   candidate_name = c.candidate_fname + " " + c.candidate_lname,
                                   tally_value = t.tally_value
                               }).ToList();


                return Json(getAud, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult loadPiePRO(int setNum)
        {
            using (var db = new votingDbContext())
            {
                var getPRO = (from t in db.tbltallies
                               join c in db.tblcandidates on t.tally_candidateid equals c.candidate_id
                               join p in db.tblpositions on c.candidate_positionid equals p.position_id
                               join vs in db.tblvotesets on t.tally_votesetid equals vs.set_id
                               where p.position_id.Equals(6) && vs.set_id.Equals(setNum)
                               select new
                               {
                                   candidate_name = c.candidate_fname + " " + c.candidate_lname,
                                   tally_value = t.tally_value
                               }).ToList();


                return Json(getPRO, JsonRequestBehavior.AllowGet);
            }
        }

        public void addAccount(String uEmail, String uPass)
        {
            using (var db = new votingDbContext())
            {
                var addAcc = new tblaccountsModel
                {
                    account_email = uEmail,
                    account_pass = uPass,
                    created_on = DateTime.Now,
                    updated_on = DateTime.Now
                };
                db.tblaccounts.Add(addAcc);
                db.SaveChanges();
            }
        }

        public void addParty(String pName, String pCamp)
        {
            using (var db = new votingDbContext())
            {
                var addParty = new tblpartiesModel
                {
                    party_name = pName,
                    party_campaign = pCamp,
                    created_on = DateTime.Now,
                    updated_on = DateTime.Now
                };
                db.tblparties.Add(addParty);
                db.SaveChanges();
            }
        }
        
        public void addStud(int studNum)
        {
            using (var db = new votingDbContext())
            {
                var addStud = new tblstudentsModel
                {
                    student_num = studNum,
                    created_on = DateTime.Now,
                    updated_on = DateTime.Now
                };
                db.tblstudents.Add(addStud);
                db.SaveChanges();
            }
        }

        public void addCandi(String fName, String lName, int party, int pos)
        {
            using (var db = new votingDbContext())
            {
                var addCandi = new tblcandidatesModel
                {
                    candidate_fname = fName,
                    candidate_lname = lName,
                    candidate_partyid = party,
                    candidate_positionid = pos,
                    created_on = DateTime.Now,
                    updated_on = DateTime.Now
                };
                db.tblcandidates.Add(addCandi);
                db.SaveChanges();
            }
        }

        public JsonResult loadEditAcc(int accid)
        {
            using (var db = new votingDbContext())
            {
                var accData = db.tblaccounts.Where(a => a.account_id.Equals(accid))
                .Select(a => new
                {
                    accId = a.account_id,
                    accEmail = a.account_email,
                    accPass = a.account_pass
                }).FirstOrDefault();

                return Json(accData, JsonRequestBehavior.AllowGet);
            }

        }

        public void editAccount(int accid, String uEmail, String uPass)
        {
            using (var db = new votingDbContext())
            {
                var editAcc = db.tblaccounts.Where(a => a.account_id.Equals(accid)).FirstOrDefault();

                editAcc.account_email = uEmail;
                editAcc.account_pass = uPass;
                editAcc.updated_on = DateTime.Now;
                db.SaveChanges();
            }
        }

        public JsonResult loadEditCandi(int candid)
        {
            using (var db = new votingDbContext())
            {
                var candiData = db.tblcandidates.Where(c => c.candidate_id.Equals(candid))
                .Select(c => new
                {
                    candId = c.candidate_id,
                    candFname = c.candidate_fname,
                    candLname = c.candidate_lname,
                    candParty = c.candidate_partyid,
                    candPos = c.candidate_positionid
                }).FirstOrDefault();

                return Json(candiData, JsonRequestBehavior.AllowGet);
            }
        }

        public void editCandi(int candid, String fname, String lname, int party, int pos)
        {
            using (var db = new votingDbContext())
            {
                var editCandi = db.tblcandidates.Where(c => c.candidate_id.Equals(candid)).FirstOrDefault();

                editCandi.candidate_fname = fname;
                editCandi.candidate_lname = lname;
                editCandi.candidate_partyid = party;
                editCandi.candidate_positionid = pos;
                editCandi.updated_on = DateTime.Now;
                db.SaveChanges();
            }
        }

        public JsonResult loadEditParty(int partyid)
        {
            using (var db = new votingDbContext())
            {
                var partyData = db.tblparties.Where(p => p.party_id.Equals(partyid))
                .Select(p => new
                {
                    partyId = p.party_id,
                    partyName = p.party_name,
                    partyCamp = p.party_campaign
                }).FirstOrDefault();

                return Json(partyData, JsonRequestBehavior.AllowGet);
            }
        }

        public void editParty(int partyid, String pName, String pCamp)
        {
            using (var db = new votingDbContext())
            {
                var editParty = db.tblparties.Where(p => p.party_id.Equals(partyid)).FirstOrDefault();

                editParty.party_name = pName;
                editParty.party_campaign = pCamp;
                editParty.updated_on = DateTime.Now;
                db.SaveChanges();
            }
        }

        public JsonResult loadEditStud(int studid)
        {
            using (var db = new votingDbContext())
            {
                var studData = db.tblstudents.Where(s => s.student_id.Equals(studid))
                .Select(s => new
                {
                    studId = s.student_id,
                    studNum = s.student_num
                }).FirstOrDefault();

                return Json(studData, JsonRequestBehavior.AllowGet);
            }
        }

        public void editStud(int studid, int studNum)
        {
            using (var db = new votingDbContext())
            {
                var editStud = db.tblstudents.Where(s => s.student_id.Equals(studid)).FirstOrDefault();

                editStud.student_num = studNum;
                editStud.updated_on = DateTime.Now;
                db.SaveChanges();
            }
        }

        public void archiveAcc(int accid)
        {
            using (var db = new votingDbContext())
            {
                var archAcc = db.tblaccounts.Where(a => a.account_id.Equals(accid)).FirstOrDefault();

                archAcc.archive_status = 1;
                archAcc.updated_on = DateTime.Now;
                db.SaveChanges();
            }
        }
        public void archiveCandi(int candid)
        {
            using (var db = new votingDbContext())
            {
                var archCandi = db.tblcandidates.Where(c => c.candidate_id.Equals(candid)).FirstOrDefault();

                archCandi.archive_status = 1;
                archCandi.updated_on = DateTime.Now;
                db.SaveChanges();
            }
        }
        public void archiveParty(int partyid)
        {
            using (var db = new votingDbContext())
            {
                var archParty = db.tblparties.Where(p => p.party_id.Equals(partyid)).FirstOrDefault();

                archParty.archive_status = 1;
                archParty.updated_on = DateTime.Now;
                db.SaveChanges();
            }
        }
        public void archiveStud(int studid)
        {
            using (var db = new votingDbContext())
            {
                var archStud = db.tblstudents.Where(s => s.student_id.Equals(studid)).FirstOrDefault();

                archStud.archive_status = 1;
                archStud.updated_on = DateTime.Now;
                db.SaveChanges();
            }
        }

        public void submitAdminLog(String uEmail, String lAction)
        {
            using (var db = new votingDbContext())
            {
                var addLog = new tbllogsModel
                {
                    log_email = uEmail,
                    log_action = lAction,
                    created_on = DateTime.Now,
                    updated_on = DateTime.Now
                };
                db.tbllogs.Add(addLog);
                db.SaveChanges();
            }
        }
    }
}