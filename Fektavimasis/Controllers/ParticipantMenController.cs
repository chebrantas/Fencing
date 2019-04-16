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
    public class ParticipantMenController : Controller
    {
        private ParticipantsDB db = new ParticipantsDB();

        // GET: ParticipantMen
        public ActionResult Index()
        {
            return View(db.ParticipantMens.ToList());
        }

        // GET: ParticipantMen/Details/5
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

        // GET: ParticipantMen/Delete/5
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

        // POST: ParticipantMen/Delete/5
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
