using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using eGo.ScrumMolder.Dto.Bugs;
using eGo.ScrumMolder.Web.Models;

namespace eGo.ScrumMolder.Web
{
    public static class Extensions
    {
        public static Dictionary<string, string> ToDictionary(this IEnumerable source, string keyField = "Id", string valueField = "Name")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (var item in source)
            {
                Type itemType = item.GetType();

                PropertyInfo keyProperty = itemType.GetProperty(keyField);
                PropertyInfo valueProperty = itemType.GetProperty(valueField);

                if (keyProperty == null)
                    throw new ArgumentException(string.Format("Object in list must contain property {0}", keyField));
                if (valueProperty == null)
                    throw new ArgumentException(string.Format("Object in list must contain property {0}", valueField));

                dict.Add(keyProperty.GetValue(item, null).ToString(), valueProperty.GetValue(item, null).ToString());
            }

            return dict;
        }

        public static List<SelectListItem> ToDropDownList(this List<Client> source)
        {
            return source.Select(item => new SelectListItem {Value = item.Id.ToString(), Text = item.Name}).ToList();
        }

        public static List<SelectListItem> ToDropDownList(this List<Project> source)
        {
            return source.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();
        }

        public static List<SelectListItem> ToDropDownList(this List<User> source)
        {
            return source.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.UserName }).ToList();
        }

        public static List<SelectListItem> ToDropDownList(this List<Category> source)
        {
            return source.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();
        }

    }
}