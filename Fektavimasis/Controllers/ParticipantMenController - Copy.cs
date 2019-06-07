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
    //public class ParticipantMenController : Controller
    //{
    //    private ParticipantsDB db = new ParticipantsDB();

    //    private List<SelectListItem> Score()
    //    {
    //        List<SelectListItem> listSelectListResult = new List<SelectListItem>();
    //        for (int i = 0; i < 4; i++)
    //        {
    //            SelectListItem selectListResult = new SelectListItem()
    //            {
    //                Text = i.ToString(),
    //                Value = i.ToString(),
    //                Selected = (i == 0 ? true : false)
    //            };
    //            listSelectListResult.Add(selectListResult);
    //        }
    //        return listSelectListResult;
    //    }


    //    //Roundo dropdownlistui
    //    private List<SelectListItem> Round()
    //    {
    //        List<SelectListItem> listSelectListResult = new List<SelectListItem>();
    //        for (int i = 1; i < 5; i++)
    //        {
    //            SelectListItem selectListResult = new SelectListItem()
    //            {
    //                Text = i.ToString(),
    //                Value = i.ToString(),
    //                Selected = (i == 0 ? true : false)
    //            };
    //            listSelectListResult.Add(selectListResult);
    //        }
    //        return listSelectListResult;
    //    }

    //    //dalyviu listboxui
    //    private List<SelectListItem> Members(int id = 0)
    //    {
    //        List<SelectListItem> listSelectListResult = new List<SelectListItem>();
    //        foreach (var item in db.ParticipantMens)
    //        {
    //            SelectListItem selectListResult = new SelectListItem()
    //            {
    //                Text = item.NameSurname,
    //                Value = item.ParticipantMenId.ToString(),
    //                Selected = (item.ParticipantMenId == id ? true : false)
    //            };
    //            listSelectListResult.Add(selectListResult);
    //        }

    //        return listSelectListResult;
    //    }

    //    // GET: Test
    //    public ActionResult Index()
    //    {
    //        var query_all = db.MenResults.GroupBy(g => g.ParticipantMenId).Select(g => new FinalAllParticipantsInfoVM
    //        {
    //            ID = g.Key,//irasomas Dalyvio ID
    //            NameSurname = db.ParticipantMens.Where(p => p.ParticipantMenId == g.Key).FirstOrDefault().NameSurname, //irasomas dalyvio VArdasPavarde
    //            Win = g.Count(i => i.Piercing == 3),
    //            PiercingTotal = g.Sum(s => s.Piercing),
    //            ReceivedTotal = g.Sum(s => s.Received)
    //        })
    //            .OrderByDescending(o => o.Win).ThenByDescending(o => o.PiercingTotal - o.ReceivedTotal).ToList();//surikiuoja pagal pergales, o tada pagal idurtu praleistu santyki

    //        //return View(db.ParticipantMens.ToList());
    //        return View(query_all);
    //    }




    //    //###############################################################################




























    //    // GET: ParticipantMen
    //    public ActionResult Index()
    //    {
    //        //var query = (from r in db.MenResults into rr select NameSurname from db.ParticipantMens where ParticipantMenId = r.ParticipantMenId as 'Home', Piercing, Received,
    //        //(select NameSurname from db.ParticipantMens where ParticipantMenId = r.ParticipantCompetingId) as 'Away')
                         

    //        return View(db.ParticipantMens.ToList());
    //    }

    //    // GET: ParticipantMen/Details/5
    //    public ActionResult Details(int? id)
    //    {

    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        ParticipantMen participantMen = db.ParticipantMens.Find(id);
    //        if (participantMen == null)
    //        {
    //            return HttpNotFound();
    //        }

    //        var query = (from m in db.MenResults
    //                     join p in db.ParticipantMens on m.ParticipantMenId equals p.ParticipantMenId
    //                     join pp in db.ParticipantSecondMens on m.ParticipantCompetingId equals pp.ParticipantSecondMenId
    //                     where p.ParticipantMenId == id
    //                     select new ParticipantsInfoViewModel() { FirstParticipantNameSurname = p.NameSurname, SecondParticipantNameSurname = pp.NameSurname, Piercing = m.Piercing, Received = m.Received, Round = m.Round }).ToList();

    //        return View(query);
    //    }

    //    // GET: ParticipantMen/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: ParticipantMen/Create
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create([Bind(Include = "ParticipantMenId,NameSurname")] ParticipantMen participantMen)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.ParticipantMens.Add(participantMen);
    //            db.SaveChanges();

    //            //i antra db raso
    //            ParticipantSecondMen newObj = new ParticipantSecondMen()
    //            {
    //                NameSurname = participantMen.NameSurname,
    //                ParticipantSecondMenId = participantMen.ParticipantMenId,
    //                ServiceUnit = participantMen.ServiceUnit
    //            };

    //            db.ParticipantSecondMens.Add(newObj);
    //            db.SaveChanges();
    //            return RedirectToAction("Create");
    //        }

    //        return View(participantMen);
    //    }

    //    // GET: ParticipantMen/Edit/5
    //    public ActionResult Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        ParticipantMen participantMen = db.ParticipantMens.Find(id);
    //        if (participantMen == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(participantMen);
    //    }

    //    // POST: ParticipantMen/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit([Bind(Include = "ParticipantMenId,NameSurname")] ParticipantMen participantMen)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Entry(participantMen).State = EntityState.Modified;
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        return View(participantMen);
    //    }

    //    // GET: ParticipantMen/Delete/5
    //    public ActionResult Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        ParticipantMen participantMen = db.ParticipantMens.Find(id);

    //        if (participantMen == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(participantMen);
    //    }

    //    // POST: ParticipantMen/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(int id)
    //    {
    //        ParticipantMen participantMen = db.ParticipantMens.Find(id);
    //        db.ParticipantMens.Remove(participantMen);
    //        db.SaveChanges();

    //        ParticipantSecondMen participant2Men = db.ParticipantSecondMens.Find(id);
    //        db.ParticipantSecondMens.Remove(participant2Men);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    //}
}
