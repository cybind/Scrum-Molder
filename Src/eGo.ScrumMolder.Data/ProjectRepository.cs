using eGo.ScrumMolder.Data.Interface;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Data
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ScrumMolderContext context) : base(context) { }
    }
}