using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fektavimasis.Models.ViewModel
{
    public class ParticipantListWithInfoViewModel
    {
        public virtual int ParticipantMenId { get; set; }
        public virtual string NameSurname { get; set; }
        public virtual int Wins { get; set; }
        public virtual int Piercing { get; set; }
        public virtual int Received { get; set; }
    }
}