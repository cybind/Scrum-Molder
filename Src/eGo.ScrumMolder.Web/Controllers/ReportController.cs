using System;
using System.Web.Mvc;
using eGo.ScrumMolder.Web.Models;
using System.IO;

namespace eGo.ScrumMolder.Web.Controllers
{
    [RequiresAuthentication]
    public class ReportController : BaseController
    {
        public ReportController(IRepository repository, ISession session) : base(repository, session) { }

        public ActionResult Report()
        {
            var model = new EffortReport();
            model.EndDate = DateTime.Now;
            model.StartDate = DateTime.Now.AddDays(-7);
            return View(model);
        }

        [HttpPost]
        public ActionResult Report(EffortReport report)
        {
            var resReport = _repository.GetReport(report.StartDate, report.EndDate);
            if (!ModelState.IsValid)
                return View(report);
            if (resReport == null)
                return View(report);
            resReport.Position = 0;
            return File(resReport, "application/vnd.ms-excel", "timelog_projects_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".xls");
        }
    }
}