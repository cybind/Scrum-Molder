using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eGo.ScrumMolder.Bl.Interface;
using eGo.ScrumMolder.Data;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Bl
{
    public class ProjectManager : BaseManager, IProjectManager
    {
        public IEnumerable<Project> GetAll()
        {
            try
            {
                return _context.Projects.All();
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Exception: {0}", ex.ToString());
                return null;
            }
        }

        public Project Get(Guid projectId)
        {
            try
            {
                return _context.Projects.Filter(p=>p.Id == projectId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Exception: {0}", ex.ToString());
                return null;
            }
        }

        public bool Save(Project project)
        {
            try
            {
                if (!_context.Projects.EntityChange(project))
                    _context.Projects.Create(project);
                else
                    _context.Projects.Update(project);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Exception: {0}", ex.ToString());
                return false;
            }

            return true;
        }
        
    }
}
