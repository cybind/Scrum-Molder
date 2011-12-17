using System.Collections.Generic;
using System.Web.Mvc;

namespace eGo.ScrumMolder.Web.Models
{
    public class EditBugModel
    {
        public Bug Bug { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public List<SelectListItem> Users { get; set; }

        public List<SelectListItem> Priorities { get; set; }

        public List<SelectListItem> States { get; set; }

        public List<SelectListItem> Reasons { get; set; }
    }
}