using System;
using eGo.ScrumMolder.Data.Interface;

namespace eGo.ScrumMolder.Data
{
    public class DalContext : IDalContext
    {
        private IDailyScrumRepository _dailyScrums;
        private IBugRepository _bugs;
        private IBugHistoryRepository _bugHistories;
        private IProjectRepository _projects;
        private IUserRepository _users;

        public DalContext()
        {
            Ioc.RegisterServices();
        }
        public void Dispose()
        {
            if (_dailyScrums != null)
                _dailyScrums.Dispose();
            if (_bugs != null)
                _bugs.Dispose();
            if (_projects != null)
                _projects.Dispose();
            if (_users != null)
                _users.Dispose();
           
            GC.SuppressFinalize(this);
        }
      
        public IDailyScrumRepository DailyScrums
        {
            get { return _dailyScrums ?? (_dailyScrums = Ioc.Instance.GetService(typeof(IDailyScrumRepository)) as IDailyScrumRepository); }
        }

        public IBugRepository Bugs
        {
            get { return _bugs ?? (_bugs = Ioc.Instance.GetService(typeof (IBugRepository)) as IBugRepository); }
        }

        public IBugHistoryRepository BugHistories
        {
            get { return _bugHistories ?? (_bugHistories = Ioc.Instance.GetService(typeof(IBugHistoryRepository)) as IBugHistoryRepository); }
        }

        public IProjectRepository Projects
        {
            get { return _projects ?? (_projects = Ioc.Instance.GetService(typeof(IProjectRepository)) as IProjectRepository); }
        }

        public IUserRepository Users
        {
            get { return _users ?? (_users = Ioc.Instance.GetService(typeof(IUserRepository)) as IUserRepository); }
        }
    }
}