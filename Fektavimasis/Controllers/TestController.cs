using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Fektavimasis.Models;
using Fektavimasis.Models.ViewModel;

namespace Fektavimasis.Controllers
{
    public class TestController : Controller
    {
        private ParticipantsDB db = new ParticipantsDB();

        //create score listbox
        private List<SelectListItem> Score()
        {
            List<SelectListItem> listSelectListResult = new List<SelectListItem>();
            for (int i = 0; i < 4; i++)
            {
                SelectListItem selectListResult = new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = (i == 0 ? true : false)
                };
                //if (i==0)
                //{
                //    selectListResult.Selected = true;
                //}
                listSelectListResult.Add(selectListResult);
            }
            return listSelectListResult;
        }


        //Roundo dropdownlistui
        private List<SelectListItem> Round()
        {
            List<SelectListItem> listSelectListResult = new List<SelectListItem>();
            for (int i = 1; i < 5; i++)
            {
                SelectListItem selectListResult = new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = (i == 0 ? true : false)
                };
                //if (i==0)
                //{
                //    selectListResult.Selected = true;
                //}
                listSelectListResult.Add(selectListResult);
            }
            return listSelectListResult;
        }

        //dalyviu listboxui
        private List<SelectListItem> Members(int id = 0)
        {
            List<SelectListItem> listSelectListResult = new List<SelectListItem>();
            foreach (var item in db.ParticipantMens)
            {
                SelectListItem selectListResult = new SelectListItem()
                {
                    Text = item.NameSurname,
                    Value = item.ParticipantMenId.ToString(),
                    Selected = (item.ParticipantMenId == id ? true : false)
                };

                //if (item.ParticipantMenId == id)
                //{
                //    selectListResult.Selected = true;
                //}
                listSelectListResult.Add(selectListResult);
            }

            return listSelectListResult;
        }


        // GET: Test
        public ActionResult Index()
        {

            //var test = (from m in db.MenResults
            //            join p in db.ParticipantMens on m.ParticipantMenId equals p.ParticipantMenId
            //            join pp in db.ParticipantSecondMens on m.ParticipantCompetingId equals pp.ParticipantSecondMenId
            //            select new ParticipantsInfoViewModel() { FirstParticipantNameSurname = p.NameSurname, SecondParticipantNameSurname = pp.NameSurname, Piercing = m.Piercing, Received = m.Received, Round = m.Round }).ToList();

            //var first_query = from p in db.ParticipantMens
            //                  from m in db.MenResults
            //                  where p.ParticipantMenId == m.ParticipantMenId
            //                  select new { Name = p.NameSurname, m.Piercing, m.Received, m.ParticipantCompetingId, m.Round };

            ////gerai gaunasi, jei paduodame pries tai uzklausa suformuota i ToList nebveikia nes nepriima objektu
            //var query = (from p in db.ParticipantMens
            //             from q in first_query
            //             where p.ParticipantMenId == q.ParticipantCompetingId
            //             select new ParticipantsInfoViewModel
            //             {
            //                 FirstParticipantNameSurname = q.Name,
            //                 Piercing = q.Piercing,
            //                 Received = q.Received,
            //                 SecondParticipantNameSurname = p.NameSurname,
            //                 Round = q.Round
            //             })
            //             .ToList().OrderBy(o => o.FirstParticipantNameSurname);

            //var test2=from m in db.MenResults where m.Piercing==3 && m.ParticipantMenId==1
            //          select new { cc=m }

            //var test3 = (from m in db.MenResults where m.Piercing == 3 && m.ParticipantMenId == 1 select new { }).ToList().Count();
            //var test4 = db.MenResults.GroupBy(g => 1).Select(g => new { Win = g.Count(i => i.Piercing == 3), PiersingTotal = g.Sum(i => i.Piercing), ReceivedTotal = g.Sum(i => i.Received) }).ToList();

            var query_all = db.MenResults.GroupBy(g => g.ParticipantMenId).Select(g => new FinalAllParticipantsInfoVM
            {
                ID = g.Key,//irasomas Dalyvio ID
                NameSurname = db.ParticipantMens.Where(p => p.ParticipantMenId == g.Key).FirstOrDefault().NameSurname, //irasomas dalyvio VArdasPavarde
                Win = g.Count(i => i.Piercing == 3),
                PiercingTotal = g.Sum(s => s.Piercing),
                ReceivedTotal = g.Sum(s => s.Received)
            })
                .OrderByDescending(o => o.Win).ThenByDescending(o => o.PiercingTotal - o.ReceivedTotal).ToList();//surikiuoja pagal pergales, o tada pagal idurtu praleistu santyki

            //return View(db.ParticipantMens.ToList());
            return View(query_all);
        }

        public ActionResult Bandom()
        {
            //return View(db.ParticipantMens.ToList());
            //ViewBag.ParticipantsId = new SelectList(db.ParticipantMens, "ParticipantMenId", "NameSurname");
            ViewBag.ParticipantsId = Members();
            //ViewBag.Participants2Id = new SelectList(db.ParticipantMens, "ParticipantMenId", "NameSurname");
            ViewBag.Participants2Id = Members();
            ViewBag.ScoreId = Score();
            ViewBag.Score2Id = Score();
            ViewBag.RoundId = Round();


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Bandom(IEnumerable<string> ParticipantsId, IEnumerable<string> Participants2Id, IEnumerable<string> ScoreId, IEnumerable<string> Score2Id, IEnumerable<string> RoundId, ParticipantsViewModel Namesmodel)
        {
            if (ScoreId == null)
            {
                ViewBag.ParticipantsId = new SelectList(db.ParticipantMens, "ParticipantMenId", "NameSurname");
                ViewBag.Participants2Id = new SelectList(db.ParticipantMens, "ParticipantMenId", "NameSurname");
                ViewBag.ScoreId = Score();
                ViewBag.Score2Id = Score();
                ViewBag.UpdateInfo = "No Result 1 player are selected";
                return PartialView("Bandom");
            }
            else if (Score2Id == null)
            {
                ViewBag.ParticipantsId = new SelectList(db.ParticipantMens, "ParticipantMenId", "NameSurname");
                ViewBag.Participants2Id = new SelectList(db.ParticipantMens, "ParticipantMenId", "NameSurname");
                ViewBag.ScoreId = Score();
                ViewBag.Score2Id = Score();
                ViewBag.UpdateInfo = "No Result 2 player are selected";
                return PartialView("Bandom");
            }
            else if (ParticipantsId == null)
            {
                ViewBag.ParticipantsId = new SelectList(db.ParticipantMens, "ParticipantMenId", "NameSurname");
                ViewBag.Participants2Id = new SelectList(db.ParticipantMens, "ParticipantMenId", "NameSurname");
                ViewBag.ScoreId = Score();
                ViewBag.Score2Id = Score();
                ViewBag.UpdateInfo = "No Names for player 1 are selected";
                return PartialView("Bandom");
            }
            else if (Participants2Id == null)
            {
                ViewBag.ParticipantsId = new SelectList(db.ParticipantMens, "ParticipantMenId", "NameSurname");
                ViewBag.Participants2Id = new SelectList(db.ParticipantMens, "ParticipantMenId", "NameSurname");
                ViewBag.ScoreId = Score();
                ViewBag.Score2Id = Score();
                ViewBag.UpdateInfo = "No Names for player 2 are selected";
                return PartialView("Bandom");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                //sb.Append("Players " + string.Join(",", db.ParticipantMens.Find(Int32.Parse(ParticipantsId.First())).NameSurname) + " vs " + string.Join(",", db.ParticipantMens.Find(Int32.Parse(Participants2Id.First())).NameSurname) + " ended " + string.Join(",", ScoreId) + string.Join(",", Score2Id));
                string info = $"Roundas:{RoundId.First()} {db.ParticipantMens.Find(Int32.Parse(ParticipantsId.First())).NameSurname} vs {db.ParticipantMens.Find(Int32.Parse(Participants2Id.First())).NameSurname} - {ScoreId.First()}:{Score2Id.First()}";
                ViewBag.ParticipantsId = Members();
                ViewBag.Participants2Id = Members();
                //ViewBag.ParticipantsId = new SelectList(db.ParticipantMens, "ParticipantMenId", "NameSurname", ParticipantsId.First());
                //ViewBag.Participants2Id = new SelectList(db.ParticipantMens, "ParticipantMenId", "NameSurname", Participants2Id.Last());
                ViewBag.ScoreId = Score();
                ViewBag.Score2Id = Score();
                ViewBag.RoundId = Round();

                //ViewBag.UpdateInfo = sb.ToString();
                //return sb.ToString();

                //Irasoma i DB
                MenResult newRecord = new MenResult();

                //nesupranta Parse todel konvertuojame pries uzklausa
                int ParticipantIdForEF = Int32.Parse(ParticipantsId.First());
                int ParticipantCompetingIdForEF = Int32.Parse(Participants2Id.First());
                int RoundIdForEF = Int32.Parse(RoundId.First());

                var MatchupExist = db.MenResults.Where(x => x.ParticipantMenId == ParticipantIdForEF && x.ParticipantCompetingId == ParticipantCompetingIdForEF && x.Round == RoundIdForEF).FirstOrDefault();

                if (MatchupExist == null && (ParticipantIdForEF != ParticipantCompetingIdForEF))
                {
                    newRecord = new MenResult
                    {
                        ParticipantMenId = Int32.Parse(ParticipantsId.First()),
                        ParticipantCompetingId = Int32.Parse(Participants2Id.First()),
                        Piercing = Int32.Parse(ScoreId.First()),
                        Received = Int32.Parse(Score2Id.First()),
                        Round = Int32.Parse(RoundId.First())
                    };

                    db.MenResults.Add(newRecord);
                    db.SaveChanges();
                }
                else
                {
                    info = $"Toks įrašas šiame raunde jau egzistuoja";
                }

                ViewBag.UpdateInfo = info;
                return PartialView("Bandom");
                //TODO padaryti kad score nunulintu po posto
            }
        }


        //sitas geras variantas
        public ActionResult BandomDu()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            List<SelectListItem> listSelectListResult = new List<SelectListItem>();

            foreach (var member in db.ParticipantMens)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = member.NameSurname,
                    Value = member.ParticipantMenId.ToString()
                };
                listSelectListItems.Add(selectList);
            }
            for (int i = 0; i < 4; i++)
            {
                SelectListItem selectListResult = new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                };
                listSelectListResult.Add(selectListResult);
            }


            ParticipantsViewModel participantsViewModel = new ParticipantsViewModel()
            {
                Names = listSelectListItems,
                Result = listSelectListResult
            };
            return View(participantsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult BandomDu(IEnumerable<string> Names, IEnumerable<string> Result, ParticipantsViewModel Namesmodel)
        {
            if (Result == null)
            {
                ViewBag.UpdateInfo = "No Result are selected";
                return PartialView("BandomDu");
            }
            else if (Names == null)
            {
                ViewBag.UpdateInfo = "No Names are selected";
                return PartialView("BandomDu");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Players " + string.Join(",", Names) + " ended " + string.Join(",", Result));
                ViewBag.UpdateInfo = sb.ToString();
                //return sb.ToString();
                return PartialView("BandomDu");
            }
        }




        public JsonResult RefreshDepartments()
        {
            return Json(GetDepartments(), JsonRequestBehavior.AllowGet);
        }

        private SelectList GetDepartments()
        {
            var deparments = GetDepartments();
            SelectList list = new SelectList(deparments);
            return list;
        }


        //--------------------------
        // GET: Destination/Edit/5
        public ActionResult Edit1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipantMen destination = db.ParticipantMens.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: Destination/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "DestinationId,Name,Country,Description,Photo")] ParticipantsViewModel destination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destination).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return View(destination);
            }
            return View(destination);
        }













        //-----------------------------

        // GET: Test/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var first_query = from p in db.ParticipantMens
                              from m in db.MenResults
                              where p.ParticipantMenId == m.ParticipantMenId && p.ParticipantMenId == id
                              select new { MatchupId = m.MenResultId, Name = p.NameSurname, m.Piercing, m.Received, m.ParticipantCompetingId, m.Round };

            //gerai gaunasi, jei paduodame pries tai uzklausa suformuota i ToList neveikia nes nepriima objektu
            var query = (from p in db.ParticipantMens
                         from q in first_query
                         where p.ParticipantMenId == q.ParticipantCompetingId
                         select new ParticipantsInfoViewModel
                         {
                             MenResultsId = q.MatchupId,//cia reikia kovos id kad galetum editint jei klaida butu
                             FirstParticipantNameSurname = q.Name,
                             Piercing = q.Piercing,
                             Received = q.Received,
                             SecondParticipantNameSurname = p.NameSurname,
                             Round = q.Round
                         })
                         .ToList().OrderBy(o => o.FirstParticipantNameSurname);

            return View(query);
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParticipantMenId,NameSurname")] ParticipantMen participantMen)
        {
            if (ModelState.IsValid)
            {
                db.ParticipantMens.Add(participantMen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(participantMen);
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipantMen participantMen = db.ParticipantMens.Find(id);
            if (participantMen == null)
            {
                return HttpNotFound();
            }
            return View(participantMen);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParticipantMenId,NameSurname")] ParticipantMen participantMen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participantMen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(participantMen);
        }

        // GET: Test/EditMatchup/5
        public ActionResult EditMatchup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenResult results = db.MenResults.Find(id);
            if (results == null)
            {
                return HttpNotFound();
            }
            EditMatchupViewModel matchupEdit = new EditMatchupViewModel()
            {
                ID=results.MenResultId,
                FirstParticipantNameSurname = db.ParticipantMens.Where(x=>x.ParticipantMenId==results.ParticipantMenId).First().NameSurname,
                Piercing=results.Piercing,
                Received=results.Received,
                SecondParticipantNameSurname = db.ParticipantMens.Where(x => x.ParticipantMenId == results.ParticipantCompetingId).First().NameSurname,
                Round=results.Round
            };

            return View(matchupEdit);
        }

        // POST: Test/EditMatchup/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMatchup([Bind(Include = "ID,Piercing,Received")] EditMatchupViewModel Matchup)
        {
            if (ModelState.IsValid)
            {
                if (Matchup.ID == 0) 
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MenResult results = db.MenResults.Find(Matchup.ID);
                db.Entry(results).State = EntityState.Modified;
                results.Piercing = Matchup.Piercing;
                results.Received = Matchup.Received;
                db.SaveChanges();

                return RedirectToAction("Details", new { id =results.ParticipantMenId });
            }
            return View(Matchup);
        }

        // GET: Test/Delete/5
        public ActionResult DeleteMatchup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenResult matchup = db.MenResults.Find(id);
            if (matchup == null)
            {
                return HttpNotFound();
            }
            EditMatchupViewModel matchupDelete = new EditMatchupViewModel()
            {
                ID = matchup.MenResultId,
                FirstParticipantNameSurname = db.ParticipantMens.Where(x => x.ParticipantMenId == matchup.ParticipantMenId).First().NameSurname,
                Piercing = matchup.Piercing,
                Received = matchup.Received,
                SecondParticipantNameSurname = db.ParticipantMens.Where(x => x.ParticipantMenId == matchup.ParticipantCompetingId).First().NameSurname,
                Round = matchup.Round
            };


            return View(matchupDelete);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("DeleteMatchup")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedMatchup(int id)
        {
            MenResult matchup = db.MenResults.Find(id);
            db.MenResults.Remove(matchup);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = matchup.ParticipantMenId });
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipantMen participantMen = db.ParticipantMens.Find(id);
            if (participantMen == null)
            {
                return HttpNotFound();
            }
            return View(participantMen);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParticipantMen participantMen = db.ParticipantMens.Find(id);
            db.ParticipantMens.Remove(participantMen);
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
