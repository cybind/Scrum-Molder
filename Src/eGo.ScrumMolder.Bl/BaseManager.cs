using eGo.ScrumMolder.Data;
using eGo.ScrumMolder.Data.Interface;
using log4net;

namespace eGo.ScrumMolder.Bl
{
    public class BaseManager
    {
        internal readonly IDalContext _context;
        internal readonly ILog _logger;


        public BaseManager()
        {
            Ioc.RegisterServices();
            _context = Ioc.Instance.GetService(typeof(IDalContext)) as IDalContext;
            _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}