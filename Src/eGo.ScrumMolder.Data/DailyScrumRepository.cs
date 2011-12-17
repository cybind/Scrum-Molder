using System.Data;
using eGo.ScrumMolder.Data.Interface;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Data
{
    public class DailyScrumRepository : Repository<DailyScrum>, IDailyScrumRepository
    {
        public DailyScrumRepository(ScrumMolderContext context) : base(context)
        {
            
        }

        public override DailyScrum Create(DailyScrum dailyScrum)
        {
            var newEntry = DbSet.Add(dailyScrum);
            Context.Entry(dailyScrum.User).State = EntityState.Unchanged;

            if (Context.SaveChanges() == 0)
                return null;

            return newEntry;
        }
    }
}