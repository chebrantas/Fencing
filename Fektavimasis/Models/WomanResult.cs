using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fektavimasis.Models
{
    public class WomanResult
    {
        public virtual int WomanResultId { get; set; }
        public virtual int ParticipantWomanId { get; set; }
        public virtual int Piercing { get; set; }
        public virtual int Received { get; set; }
        public virtual int ParticipantCompetingId { get; set; }
        public virtual int Round { get; set; }
    }
}