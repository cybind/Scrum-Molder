using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using eGo.ScrumMolder.Dto.Bugs;

namespace eGo.ScrumMolder.Web.Models
{
    public class Bug
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Bug short name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Bug description")]
        public string Description { get; set; }

        public Project Project { get; set; }

        public Guid CreatedById { get; set; }

        public User CreatedBy { get; set; }

        public Guid? AssignedToId { get; set; }

        [Display(Name = "Assigned to")]
        public User AssignedTo { get; set; }

        public int Reason { get; set; }

        public int Priority { get; set; }

        public int State { get; set; }

        [Display(Name = "Project category")]
        public Category Category { get; set; }

        public DateTime CreateDate { get; set; }

        public int? ImportedBugId { get; set; }

        public List<BugHistory> History { get; set; }
    }
}