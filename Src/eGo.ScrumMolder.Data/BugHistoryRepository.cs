using System;
using eGo.ScrumMolder.Data.Interface;
using eGo.ScrumMolder.Dto.Bugs;

namespace eGo.ScrumMolder.Data
{
    public class BugHistoryRepository : Repository<BugHistory>, IBugHistoryRepository
    {
        public BugHistoryRepository(ScrumMolderContext context) : base(context) { }

        
    }
}