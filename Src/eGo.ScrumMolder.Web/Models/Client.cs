using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace eGo.ScrumMolder.Web.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
    }
}