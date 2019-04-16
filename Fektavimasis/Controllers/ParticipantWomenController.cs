using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fektavimasis.Models;

namespace Fektavimasis.Controllers
{
    public class ParticipantWomenController : Controller
    {
        private ParticipantsDB db = new ParticipantsDB();

        // GET: ParticipantWomen
        public ActionResult Index()
        {
            return View(db.ParticipantWomen.ToList());
        }

        // GET: ParticipantWomen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipantWoman participantWoman = db.ParticipantWomen.Find(id);
            if (participantWoman == null)
            {
                return HttpNotFound();
            }
            return View(participantWoman);
        }

        // GET: ParticipantWomen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParticipantWomen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParticipantWomanId,NameSurname")] ParticipantWoman participantWoman)
        {
            if (ModelState.IsValid)
            {
                db.ParticipantWomen.Add(participantWoman);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(participantWoman);
        }

        // GET: ParticipantWomen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipantWoman participantWoman = db.ParticipantWomen.Find(id);
            if (participantWoman == null)
            {
                return HttpNotFound();
            }
            return View(participantWoman);
        }

        // POST: ParticipantWomen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParticipantWomanId,NameSurname")] ParticipantWoman participantWoman)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participantWoman).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(participantWoman);
        }

        // GET: ParticipantWomen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipantWoman participantWoman = db.ParticipantWomen.Find(id);
            if (participantWoman == null)
            {
                return HttpNotFound();
            }
            return View(participantWoman);
        }

        // POST: ParticipantWomen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParticipantWoman participantWoman = db.ParticipantWomen.Find(id);
            db.ParticipantWomen.Remove(participantWoman);
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
