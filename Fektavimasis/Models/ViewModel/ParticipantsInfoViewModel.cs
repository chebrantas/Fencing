using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Fektavimasis.Models.ViewModel
{
    public class ParticipantsInfoViewModel
    {
        [DisplayName("ID")]
        public virtual int MenResultsId { get; set; }
        [DisplayName("First Participant")]
        public virtual string FirstParticipantNameSurname { get; set; }
        public virtual int Piercing { get; set; }
        public virtual int Received { get; set; }
        [DisplayName("Second Participant")]
        public virtual string SecondParticipantNameSurname { get; set; }
        public virtual int Round { get; set; }
    }
}