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
    public class WomanResultsController : Controller
    {
        private ParticipantsDB db = new ParticipantsDB();

        // GET: WomanResults
        public ActionResult Index()
        {
            return View(db.WomenResults.ToList());
        }

        // GET: WomanResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WomanResult womanResult = db.WomenResults.Find(id);
            if (womanResult == null)
            {
                return HttpNotFound();
            }
            return View(womanResult);
        }

        // GET: WomanResults/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WomanResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WomanResultId,ParticipantWomanId,ParticipantCompetingId,Piercing,Received,Round")] WomanResult womanResult)
        {
            if (ModelState.IsValid)
            {
                db.WomenResults.Add(womanResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(womanResult);
        }

        // GET: WomanResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WomanResult womanResult = db.WomenResults.Find(id);
            if (womanResult == null)
            {
                return HttpNotFound();
            }
            return View(womanResult);
        }

        // POST: WomanResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WomanResultId,ParticipantWomanId,ParticipantCompetingId,Piercing,Received,Round")] WomanResult womanResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(womanResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(womanResult);
        }

        // GET: WomanResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WomanResult womanResult = db.WomenResults.Find(id);
            if (womanResult == null)
            {
                return HttpNotFound();
            }
            return View(womanResult);
        }

        // POST: WomanResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WomanResult womanResult = db.WomenResults.Find(id);
            db.WomenResults.Remove(womanResult);
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
