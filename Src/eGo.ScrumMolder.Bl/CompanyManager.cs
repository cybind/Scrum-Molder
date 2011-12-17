using System;
using System.Collections.Generic;
using System.Linq;
using eGo.ScrumMolder.Bl.Interface;
using eGo.ScrumMolder.Data;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Bl
{
    public class CompanyManager : ICompanyManager
    {
        private readonly ScrumMolderContext _context = new ScrumMolderContext();

        public IEnumerable<Company> GetAll()
        {
            try
            {
                return _context.Companies;
            }
            catch (Exception ex)
            {
                //logger.ErrorFormat("Exception: {0}", ex.ToString());
                return null;
            }
        }

        public Company Get(Guid companyId)
        {
            try
            {
                return _context.Companies.SingleOrDefault(c=>c.Id == companyId);
            }
            catch (Exception ex)
            {
                //logger.ErrorFormat("Exception: {0}", ex.ToString());
                return null;
            }
        }

        public bool Create(Company company)
        {
            try
            {
                _context.Companies.Add(company);
                if (_context.SaveChanges() == 0)
                    return false;
                
            }
            catch (Exception ex)
            {
                //logger.ErrorFormat("Exception: {0}", ex.ToString());
                return false;
            }
            return true;
        }
    }
}