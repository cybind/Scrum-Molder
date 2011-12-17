using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using eGo.ScrumMolder.Data;
using eGo.ScrumMolder.Data.Interface;

namespace eGo.ScrumMolder.Data
{
    public static class Ioc
    {
        private static IKernel kernel;
        private static object outerLock = new object();
        private static object innerLock = new object();

        private static bool _wasBind;

        public static IKernel Instance
        {
            get
            {
                lock (outerLock)
                {
                    if (kernel == null)
                    {
                        lock (innerLock)
                        {
                            kernel = new StandardKernel();
                        }
                    }
                }

                return kernel;
            }
        }

        public static void RegisterServices()
        {
            if (!_wasBind)
            {
                Instance.Bind(typeof (IDailyScrumRepository)).To(typeof (DailyScrumRepository));
                Instance.Bind(typeof (IBugRepository)).To(typeof (BugRepository));
                Instance.Bind(typeof(IBugHistoryRepository)).To(typeof(BugHistoryRepository));
                Instance.Bind(typeof (IProjectRepository)).To(typeof (ProjectRepository));
                Instance.Bind(typeof(IUserRepository)).To(typeof(UserRepository));

                _wasBind = true;
            }
        }

    }
}
