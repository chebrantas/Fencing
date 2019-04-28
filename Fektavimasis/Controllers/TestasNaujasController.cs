using Fektavimasis.Models;
using Fektavimasis.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fektavimasis.Controllers
{
    public class TestasNaujasController : Controller
    {
        private ParticipantsDB db = new ParticipantsDB();

        // GET: TestasNaujas
        public ActionResult Index()
        {
            var query = (from m in db.MenResults
                         join p in db.ParticipantMens on m.ParticipantMenId equals p.ParticipantMenId
                         join pp in db.ParticipantSecondMens on m.ParticipantCompetingId equals pp.ParticipantSecondMenId
                         where p.ParticipantMenId == 1
                         select new ParticipantsInfoViewModel() { FirstParticipantNameSurname = p.NameSurname, SecondParticipantNameSurname = pp.NameSurname, Piercing = m.Piercing, Received = m.Received, Round = m.Round }).ToList();

            var query1 = (from ma in db.Matchups
                         join d in db.Dalyviai on ma.WinnerId equals d.DalyviaiId
                         join me in db.MatchupEntries on ma.MatchupId equals me.MatchupId
                         select new TestasVM() { NameWinner = ma.WinnerName, NameLoserId = me.PersonCompetingId, MatchupRound = ma.MatchupRound, Score = me.Score }).ToList();
                        
            return View(query1);
        }
    }
}