using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Web.Models;
using BasicPlatform.Core;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace BasicPlatform.Auth
{
    public partial class SetAlias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated) // 如果已经登录，则试图取绑定
            {
                // 如果已经登录，但是用户不存在
                User u = User.Identity.Name.GetUser();
                if (u.IsAnonymous())
                {
                    Response.StatusCode = 204;
                    Response.End();
                }

                var app = ClientIdentifier.GetApp();
                if (app == null)  // 如果应用不存在
                {
                    Response.StatusCode = 204;
                    Response.End();
                }

                BasicPlatform.Web.Models.Action a = "ConnectUsersOfApps".GetAction();
                AppAction action = app.Id.GetAppAction(a.Id);
                AppRole role = app.Id.GetAppRole(u.Id);
                if (u.IsSysAdmin() || u.IsAppAdmin(app) || (role != null && action != null && action.Roles != null && role.Roles != null && role.Roles.Intersect(action.Roles).Any())) // 判断权限
                {
                    var accessToken = TokenHelper.GenerateAccessToken(u, app, UserToken);
                    FormsAuthentication.SetAuthCookie(u.Username, FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).IsPersistent);

                    Response.Redirect(UrlHelper.GetAppEntryForSetAlias(app.Url, accessToken, Username, Alias, app.ConnectUrl), true);
                }
                else
                {
                    Response.StatusCode = 204;
                    Response.End();
                }
            }
            else // 如果没有登录
            {
                Response.StatusCode = 204;
                Response.End();
            }
        }

        public string ClientIdentifier { get { return Request["client_identifier"]; } }
        public string Username { get { return Request["u"]; } }
        public string Alias { get { return Request["a"]; } }
        public string ReturnUrl { get { return Request["return_url"]; } }
        public string UserToken { get { return Request[FormsAuthentication.FormsCookieName]; } }
    }
}