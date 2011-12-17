using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using FluentEmail;
using eGo.ScrumMolder.Bl.Interface;
using eGo.ScrumMolder.Bl.Properties;
using eGo.ScrumMolder.Data;
using eGo.ScrumMolder.Data.Interface;
using eGo.ScrumMolder.Dto;
using log4net;

namespace eGo.ScrumMolder.Bl
{
    public class DailyScrumEmail
    {
        public List<DailyProjectScrumEmail> DailyProjectScrums { get; set; }

        public DailyScrumEmail()
        {
            DailyProjectScrums = new List<DailyProjectScrumEmail>();
        }
    }

    public class DailyProjectScrumEmail
    {
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public TimeSpan SpentTime { get; set; }
        public string WhatDoneLastTime { get; set; }
        public string WhatProblems { get; set; }
        public string WhatToDoNext { get; set; }
    }

    public class DailyScrumManager : BaseManager, IDailyScrumManager
    {
       
        private readonly IClientManager _clientManager;
        private readonly IProjectManager _projectManager;

        public DailyScrumManager(IClientManager clientManager, IProjectManager projectManager)
        {
            _clientManager = clientManager;
            _projectManager = projectManager;
        }

        public bool Save(DailyScrum dailyScrum)
        {
            try
            {
                DailyScrum ret = null;
                if (!_context.DailyScrums.EntityChange(dailyScrum))
                    ret = _context.DailyScrums.Create(dailyScrum);
                //else
                //   ret = _context.DailyScrums.Update(dailyScrum);
                

                if (ret == null)
                    return false;

                var templatePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                templatePath = templatePath.Remove(0, 6);

                var dailyScrumEmail = new DailyScrumEmail();
                foreach (var item in dailyScrum.DailyProjectScrums)
                {
                    var project = _projectManager.Get(item.ProjectId);
                    var client = _clientManager.GetByProjectId(item.ProjectId);
                    dailyScrumEmail.DailyProjectScrums.Add(
                        new DailyProjectScrumEmail
                        {
                            ClientName = client.Name,
                            ProjectName = project.Name,
                            SpentTime = item.SpentTime,
                            WhatDoneLastTime = item.WhatDoneLastTime,
                            WhatProblems = item.WhatProblems,
                            WhatToDoNext = item.WhatToDoNext
                        }
                    );
                }

                var email = Email
                    .From(dailyScrum.User.Email, dailyScrum.User.UserName)
                    .To(Settings.Default.NotificationEmail)
                    .BCC(dailyScrum.User.Email, dailyScrum.User.UserName)
                    .Subject(Resource.DailyScrumEmailSubgect)
                    .UsingTemplateFromFile(Path.Combine(templatePath, "EmailTemplates\\DailyScrum.cshtml"), dailyScrumEmail);

                email.Send();

            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Exception: {0}", ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get User Info from AD
        /// </summary>
        /// <returns>First param Email, Second - Display Name</returns>
        //private string[] GetAdUserEmail(string userName)
        //{
        //    //Get the username and domain information
        //    //var userName = Environment.UserName;
        //    var domainName = Environment.UserDomainName;

        //    _logger.InfoFormat("userName = {0}", userName);
        //    _logger.InfoFormat("Environment.UserDomainName = {0}", Environment.UserDomainName);

        //    var email = "";
        //    var displayname = "";

        //    string filter = string.Format("(&(ObjectClass={0})(sAMAccountName={1}))", "person", userName);
        //    string domain = domainName;

        //    var adRoot = new DirectoryEntry("LDAP://" + domain, null, null, AuthenticationTypes.Secure);
        //    var searcher = new DirectorySearcher(adRoot)
        //    {
        //        SearchScope = SearchScope.Subtree,
        //        ReferralChasing = ReferralChasingOption.All,
        //        Filter = filter
        //    };

        //    SearchResult result = searcher.FindOne();

        //    _logger.InfoFormat("SearchResult = {0}", result.ToString());

        //    DirectoryEntry directoryEntry = result.GetDirectoryEntry();

        //    _logger.InfoFormat("DirectoryEntry = {0}", directoryEntry.ToString());

        //    if (directoryEntry.Properties["mail"] != null)
        //        email = directoryEntry.Properties["mail"][0].ToString();
        //    else
        //    {
        //        _logger.ErrorFormat("User do not have email: {0}", userName);
        //    }

        //    if (directoryEntry.Properties["displayname"] != null)
        //        displayname = directoryEntry.Properties["displayname"][0].ToString();
        //    else
        //    {
        //        _logger.ErrorFormat("User do not have displayname: {0}", userName);
        //    }

        //    _logger.InfoFormat("email = {0}; displayname = {1}", email, displayname);

        //    return new[] { email, displayname };
        //}
    }
}
