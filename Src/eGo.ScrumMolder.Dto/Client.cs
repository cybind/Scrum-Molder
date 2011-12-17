using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace eGo.ScrumMolder.Dto
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public virtual List<Project> Projects { get; set; }
    }
}
