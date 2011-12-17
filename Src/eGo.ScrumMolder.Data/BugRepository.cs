using System.Data;
using eGo.ScrumMolder.Data.Interface;
using eGo.ScrumMolder.Dto;
using eGo.ScrumMolder.Dto.Bugs;

namespace eGo.ScrumMolder.Data
{
    public class BugRepository : Repository<Bug>,  IBugRepository 
    {
        public BugRepository(ScrumMolderContext context) : base(context) { }

        public override Bug Create(Bug bug)
        {
            var newEntry = DbSet.Add(bug);
            Context.Entry(bug.Project).State = EntityState.Unchanged;

            if (bug.Category != null) Context.Entry(bug.Category).State = EntityState.Unchanged;

            if (Context.SaveChanges() == 0)
                return null;

            return newEntry;
        }
    }
}