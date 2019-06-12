using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fektavimasis.Models;
using Fektavimasis.Models.ViewModel;

namespace Fektavimasis.Controllers
{
    public class ParticipantMenController : Controller
    {
        private ParticipantsDB db = new ParticipantsDB();

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
                listSelectListResult.Add(selectListResult);
            }

            return listSelectListResult;
        }

        // GET: ParticipantMen
        public ActionResult Index()
        {
            //nerodo visu ivestu zmoniu nes isveda visus is rezultatu lenteles, jei zmogus nera sarase rezultatu jo ir nerodo
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


        // GET: ParticipantMen/Details/5
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

            //gerai gaunasi, jei paduodame pries tai uzklausa suformuota i ToList neveikia, nes nepriima objektu
            var query = (from p in db.ParticipantMens
                         from q in first_query
                         where p.ParticipantMenId == q.ParticipantCompetingId
                         select new ParticipantsInfoViewModel
                         {
                             ResultsId = q.MatchupId,//cia reikia kovos id kad galetum editint jei klaida butu
                             FirstParticipantNameSurname = q.Name,
                             Piercing = q.Piercing,
                             Received = q.Received,
                             SecondParticipantNameSurname = p.NameSurname,
                             Round = q.Round
                         })
                         .ToList().OrderBy(o => o.Round);

            return View(query);
        }

        // GET: ParticipantMen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParticipantMen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParticipantMenId,NameSurname")] ParticipantMen participantMen, string NameSurnameList)
        {

            if (ModelState.IsValid)
            {
                if (NameSurnameList == "")
                {
                    db.ParticipantMens.Add(participantMen);
                    db.SaveChanges();

                    return RedirectToAction("Create");
                }
                else
                {
                    string NameList = NameSurnameList.Trim().Replace("\t", " ").Replace("#", "").Replace("\r\n", "#");
                    List<string> varduSarasas = NameList.Split('#').ToList();

                    foreach (string item in varduSarasas)
                    {
                        ParticipantMen insertMen = new ParticipantMen { NameSurname = item };
                        db.ParticipantMens.Add(insertMen);
                    }
                    //db.ParticipantMens.Add(participantMen);
                    db.SaveChanges();

                    return RedirectToAction("Create");
                }
            }

            return View(participantMen);
        }

        public ActionResult Create1([Bind(Include = "ParticipantMenId,NameSurname")] ParticipantMen participantMen, string NameSurnameList)
        {
            string NameList = NameSurnameList.Trim().Replace("\t", " ").Replace("#", "").Replace("\r\n", "#");
            List<string> varduSarasas = NameList.Split('#').ToList();

            if (ModelState.IsValid)
            {
                foreach (string item in varduSarasas)
                {
                    ParticipantMen insertMen = new ParticipantMen { NameSurname = item };
                    db.ParticipantMens.Add(insertMen);
                }
                //db.ParticipantMens.Add(participantMen);
                db.SaveChanges();

                return RedirectToAction("Create");
            }

            ViewBag.testukas = varduSarasas.Count();
            return View(participantMen);
        }


        // GET: ParticipantMen/Edit/5
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

        // POST: ParticipantMen/Edit/5
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

        // GET: ParticipantMen/EditMatchup/5
        public ActionResult EditMatchup(int? id)
        {

            //TODO sutvarkyti kad viena matchupa keiciant analogiskas priesingo dalyvio irgi butu taip pat pakeistas
            //TODO paziureti priesingo rasymo su skirtingais Roundais
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
                ID = results.MenResultId,
                FirstParticipantNameSurname = db.ParticipantMens.Where(x => x.ParticipantMenId == results.ParticipantMenId).First().NameSurname,
                Piercing = results.Piercing,
                Received = results.Received,
                SecondParticipantNameSurname = db.ParticipantMens.Where(x => x.ParticipantMenId == results.ParticipantCompetingId).First().NameSurname,
                Round = results.Round
            };

            return View(matchupEdit);
        }

        // POST: ParticipantMen/EditMatchup/5
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

                //surandame analoga priesininko puses kovos ir surasome priesinga rezultata
                MenResult resultSecond = db.MenResults.Where(x => x.ParticipantMenId == results.ParticipantCompetingId)
                    .Where(y => y.ParticipantCompetingId == results.ParticipantMenId).Where(z => z.Round == results.Round).FirstOrDefault();

                db.Entry(resultSecond).State = EntityState.Modified;
                resultSecond.Piercing = Matchup.Received;
                resultSecond.Received = Matchup.Piercing;
                db.SaveChanges();

                return RedirectToAction("Details", new { id = results.ParticipantMenId });
            }
            return View(Matchup);
        }

        // GET: ParticipantMen/DeleteMatchup/5
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

        // POST: ParticipantMen/DeleteMatchup/5
        [HttpPost, ActionName("DeleteMatchup")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedMatchup(int id)
        {
            MenResult matchup = db.MenResults.Find(id);
            db.MenResults.Remove(matchup);
            db.SaveChanges();

            //surandame analoga priesininko puses kovos ir istriname taip pat
            MenResult matchupSecond = db.MenResults.Where(x => x.ParticipantMenId == matchup.ParticipantCompetingId)
                .Where(y => y.ParticipantCompetingId == matchup.ParticipantMenId).Where(z => z.Round == matchup.Round).FirstOrDefault();

            if (matchupSecond != null)
            {
                db.MenResults.Remove(matchupSecond);
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { id = matchup.ParticipantMenId });
        }

        // GET: /DeleteAll
        public ActionResult DeleteAll()
        {

            ViewBag.Lytis = "Men";
            return View();
        }

        // POST: ParticipantMen/DeleteAll
        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedAll()
        {

            //delete all results Men
            List<MenResult> deleteQueryResult = db.MenResults.Where(x => x.MenResultId != 0).ToList();

            foreach (MenResult r in deleteQueryResult)
            {
                db.MenResults.Remove(r);
            }
            db.SaveChanges();

            //delete all participants Men
            List<ParticipantMen> deleteQueryParticipants = db.ParticipantMens.Where(x => x.ParticipantMenId != 0).ToList();

            foreach (ParticipantMen p in deleteQueryParticipants)
            {
                db.ParticipantMens.Remove(p);
            }
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
