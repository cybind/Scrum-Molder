using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace eGo.ScrumMolder.Dto
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ClientId { get; set; }
    }
}
