using System;
using System.Collections.Generic;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Bl.Interface
{
    public interface ICompanyManager
    {
        IEnumerable<Company> GetAll();

        Company Get(Guid companyId);

        bool Create(Company company);


    }
}