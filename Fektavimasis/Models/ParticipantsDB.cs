using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Fektavimasis.Models;

namespace Fektavimasis.Models
{
    public class ParticipantsDB:DbContext
    {
        public ParticipantsDB():base("name=database")
        {

        }

        public DbSet<ParticipantMen> ParticipantMens { get; set; }
        public DbSet<ParticipantSecondMen> ParticipantSecondMens { get; set; }
        public DbSet<ParticipantWoman> ParticipantWomen { get; set; }
        public DbSet<MenResult> MenResults { get; set; }
        public DbSet<WomanResult> WomenResults { get; set; }


        public DbSet<Dalyviai> Dalyviai { get; set; }
        public DbSet<Matchup> Matchups { get; set; }
        public DbSet<MatchupEntries> MatchupEntries { get; set; }

    }
}