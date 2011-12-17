using System;
using System.ComponentModel.DataAnnotations;

namespace eGo.ScrumMolder.Dto.Bugs
{
    public class Issue
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual Project Project { get; set; }

        
       

    }
}