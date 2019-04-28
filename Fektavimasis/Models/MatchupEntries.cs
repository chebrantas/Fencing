using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fektavimasis.Models
{
    public class MatchupEntries
    {
        public int MatchupEntriesId { get; set; }
        public int MatchupId { get; set; }
        public int PersonCompetingId { get; set; }
        public int Score { get; set; }
    }
}