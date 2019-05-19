using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fektavimasis.Models.ViewModel
{
    public class FinalAllParticipantsInfoVM
    {
        public int ID { get; set; }
        public string NameSurname { get; set; }
        public int Win { get; set; }
        public int PiercingTotal { get; set; }
        public int ReceivedTotal { get; set; }
    }
}