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
            return View(db.ParticipantMens.ToList());
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
                ViewBag.UpdateInfo = info;
                //return sb.ToString();

                //Irasoma i DB
                MenResult newRecord = new MenResult();
                //nesupranta Parse
                //var test = db.MenResults.Where(x => x.ParticipantMenId == Int32.Parse(ParticipantsId.First())).Where(y => y.ParticipantCompetingId == Int32.Parse(Participants2Id.First())).First();
                
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
            ParticipantMen participantMen = db.ParticipantMens.Find(id);
            if (participantMen == null)
            {
                return HttpNotFound();
            }
            return View(participantMen);
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
