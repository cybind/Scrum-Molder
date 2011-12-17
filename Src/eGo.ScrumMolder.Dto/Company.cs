using System;
using System.Collections.Generic;

namespace eGo.ScrumMolder.Dto
{
    public class Company
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }

        public virtual ICollection<Company> ChildCompanies { get; set; }
    }
}