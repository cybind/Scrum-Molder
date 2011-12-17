using System;
using System.Web.Mvc;
using eGo.ScrumMolder.Dto;
using DailyProjectScrum = eGo.ScrumMolder.Web.Models.DailyProjectScrum;
using DailyScrum = eGo.ScrumMolder.Web.Models.DailyScrum;
using User = eGo.ScrumMolder.Web.Models.User;

namespace eGo.ScrumMolder.Web.Controllers
{

    [RequiresAuthentication]
    public class HomeController : BaseController
    {
        public HomeController(IRepository repository, ISession session) : base(repository, session) { }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Scrum Molder!";

            return View();
        }

        public ActionResult About()

        {
            return View();
        }

        [HttpGet]
        public ActionResult DailyScrum()
        {
            var model = new DailyScrum();
            //model.DailyProjectScrums.Add(new DailyProjectScrum{ });
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DailyScrum(DailyScrum model)
        {
            ViewBag.Clients = _repository.GetClients().ToDropDownList();
            ViewBag.Projects = _repository.GetProjects().ToDropDownList();

            if (!ModelState.IsValid)
                return View(model);

            model.CreateDate = DateTime.Now;
            model.Id = Guid.NewGuid();
            model.User = _session.CurrentUser;

            var result = _repository.SaveDailyScrum(model);

            if (result) ViewBag.SuccessMessage = Resource.DailyScrumSaveSuccessMessage;

            return View(new DailyScrum());
        }

        [HttpGet]
        public ActionResult Projects(Guid clientId)
        {
            var model = _repository.GetProjects(clientId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DailyProjectScrum()
        {
            ViewBag.Clients = _repository.GetClients().ToDropDownList();
            ViewBag.Projects = _repository.GetProjects().ToDropDownList();
            var model = new DailyProjectScrum { Id = Guid.NewGuid() };
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            throw new NotImplementedException();
        }

    }
}
