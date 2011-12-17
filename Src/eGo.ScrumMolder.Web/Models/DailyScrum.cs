using System;
using System.Collections.Generic;

namespace eGo.ScrumMolder.Web.Models
{
    public class DailyScrum
    {
        public Guid Id { get; set; }

        public User User { get; set; }

        public DateTime CreateDate { get; set; }

        public List<DailyProjectScrum> DailyProjectScrums { get; set; }

        public DailyScrum()
        {
            DailyProjectScrums = new List<DailyProjectScrum>();
        }
    }
}