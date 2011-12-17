using System;
using System.Collections.Generic;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Bl.Interface
{
    public interface IProjectManager
    {
        IEnumerable<Project> GetAll();

        Project Get(Guid projectId);

        bool Save(Project project);
        
    }
}