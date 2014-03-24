using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using BasicPlatform.Auth;

namespace BasicPlatform
{
    public partial class Login : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SignIn(object sender, EventArgs e)
        {
            User u = username.Text.ToLowerInvariant().ValidateUser(password.Text);
            if (!u.IsAnonymous())
            {
                PageResult.ok = true;
                PageResult.showtip = false;
                if (password.Text.GetPasswordStrength() < 60)
                    SendJson(Request.QueryString.HasKeys() ? "ResetPassword.aspx?" + Request.QueryString : "ResetPassword.aspx");
                else
                {
                    FormsAuthentication.SetAuthCookie(username.Text.ToLowerInvariant(), rememberme.Checked);
                    //PageResult.data = new { apps = (u.IsSysAdmin() ? Query.Null : Query.In("_id", new BsonArray(u.Apps))).GetApps().Select(app => app.Url).ToArray() };

                    SendJson(Request.QueryString.HasKeys() ? "Prepare.aspx?" + Request.QueryString : "Prepare.aspx");
                }

            }
            else
            {
                PageResult.ok = false;
                PageResult.resetbtn = true;
                PageResult.showtip = true;
                SendJson();
            }
        }

        public string ClientIdentifier { get { return Request.QueryString["client_identifier"]; } }
        public string Secret { get { return Request.QueryString["secret"]; } }
        public string ReturnUrl { get { return Request.QueryString["return_url"] ?? Request.QueryString["returnUrl"] ?? "/"; } }
        public bool IsConnect { get { return Request.QueryString["connect"] == "1"; } }
    }
}