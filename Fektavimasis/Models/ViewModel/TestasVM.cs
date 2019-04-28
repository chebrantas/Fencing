using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fektavimasis.Models.ViewModel
{
    public class TestasVM
    {
        public string NameWinner { get; set; }
        public int NameLoserId { get; set; }
        public int Score { get; set; }
        public int MatchupRound { get; set; }
    }
}