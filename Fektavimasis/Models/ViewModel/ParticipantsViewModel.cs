using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fektavimasis.Models.ViewModel
{
    public class ParticipantsViewModel
    {
        public IEnumerable<SelectListItem> Names { get; set; }
        public IEnumerable<SelectListItem> Result { get; set; }

    }
}