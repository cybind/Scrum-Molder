using System.Web.Mvc;
using log4net;

namespace eGo.ScrumMolder.Web
{
    public class RequiresAuthenticationAttribute : ActionFilterAttribute
    {
        protected static ILog _logger = LogManager.GetLogger("Authentication");
        private ISession _session = DependencyResolver.Current.GetService(typeof(ISession)) as ISession;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                filterContext.Result = new HttpUnauthorizedResult();

        }

    }
}