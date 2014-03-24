using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Web.Models;
using System.Web.Security;

namespace BasicPlatform.Auth
{
    public partial class Connect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* 流程
             * 1 bpo.aspx?returnurl=nnn.aspx
             * 2 connect.aspx?returnurl=nnn.aspx
             * 3 bpo.aspx?returnurl=nnn.aspx&accesstoken=xxx 用accesstoken获取alias，如果alias存在就登录，转到8；如果不存在，则未绑定，转到4
             * 4 connecturl?returnurl=nnn.aspx 完成登录
             * 5 bpo.aspx?returnurl=nnn.aspx
             * 6 connect.aspx?returnurl=nnn.aspx
             * 7 bpo.aspx?accesstoken=xxx&returnurl=nnn.aspx 因为已经登录，尝试使用accesstoken绑定
             * 8 nnn.aspx
             * */
            if (User.Identity.IsAuthenticated) // 如果已经登录，则试图取绑定
            {
                // 如果已经登录，但是用户不存在，也重新登录
                var u = User.Identity.Name.GetUser();
                if (u.IsAnonymous())
                    Response.Redirect(UrlHelper.GetLoginUrlForConnect(ClientIdentifier, ReturnUrl), true);

                var app = ClientIdentifier.GetApp();
                if (app == null)  // 如果应用不存在，就直接跳转到无应用页
                    Response.Redirect("AppNotFound.aspx", true);
                /* * 
                 * 如果不希望主动打开授权，使用下面的代码
                 *  else if (u.Apps == null || !u.Apps.Contains(app.Id)) // 如果用户没有权限，则跳转到无权限提示页进行后续处理
                 *      Response.Redirect(UrlHelper.GetAccessDeniedUrl(ClientIdentifier), true);
                 * */

                var accessToken = TokenHelper.GenerateAccessToken(u, app, UserToken);
                FormsAuthentication.SetAuthCookie(u.Username, FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).IsPersistent);
                Response.Redirect(UrlHelper.GetAppEntryForConnect(app.Url, accessToken, ReturnUrl), true);
            }
            else // 如果没有登录，则去登录后再回来重新处理
            {
                Response.Redirect(UrlHelper.GetLoginUrlForConnect(ClientIdentifier, ReturnUrl), true);
            }
        }

        public string ClientIdentifier { get { return Request["client_identifier"]; } }
        public string ReturnUrl { get { return Request["return_url"]; } }
        public string UserToken { get { return Request[FormsAuthentication.FormsCookieName]; } }
    }
}