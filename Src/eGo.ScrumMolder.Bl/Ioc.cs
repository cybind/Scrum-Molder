using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using eGo.ScrumMolder.Bl.Interface;
using eGo.ScrumMolder.Data;
using eGo.ScrumMolder.Data.Interface;

namespace eGo.ScrumMolder.Bl
{
    public static class Ioc
    {
        private static IKernel kernel;
        private static object outerLock = new object();
        private static object innerLock = new object();

        //only for first load
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
                Instance.Bind(typeof (IProjectManager)).To(typeof (ProjectManager));
                Instance.Bind(typeof (IClientManager)).To(typeof (ClientManager));
                Instance.Bind(typeof (IDailyScrumManager)).To(typeof (DailyScrumManager));
                Instance.Bind(typeof (IDalContext)).To(typeof (DalContext));
                
                _wasBind = true;
            }
        }

    }
}
