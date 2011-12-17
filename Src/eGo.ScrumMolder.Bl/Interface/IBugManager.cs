using System;
using System.Collections.Generic;
using System.Linq;
using eGo.ScrumMolder.Dto;
using eGo.ScrumMolder.Dto.Bugs;

namespace eGo.ScrumMolder.Bl.Interface
{
    public interface IBugManager
    {
        IQueryable<Bug> GetAll();

        Bug Save(Bug bug, Guid? modifiedById = null);
        Bug Get(int bugId);
    }
}