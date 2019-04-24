using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fektavimasis.Models.ViewModel
{
    public class ParticipantsInfoViewModel
    {
        public virtual int ParticipantMenId { get; set; }
        public virtual string FirstParticipantNameSurname { get; set; }
        public virtual string SecondParticipantNameSurname { get; set; }
        public virtual int Piercing { get; set; }
        public virtual int Received { get; set; }
        public virtual int Round { get; set; }
    }
}