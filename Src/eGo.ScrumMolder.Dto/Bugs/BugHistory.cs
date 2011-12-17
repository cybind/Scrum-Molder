using System;
using System.ComponentModel.DataAnnotations;

namespace eGo.ScrumMolder.Dto.Bugs
{
    public class BugHistory : History
    {
        public int? Reason { get; set; }

        public int? State { get; set; }

        public int? ImportedBugId { get; set; }
        
        public Guid? AssignedToId { get; set; }
        public virtual User AssignedTo { get; set; }
        
    }
}