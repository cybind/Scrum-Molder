using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using eGo.ScrumMolder.Web.Models;

namespace eGo.ScrumMolder.Web
{
    public class SessionHelper : ISession
    {
        public enum SessionVariable
        {
            CurrentUser
        }

        private static HttpSessionState Session
        {
            get
            {
                // HttpContext can be null when the website is shutting down
                return HttpContext.Current != null ? HttpContext.Current.Session : null;
            }
        }

        public User CurrentUser
        {
            get
            {
                var result = Session[SessionVariable.CurrentUser.ToString()];
                if (result == null)
                    this.CurrentUser = new User();

                return Session[SessionVariable.CurrentUser.ToString()] != null
                           ? Session[SessionVariable.CurrentUser.ToString()] as User
                           : null;
            }
            set
            {
                Session[SessionVariable.CurrentUser.ToString()] = value;
            }
        }

        public static void Clear()
        {
            Session.Clear();
            Session.Abandon();
        }
       
    }
}