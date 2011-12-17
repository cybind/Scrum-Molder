using System;

namespace eGo.ScrumMolder.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        //int SaveChanges();
    }

    public interface IDalContext : IUnitOfWork
    {
        IDailyScrumRepository DailyScrums { get; }
        IBugRepository Bugs { get; }
        IBugHistoryRepository BugHistories { get; }
        IProjectRepository Projects { get; }
        IUserRepository Users { get; }
    }
}