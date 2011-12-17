using System.Web;
using System.Web.Mvc;
using log4net;

namespace eGo.ScrumMolder.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ISession _session;
        protected readonly IRepository _repository;
        protected ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BaseController(IRepository repository, ISession session)
        {
            _repository = repository;
            _session = session;
        }

        protected override void OnException(ExceptionContext filterContext)
        {

            if (filterContext == null || filterContext.Exception == null)
                Logger.Error("MVC on exception fired no exception or filter.");
            else
                Logger.ErrorFormat("MVC on exception fired: {0}.", filterContext.Exception);

            base.OnException(filterContext);
        }

        [ValidateInput(false)]
        protected override void Execute(System.Web.Routing.RequestContext requestContext)
        {
            requestContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            base.Execute(requestContext);
        }

    }
}
