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
    public class ParticipantWomenController : Controller
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
            foreach (var item in db.ParticipantWomen)
            {
                SelectListItem selectListResult = new SelectListItem()
                {
                    Text = item.NameSurname,
                    Value = item.ParticipantWomanId.ToString(),
                    Selected = (item.ParticipantWomanId == id ? true : false)
                };
                listSelectListResult.Add(selectListResult);
            }

            return listSelectListResult;
        }

        // GET: ParticipantMen
        public ActionResult Index()
        {
            //nerodo visu ivestu zmoniu nes isveda visus is rezultatu lenteles, jei zmogus nera sarase rezultatu jo ir nerodo
            var query_all = db.WomenResults.GroupBy(g => g.ParticipantWomanId).Select(g => new FinalAllParticipantsInfoVM
            {
                ID = g.Key,//irasomas Dalyvio ID
                NameSurname = db.ParticipantWomen.Where(p => p.ParticipantWomanId == g.Key).FirstOrDefault().NameSurname, //irasomas dalyvio VArdasPavarde
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

            var first_query = from p in db.ParticipantWomen
                              from m in db.WomenResults
                              where p.ParticipantWomanId == m.ParticipantWomanId && p.ParticipantWomanId == id
                              select new { MatchupId = m.WomanResultId, Name = p.NameSurname, m.Piercing, m.Received, m.ParticipantCompetingId, m.Round };

            //gerai gaunasi, jei paduodame pries tai uzklausa suformuota i ToList neveikia, nes nepriima objektu
            var query = (from p in db.ParticipantWomen
                         from q in first_query
                         where p.ParticipantWomanId == q.ParticipantCompetingId
                         select new ParticipantsInfoViewModel
                         {
                             ResultsId = q.MatchupId,//cia reikia kovos id kad galetum editint jei klaida butu
                             FirstParticipantNameSurname = q.Name,
                             Piercing = q.Piercing,
                             Received = q.Received,
                             SecondParticipantNameSurname = p.NameSurname,
                             Round = q.Round
                         })
                         .ToList().OrderBy(o => o.FirstParticipantNameSurname);

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
        public ActionResult Create([Bind(Include = "ParticipantWomenId,NameSurname")] ParticipantWoman participantWomen)
        {
            if (ModelState.IsValid)
            {
                db.ParticipantWomen.Add(participantWomen);
                db.SaveChanges();

                return RedirectToAction("Create");
            }

            return View(participantWomen);
        }

        // GET: ParticipantMen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipantWoman participantWomen = db.ParticipantWomen.Find(id);
            if (participantWomen == null)
            {
                return HttpNotFound();
            }
            return View(participantWomen);
        }

        // POST: ParticipantMen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParticipantWomenId,NameSurname")] ParticipantWoman participantWomen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participantWomen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(participantWomen);
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
            WomanResult results = db.WomenResults.Find(id);
            if (results == null)
            {
                return HttpNotFound();
            }
            EditMatchupViewModel matchupEdit = new EditMatchupViewModel()
            {
                ID = results.ParticipantWomanId,
                FirstParticipantNameSurname = db.ParticipantWomen.Where(x => x.ParticipantWomanId == results.ParticipantWomanId).First().NameSurname,
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
                WomanResult results = db.WomenResults.Find(Matchup.ID);
                db.Entry(results).State = EntityState.Modified;
                results.Piercing = Matchup.Piercing;
                results.Received = Matchup.Received;
                db.SaveChanges();

                //surandame analoga priesininko puses kovos ir surasome priesinga rezultata
                WomanResult resultSecond = db.WomenResults.Where(x => x.ParticipantWomanId == results.ParticipantCompetingId)
                    .Where(y => y.ParticipantCompetingId == results.ParticipantWomanId).Where(z => z.Round == results.Round).FirstOrDefault();

                db.Entry(resultSecond).State = EntityState.Modified;
                resultSecond.Piercing = Matchup.Received;
                resultSecond.Received = Matchup.Piercing;
                db.SaveChanges();

                return RedirectToAction("Details", new { id = results.ParticipantWomanId });
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
            WomanResult matchup = db.WomenResults.Find(id);
            if (matchup == null)
            {
                return HttpNotFound();
            }
            EditMatchupViewModel matchupDelete = new EditMatchupViewModel()
            {
                ID = matchup.WomanResultId,
                FirstParticipantNameSurname = db.ParticipantWomen.Where(x => x.ParticipantWomanId == matchup.ParticipantWomanId).First().NameSurname,
                Piercing = matchup.Piercing,
                Received = matchup.Received,
                SecondParticipantNameSurname = db.ParticipantWomen.Where(x => x.ParticipantWomanId == matchup.ParticipantCompetingId).First().NameSurname,
                Round = matchup.Round
            };


            return View(matchupDelete);
        }

        // POST: ParticipantMen/DeleteMatchup/5
        [HttpPost, ActionName("DeleteMatchup")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedMatchup(int id)
        {
            WomanResult matchup = db.WomenResults.Find(id);
            db.WomenResults.Remove(matchup);
            db.SaveChanges();

            //surandame analoga priesininko puses kovos ir istriname taip pat
            WomanResult matchupSecond = db.WomenResults.Where(x => x.ParticipantWomanId == matchup.ParticipantCompetingId)
                .Where(y => y.ParticipantCompetingId == matchup.ParticipantWomanId).Where(z => z.Round == matchup.Round).FirstOrDefault();

            if (matchupSecond != null)
            {
                db.WomenResults.Remove(matchupSecond);
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { id = matchup.ParticipantWomanId });
        }

        // GET: /DeleteAll
        public ActionResult DeleteAll()
        {

            ViewBag.Lytis = "Women";
            return View();
        }

        // POST: ParticipantMen/DeleteAll
        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedAll()
        {

            //delete all results Men
            List<WomanResult> deleteQueryResult = db.WomenResults.Where(x => x.WomanResultId != 0).ToList();

            foreach (WomanResult r in deleteQueryResult)
            {
                db.WomenResults.Remove(r);
            }
            db.SaveChanges();

            //delete all participants Men
            List<ParticipantWoman> deleteQueryParticipants = db.ParticipantWomen.Where(x => x.ParticipantWomanId != 0).ToList();

            foreach (ParticipantWoman p in deleteQueryParticipants)
            {
                db.ParticipantWomen.Remove(p);
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
