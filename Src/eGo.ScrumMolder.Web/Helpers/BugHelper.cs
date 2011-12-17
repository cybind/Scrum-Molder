using System.Collections.Generic;
using System.Web.Mvc;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Web.Helpers
{
    public static class BugHelper
    {
         public static List<SelectListItem> GetPrioritiesList()
         {
             var list = new List<SelectListItem>();
             list.Add(new SelectListItem{ Text = "Critical", Value = Enums.Priority.Critical.ToString()});
             list.Add(new SelectListItem { Text = "Major", Value = Enums.Priority.Major.ToString() });
             list.Add(new SelectListItem { Text = "Minor", Value = Enums.Priority.Minor.ToString() });
             return list;
         }

         public static List<SelectListItem> GetStatesList()
         {
             var list = new List<SelectListItem>();
             list.Add(new SelectListItem { Text = "Active", Value = Enums.State.Active.ToString() });
             list.Add(new SelectListItem { Text = "Resolved", Value = Enums.State.Resolved.ToString() });
             
             return list;
         }

         public static List<SelectListItem> GetReasonList()
         {
             var list = new List<SelectListItem>();
             list.Add(new SelectListItem { Text = "Obsolete", Value = Enums.Reason.Obsolete.ToString() });
             list.Add(new SelectListItem { Text = "Fixed", Value = Enums.Reason.Fixed.ToString() });
             list.Add(new SelectListItem { Text = "Duplicate", Value = Enums.Reason.Duplicate.ToString() });
             list.Add(new SelectListItem { Text = "Deferred", Value = Enums.Reason.Deferred.ToString() });
             list.Add(new SelectListItem { Text = "CannotReproduce", Value = Enums.Reason.CannotReproduce.ToString() });
             list.Add(new SelectListItem { Text = "AsDesigned", Value = Enums.Reason.AsDesigned.ToString() });

             return list;
         }
        
    }
}