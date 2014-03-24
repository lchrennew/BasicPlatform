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
    public partial class ResetPassword : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SignInAndResetPassword(object sender, EventArgs e)
        {
            User u = username.Text.ToLowerInvariant().ValidateUser(oldpwd.Text);
            if (!u.IsAnonymous())
            {
                if (newpwd.Text.GetPasswordStrength() < 60)
                {
                    PageResult.ok = false;
                    PageResult.resetbtn = true;
                    PageResult.showtip = false;
                    PageResult.errors[newpwd.ID] = "weak";
                    SendJson();
                }
                else
                {
                    u.Password = newpwd.Text;
                    u.Save(true);
                    FormsAuthentication.SetAuthCookie(username.Text.ToLowerInvariant(), false);
                    PageResult.ok = true;
                    PageResult.showtip = false;
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
    }
}