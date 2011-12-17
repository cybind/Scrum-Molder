using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eGo.ScrumMolder.Dto
{
    public class DailyScrum
    {
        [Key]
        public Guid Id { get; set; }

        public virtual User User { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual List<DailyProjectScrum> DailyProjectScrums { get; set; }
    }
}