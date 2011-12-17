using System.Web.Mvc;
using MvcMiniProfiler;

namespace eGo.ScrumMolder.Web
{
    public class ProfilingViewEngine : IViewEngine
    {
        class WrappedView : IView
        {
            IView wrapped;
            string name;
            bool isPartial;

            public WrappedView(IView wrapped, string name, bool isPartial)
            {
                this.wrapped = wrapped;
                this.name = name;
                this.isPartial = isPartial;
            }

            public void Render(ViewContext viewContext, System.IO.TextWriter writer)
            {
                using (MiniProfiler.Current.Step("Render " + (isPartial ? "partial" : "") + ": " + name))
                {
                    wrapped.Render(viewContext, writer);
                }
            }
        }

        IViewEngine wrapped;

        public ProfilingViewEngine(IViewEngine wrapped)
        {
            this.wrapped = wrapped;
        }

        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var found = wrapped.FindPartialView(controllerContext, partialViewName, useCache);
            if (found != null && found.View != null)
            {
                found = new ViewEngineResult(new WrappedView(found.View, partialViewName, isPartial: true), this);
            }
            return found;
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var found = wrapped.FindView(controllerContext, viewName, masterName, useCache);
            if (found != null && found.View != null)
            {
                found = new ViewEngineResult(new WrappedView(found.View, viewName, isPartial: false), this);
            }
            return found;
        }

        public void ReleaseView(ControllerContext controllerContext, IView view)
        {
            wrapped.ReleaseView(controllerContext, view);
        }
    }
}