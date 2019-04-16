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
    public class MenResultsController : Controller
    {
        private ParticipantsDB db = new ParticipantsDB();

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
