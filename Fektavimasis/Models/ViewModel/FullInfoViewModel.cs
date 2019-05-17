using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fektavimasis.Models.ViewModel
{
    public class FullInfoViewModel
    {
        public virtual string Participant { get; set; }
        public virtual int Piercing { get; set; }
        public virtual int Received { get; set; }
        public virtual string SecondParticipant { get; set; }
        public virtual int Round { get; set; }
    }
}