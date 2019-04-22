﻿using System;
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
    public class MenResultsController : Controller
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

        public ActionResult InsertResult()
        {
            ViewBag.ParticipantsId = Members();
            ViewBag.Participants2Id = Members();
            ViewBag.ScoreId = Score();
            ViewBag.Score2Id = Score();
            ViewBag.RoundId = Round();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertResult(IEnumerable<string> ParticipantsId, IEnumerable<string> Participants2Id, IEnumerable<string> ScoreId, IEnumerable<string> Score2Id, IEnumerable<string> RoundId, ParticipantsViewModel Namesmodel)
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
                string info = $"Roundas:{RoundId.First()} {db.ParticipantMens.Find(Int32.Parse(ParticipantsId.First())).NameSurname} vs {db.ParticipantMens.Find(Int32.Parse(Participants2Id.First())).NameSurname} - {ScoreId.First()}:{Score2Id.First()}";
                ViewBag.ParticipantsId = Members();
                ViewBag.Participants2Id = Members();
                ViewBag.ScoreId = Score();
                ViewBag.Score2Id = Score();
                ViewBag.RoundId = Round();
                ViewBag.UpdateInfo = info;

                //Irasoma i DB
                MenResult newRecord = new MenResult();

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

                return PartialView("Bandom");
                //TODO padaryti kad score nunulintu po posto
            }
        }

        // GET: MenResults
        public ActionResult Index()
        {
            return View(db.MenResults.ToList());
        }

        // GET: MenResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenResult menResult = db.MenResults.Find(id);
            if (menResult == null)
            {
                return HttpNotFound();
            }
            return View(menResult);
        }

        // GET: MenResults/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenResultId,ParticipantMenId,ParticipantCompetingId,Piercing,Received,Round")] MenResult menResult)
        {
            if (ModelState.IsValid)
            {
                db.MenResults.Add(menResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menResult);
        }

        // GET: MenResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenResult menResult = db.MenResults.Find(id);
            if (menResult == null)
            {
                return HttpNotFound();
            }
            return View(menResult);
        }

        // POST: MenResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenResultId,ParticipantMenId,ParticipantCompetingId,Piercing,Received,Round")] MenResult menResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menResult);
        }

        // GET: MenResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenResult menResult = db.MenResults.Find(id);
            if (menResult == null)
            {
                return HttpNotFound();
            }
            return View(menResult);
        }

        // POST: MenResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenResult menResult = db.MenResults.Find(id);
            db.MenResults.Remove(menResult);
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
