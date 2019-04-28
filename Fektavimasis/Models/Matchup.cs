using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fektavimasis.Models
{
    public class Matchup
    {
        public int MatchupId { get; set; }
        public int WinnerId { get; set; }
        public string WinnerName { get; set; }
        public int MatchupRound { get; set; }
    }
}