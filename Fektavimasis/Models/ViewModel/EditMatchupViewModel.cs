using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Fektavimasis.Models.ViewModel
{
    public class EditMatchupViewModel
    {
        public int ID { get; set; }
        [DisplayName("First Participant")]
        public string FirstParticipantNameSurname { get; set; }
        public int Piercing { get; set; }
        public int Received { get; set; }
        [DisplayName("Second Participant")]
        public string SecondParticipantNameSurname { get; set; }
        public int Round { get; set; }
    }
}