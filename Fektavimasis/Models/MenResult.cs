using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fektavimasis.Models
{
    public class MenResult
    {
        public virtual int MenResultId { get; set; }
        public virtual int ParticipantMenId { get; set; }
        public virtual int ParticipantCompetingId { get; set; }
        public virtual int Piercing { get; set; }
        public virtual int Received { get; set; }
        public virtual int Round { get; set; }

    }
}