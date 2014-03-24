using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BasicPlatform.MvcConsumer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "你的应用程序说明页。";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }

        [HttpPost]
        public void StartConnect(string username, string return_url)
        {
            FormsAuthentication.SetAuthCookie(username, false);
            Response.Redirect(FormsAuthentication.LoginUrl + "?return_url=" + HttpUtility.UrlEncode(return_url), true);
        }

        [HttpGet]
        public ActionResult Connect(string return_url)
        {
            return View();
        }
    }
}
