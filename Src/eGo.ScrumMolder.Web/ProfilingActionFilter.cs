using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using MvcMiniProfiler;

namespace eGo.ScrumMolder.Web
{
    public class ProfilingActionFilter : ActionFilterAttribute
    {
        const string stackKey = "ProfilingActionFilterStack";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var mp = MiniProfiler.Current;
            if (mp != null)
            {
                var stack = HttpContext.Current.Items[stackKey] as Stack<IDisposable>;
                if (stack == null)
                {
                    stack = new Stack<IDisposable>();
                    HttpContext.Current.Items[stackKey] = stack;
                }

                var prof = MiniProfiler.Current.Step("Controller: " + filterContext.Controller.ToString() + "." + filterContext.ActionDescriptor.ActionName);
                stack.Push(prof);

            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var stack = HttpContext.Current.Items[stackKey] as Stack<IDisposable>;
            if (stack != null && stack.Count > 0)
            {
                stack.Pop().Dispose();
            }
        }
    }
}