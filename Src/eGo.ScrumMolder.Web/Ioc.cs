using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eGo.ScrumMolder.Bl;
using eGo.ScrumMolder.Bl.Interface;
using log4net;
using Ninject;


namespace eGo.ScrumMolder.Web
{
    public static class Ioc
    {
        private static IKernel kernel;
        private static object outerLock = new object();
        private static object innerLock = new object();

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
            Instance.Bind(typeof(ILog)).ToMethod(x => LogManager.GetLogger(x.Request.ParentContext.Plan.Type));
            Instance.Bind(typeof(IProjectManager)).To(typeof(ProjectManager));
            Instance.Bind(typeof(IClientManager)).To(typeof(ClientManager));
            Instance.Bind(typeof(IBugManager)).To(typeof(BugManager));
            Instance.Bind(typeof(IDailyScrumManager)).To(typeof(DailyScrumManager));
            Instance.Bind(typeof(IUserManager)).To(typeof(UserManager));
            Instance.Bind(typeof(IReportManager)).To(typeof(ReportManager));
            Instance.Bind(typeof(IPasswordHelper)).To(typeof(PasswordHelper));
        }
    }
}
