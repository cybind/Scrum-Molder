using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eGo.ScrumMolder.Dto.Bugs;
using eGo.ScrumMolder.Data;
using eGo.ScrumMolder.Web.Helpers;
using eGo.ScrumMolder.Web.Models;
using Bug = eGo.ScrumMolder.Web.Models.Bug;
using Project = eGo.ScrumMolder.Dto.Project;

namespace eGo.ScrumMolder.Web.Controllers
{ 
    public class BugController : BaseController
    {
        public BugController(IRepository repository, ISession session) : base(repository, session)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Clients = _repository.GetClients().ToDropDownList();
            ViewBag.Projects = _repository.GetProjects().ToDropDownList();

            return View();
        }


        [HttpGet]
        public ActionResult ProjectBugs(Guid projectId)
        {
            var model = _repository.GetBugsByProject(projectId);

            //ViewBag.Categories = (new List<Category> { new Category { Id = Guid.NewGuid(), Name = "Test cat", Project = new Project{ Id = projectId}}}).ToDropDownList();
            //ViewBag.Users = _repository.GetUsers().ToDropDownList();

            return PartialView("_BugList", model);
        }

        [HttpPost]
        public ActionResult SaveBug(Models.Bug bug)
        {
            //var , Guid? modifiedById
            if (bug.Id == 0)
            {
                bug.CreateDate = DateTime.Now;
                bug.CreatedById = _session.CurrentUser.Id;
                if (bug.AssignedToId == Guid.Empty) bug.AssignedToId = _session.CurrentUser.Id;
            }
            
            if (bug.Category.Id == Guid.Empty) bug.Category = null;


            var response = _repository.SaveBug(bug, _session.CurrentUser.Id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditBug(Guid projectId, int? bugId)
        {
            var model = new EditBugModel();
            model.Bug = bugId.HasValue ? _repository.GetBug(bugId.Value) : 
                                         new Bug { Project = new Models.Project { Id = projectId }, AssignedTo = new User() };
            
            model.Users = _repository.GetUsers().ToDropDownList();
            model.Categories = new List<SelectListItem>();
            model.Priorities = BugHelper.GetPrioritiesList();
            model.States = BugHelper.GetStatesList();
            model.Reasons = BugHelper.GetReasonList();
           

            return PartialView("EditBugPartial", model);
        }
    }
}