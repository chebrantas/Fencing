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

        // GET: Test
        public ActionResult Index()
        {
            return View(db.ParticipantMens.ToList());
        }

        public ActionResult Bandom()
        {
            //return View(db.ParticipantMens.ToList());
            ViewBag.ParticipantsId = new SelectList(db.ParticipantMens, "ParticipantMenId", "NameSurname");
            return View();
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
        public string BandomDu(IEnumerable<string> Names , IEnumerable<string> Result)
        {
            if (Result == null)
            {
                return "No Result are selected";
            }
            else if (Names == null)
            {
                return "No Names are selected";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Players " + string.Join(",", Names) + " ended " + string.Join(",", Result));
                return sb.ToString();
            }
        }

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
