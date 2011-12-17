using System;
using System.Collections.Generic;
using eGo.ScrumMolder.Web.Models;
using System.IO;

namespace eGo.ScrumMolder.Web
{
    public interface IRepository
    {
        #region Common
        
        User LogOn(string username, string password);
        User Register(RegisterModel registerModel);
        List<Client> GetClients();
        List<Project> GetProjects();
        List<Project> GetProjects(Guid clientId);
        List<User> GetUsers();
        
        #endregion

        #region Reporting
        
        Stream GetReport(DateTime dateFrom, DateTime dateTo);
        
        #endregion

        #region Daily Scrum

        bool SaveDailyScrum(DailyScrum dailyScrum);

        #endregion

        #region Bugtracking

        List<Bug> GetBugsByProject(Guid projectId);
        bool SaveBug(Bug bug, Guid? modifiedById = null);
        Bug GetBug(int bugId);

        #endregion
    }
}
