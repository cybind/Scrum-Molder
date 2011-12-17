using System;
using System.Collections.Generic;
using System.Linq;
using EmitMapper;
using eGo.ScrumMolder.Bl;
using eGo.ScrumMolder.Bl.Interface;
using eGo.ScrumMolder.Dto.Bugs;
using eGo.ScrumMolder.Web.Models;
using Model = eGo.ScrumMolder.Web.Models;
using System.IO;
using Bug = eGo.ScrumMolder.Web.Models.Bug;

namespace eGo.ScrumMolder.Web
{
    public class Repository : IRepository
    {

        public User LogOn(string username, string password)
        {
            var manager = Ioc.Instance.GetService(typeof(IUserManager)) as IUserManager;
            var dtoUser = manager.ValidateUser(username, password);

            var mUser = ObjectMapperManager.DefaultInstance.GetMapper<Dto.User,Model.User>().Map(dtoUser);

            return mUser;
        }

        public User Register(Model.RegisterModel registerModel)
        {
            var manager = Ioc.Instance.GetService(typeof(IUserManager)) as IUserManager;

            var dtoUser = ObjectMapperManager.DefaultInstance.GetMapper<Model.RegisterModel, Dto.User>().Map(registerModel);

            var savedUser = manager.Save(dtoUser);
            var result = ObjectMapperManager.DefaultInstance.GetMapper<Dto.User, Models.User>().Map(savedUser);

            return result;
        }

        public List<Model.Client> GetClients()
        {
            var manager = Ioc.Instance.GetService(typeof(IClientManager)) as IClientManager;

            //var clients = new List<Model.Client>();

            var dtoClients = manager.GetAll().ToList();

            var clients = ObjectMapperManager.DefaultInstance.GetMapper<List<Dto.Client>, List<Model.Client>>().Map(dtoClients);

            return clients;
        }

        public List<Model.Project> GetProjects()
        {
            var manager = Ioc.Instance.GetService(typeof(IProjectManager)) as IProjectManager;
            
            var dtoProjects = manager.GetAll().ToList();
            var projects = ObjectMapperManager.DefaultInstance.GetMapper<List<Dto.Project>, List<Model.Project>>().Map(dtoProjects);

            return projects;
        }

        public List<Model.Project> GetProjects(Guid clientId)
        {
            var manager = Ioc.Instance.GetService(typeof(IClientManager)) as IClientManager;
            
            var dtoProjects = manager.Get(clientId).Projects.ToList();
            var projects = ObjectMapperManager.DefaultInstance.GetMapper<List<Dto.Project>, List<Model.Project>>().Map(dtoProjects);

            return projects;
        }

        public bool SaveDailyScrum(Model.DailyScrum dailyScrum)
        {
            var manager = Ioc.Instance.GetService(typeof(IDailyScrumManager)) as IDailyScrumManager;

            var dtoDailyScrum = ObjectMapperManager.DefaultInstance.GetMapper<Model.DailyScrum, Dto.DailyScrum>().Map(dailyScrum);

            //if(dailyScrum.DailyProjectScrums!=null)
            //    for (var i = 0; i < dailyScrum.DailyProjectScrums.Count; i++)
            //    {
            //        dtoDailyScrum.DailyProjectScrums[i].Project = new Dto.Project();
            //        dtoDailyScrum.DailyProjectScrums[i].Project.Id = dailyScrum.DailyProjectScrums[i].ProjectId;
            //    }

            return manager.Save(dtoDailyScrum);
        }

        public List<Bug> GetBugsByProject(Guid projectId)
        {
            var manager = Ioc.Instance.GetService(typeof(IBugManager)) as IBugManager;

            var dtoBugs = manager.GetAll().ByProject(projectId);
            var bugs = ObjectMapperManager.DefaultInstance.GetMapper<List<Dto.Bugs.Bug>, List<Model.Bug>>().Map(dtoBugs.ToList());

            return bugs;
        }

        public bool SaveBug(Bug bug, Guid? modifiedById = null)
        {
            var manager = Ioc.Instance.GetService(typeof(IBugManager)) as IBugManager;

            var dtoBug = ObjectMapperManager.DefaultInstance.GetMapper<Models.Bug, Dto.Bugs.Bug>().Map(bug);

            return manager.Save(dtoBug, modifiedById) != null;
        }

        public List<User> GetUsers()
        {
            var manager = Ioc.Instance.GetService(typeof(IUserManager)) as IUserManager;

            var dtoUsers = manager.GetAll();
            var users = ObjectMapperManager.DefaultInstance.GetMapper<List<Dto.User>, List<Model.User>>().Map(dtoUsers.ToList());

            return users;
        }

        public Bug GetBug(int bugId)
        {
            var manager = Ioc.Instance.GetService(typeof(IBugManager)) as IBugManager;

            var dtoBug = manager.Get(bugId);

            var modelBug = ObjectMapperManager.DefaultInstance.GetMapper<Dto.Bugs.Bug, Models.Bug>().Map(dtoBug);

            return modelBug;
        }

        public Stream GetReport(DateTime dateFrom, DateTime dateTo)
        {
            var manager = Ioc.Instance.GetService(typeof(IReportManager)) as IReportManager;
            
            return manager.GetReport(dateFrom,dateTo);
        }
    }
}