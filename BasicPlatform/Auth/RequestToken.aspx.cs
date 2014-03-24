using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Web.Models;
using MongoDB.Bson;
using System.Diagnostics;
using System.Web.Security;

namespace BasicPlatform.Auth
{
    /// <summary>
    /// 通过client_identifier获取一个request_token
    /// </summary>
    public partial class RequestToken : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated) // 如果没有登录，则直接去登录
            {
                Response.Redirect(UrlHelper.GetLoginUrlForRequestToken(ClientIdentifier, ReturnUrl), true);
            }
            else
            {
                // 如果已经登录，但是用户不存在，也重新登录
                var u = User.Identity.Name.GetUser();
                if (u.IsAnonymous())
                    Response.Redirect(UrlHelper.GetLoginUrlForRequestToken(ClientIdentifier, ReturnUrl), true);

                var app = ClientIdentifier.GetApp();
                if (app == null)  // 如果应用不存在，就直接跳转到无应用页
                    Response.Redirect("AppNotFound.aspx", true);
                else if (!u.IsSysAdmin() && !u.IsAppAdmin(app) && (u.Apps == null || !u.Apps.Contains(app.Id))) // 如果用户没有权限，则跳转到无权限提示页进行后续处理
                    Response.Redirect(UrlHelper.GetAccessDeniedUrl(ClientIdentifier), true);
                else
                {
                    // 获取request_token并返回
                    string requestToken = TokenHelper.GenerateRequestToken(u, app, UserToken);
                    Response.Redirect(UrlHelper.GetAppEntryForRequestToken(app.Url, requestToken, ReturnUrl), true);
                }
            }
        }

        public string ClientIdentifier { get { return Request["client_identifier"]; } }
        public string ReturnUrl { get { return Request["return_url"]; } }
        public string UserToken { get { return Request[FormsAuthentication.FormsCookieName]; } }
    }
}