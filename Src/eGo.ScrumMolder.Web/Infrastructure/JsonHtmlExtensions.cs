using System.Web.Mvc;
using Newtonsoft.Json;

namespace eGo.ScrumMolder.Web.Infrastructure
{
    public static class JsonHtmlExtensions
    {
        public static MvcHtmlString Json<TModel, TObject>(this HtmlHelper<TModel> html, TObject obj)
        {
            return MvcHtmlString.Create(JsonConvert.SerializeObject(obj));
        }
    }
}