using System;
using System.Collections.Generic;
using System.Linq;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using eGo.ScrumMolder.Bl.Interface;
using eGo.ScrumMolder.Dto;
using eGo.ScrumMolder.Dto.Bugs;

namespace eGo.ScrumMolder.Bl
{
    public class BugManager : BaseManager, IBugManager
    {
        public IQueryable<Bug> GetAll()
        {
            try
            {
                return _context.Bugs.All();
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Exception: {0}", ex.ToString());
                return null;
            }
        }

        public Bug Save(Bug bug, Guid? modifiedById = null)
        {
            try
            {
                if (bug.Id != 0)
                {
                    //var oldBug = _context.Bugs.All().Single(b => b.Id == bug.Id);
                    var oldBug = new Bug();

                    var history = new BugHistory { Id = Guid.NewGuid(), ModifiedBy = null, ModifiedDate = DateTime.Now, ModifiedById = modifiedById.Value };

                    if (oldBug.Name != bug.Name)  history.Name = oldBug.Name;

                    
                    if (bug.History == null) bug.History = new List<BugHistory>();
                    bug.History.Add(history);
                    _context.Bugs.Update(bug);
                }
                else
                    bug = _context.Bugs.Create(bug);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Exception: {0}", ex.ToString());
                return null;
            }

            return bug;
        }

        public Bug Get(int bugId)
        {
            try
            {
                return _context.Bugs.All().SingleOrDefault(b=> b.Id == bugId);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Exception: {0}", ex.ToString());
                return null;
            }
        }
    }

    public static class BugFilters
    {
        public static  IQueryable<Bug> ByProject(this IQueryable<Bug> query, Guid projectId)
        {
            return query.Where(q => q.Project.Id == projectId);
        }
    }
}