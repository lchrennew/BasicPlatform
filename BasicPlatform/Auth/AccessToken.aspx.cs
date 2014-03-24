using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Web.Models;
using MongoDB.Bson;
using System.Web.Security;

namespace BasicPlatform.Auth
{
    /// <summary>
    /// 使用加密后的request_token获取access_token
    /// </summary>
    public partial class AccessToken : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated) // 如果未登录，则登录
            {
                Response.Redirect(UrlHelper.GetLoginUrlForAccessToken(ClientIdentifier, SecretRequestToken, ReturnUrl), true);
            }
            else
            {
                // 如果已经登录，但是用户不存在，也重新登录
                var u = User.Identity.Name.GetUser();
                if (u.IsAnonymous())
                    Response.Redirect(UrlHelper.GetLoginUrlForAccessToken(ClientIdentifier, SecretRequestToken, ReturnUrl), true);

                var app = ClientIdentifier.GetApp();
                if (app == null)  // 如果应用不存在，就直接跳转到无应用页
                    Response.Redirect("AppNotFound.aspx", true);
                else if (!u.IsSysAdmin() && !u.IsAppAdmin(app) && (u.Apps == null || !u.Apps.Contains(app.Id))) // 如果用户没有权限，则跳转到无权限提示页进行后续处理
                    Response.Redirect(UrlHelper.GetAccessDeniedUrl(ClientIdentifier), true);
                else if (!TokenHelper.ValidateRequestToken(u, app, UserToken, SecretRequestToken)) // 如果RequestToken不正确，则跳转提示无权使用
                    Response.Redirect(UrlHelper.GetAccessDeniedUrl(ClientIdentifier), true);
                else
                {
                    FormsAuthentication.SetAuthCookie(u.Username, FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).IsPersistent);
                    string accessToken = TokenHelper.GenerateAccessToken(u, app, SecretRequestToken);  // 获取access_token并返回
                    Response.Redirect(UrlHelper.GetAppEntryForAccessToken(app.Url, accessToken, ReturnUrl), true);
                }
            }
        }

        public string ReturnUrl { get { return Request.QueryString["return_url"]; } }
        public string SecretRequestToken { get { return Request.QueryString["secret"]; } }
        public string ClientIdentifier { get { return Request.QueryString["client_identifier"]; } }
        public string UserToken { get { return Request[FormsAuthentication.FormsCookieName]; } }
    }
}