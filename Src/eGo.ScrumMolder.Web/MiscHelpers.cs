using log4net;

namespace eGo.ScrumMolder.Web
{
    public static class MiscHelpers
    {
        static ILog _logger;
        public static ILog Logger
        {
            get
            {
                if (_logger == null)
                    _logger = LogManager.GetLogger("UI Logger");
                return _logger;
            }
        }

    }
}