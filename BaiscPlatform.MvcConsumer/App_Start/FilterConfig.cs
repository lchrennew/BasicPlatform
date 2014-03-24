using System.Web;
using System.Web.Mvc;

namespace BaiscPlatform.MvcConsumer
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}