using System;
using System.ComponentModel.DataAnnotations;

namespace eGo.ScrumMolder.Dto
{
    public class DailyProjectScrum
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid ProjectId { get; set; }

        public Guid DailyScrumId { get; set; }

        public virtual Project Project { get; set; }

        public DateTime? UpdateDate { get; set; }
        
        // In minutes
        public TimeSpan SpentTime { get; set; }

        public string WhatDoneLastTime { get; set; }

        public string WhatProblems { get; set; }

        public string WhatToDoNext { get; set; }
    }
}
