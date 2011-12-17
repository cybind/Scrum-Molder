using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace eGo.ScrumMolder.Dto.Bugs
{
    public class Bug : Issue
    {
        public int Reason { get; set; }

        public int Priority { get; set; }

        public int State { get; set; }

        public virtual Category Category { get; set; }

        public Guid? AssignedToId { get; set; }
        public virtual User AssignedTo { get; set; }

        public Guid CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public int? ImportedBugId { get; set; }

        public virtual List<BugHistory> History { get; set; }
        
    }


}