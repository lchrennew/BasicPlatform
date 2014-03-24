using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using MongoDB.Bson;
using BasicPlatform.Web.Models;

namespace BasicPlatform.Core
{
    /// <summary>
    /// 页面基类
    /// </summary>
    public class PageBase : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if (!ContextHelper.User.IsSysAdmin())
            {
                var actions = ContextHelper.PageActions;

                if (actions != null)
                {
                    string appfield = ConfigurationManager.AppSettings["app"];

                    if (string.IsNullOrEmpty(appfield))
                    {
                        var uaction = ContextHelper.Actions;
                        if (!actions.Any(x => x.All(y => uaction.Contains(y))))
                        {
                            Response.Redirect("~/", true);
                        }
                    }
                    else
                    {
                        string redirectUrl = ConfigurationManager.AppSettings["url"] ?? "~/";
                        ObjectId app;
                        if (string.IsNullOrEmpty(Request[appfield]) || !ObjectId.TryParse(Request[appfield], out app))
                        {
                            Response.Redirect("~/", true);
                        }
                        else if (!ContextHelper.User.Id.IsAppAdmin(app))
                        {
                            var actionDict = ContextHelper.GetActions(app);
                            var a = actionDict.ContainsKey(app) ? actionDict[app] : null;
                            if (a == null || !actions.Any(x => x.All(y => a.Contains(y))))
                            {
                                Response.Redirect(string.Format(redirectUrl, app), true);
                            }
                        }
                    }
                }
            }
            base.OnLoad(e);
        }



        PageResult pageResult = new PageResult();
        protected PageResult PageResult { get { return pageResult; } set { pageResult = value; } }
        protected void SendJson(string returnUrl = null)
        {
            if (returnUrl != null) PageResult.url = returnUrl;
            if (Request["fx"] == "1")
            {
                Response.Write(PageResult.ToJson());
                Response.End();
            }
            else Response.Redirect(returnUrl ?? Request.Url.OriginalString, true);
        }
    }
}