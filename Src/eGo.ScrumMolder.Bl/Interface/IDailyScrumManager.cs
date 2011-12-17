using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Bl.Interface
{
    public interface IDailyScrumManager
    {
        bool Save(DailyScrum dailyScrum);
    }
}