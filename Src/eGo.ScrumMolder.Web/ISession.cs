using eGo.ScrumMolder.Web.Models;

namespace eGo.ScrumMolder.Web
{
    public interface ISession
    {
        User CurrentUser { get; set; }
    }
}
