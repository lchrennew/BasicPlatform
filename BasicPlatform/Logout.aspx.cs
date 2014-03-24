using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BasicPlatform.Core;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using BasicPlatform.Web.Models;
using BasicPlatform.Auth;

namespace BasicPlatform
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnUrl = Request.QueryString["returnurl"];
            if (string.IsNullOrEmpty(ReturnUrl))
            {
                ReturnUrl = Request.UrlReferrer == null ? FormsAuthentication.DefaultUrl : Request.UrlReferrer.PathAndQuery;
                Response.Redirect("logout.aspx?returnurl=" + HttpUtility.UrlEncode(ReturnUrl), true);
            }
            else if (User.Identity.IsAuthenticated)
            {
                var apps = ContextHelper.User.IsSysAdmin() ? Query.EQ("a", true).GetApps() : Query.And(Query.In("_id", new BsonArray(ContextHelper.User.Apps)), Query.EQ("a", true)).GetApps();
                FormsAuthentication.SignOut();
                if (apps.Count() > 0)
                {
                    rpt.DataSource = apps;
                    rpt.DataBind();
                }
                else
                    Response.Redirect(ReturnUrl, true);
            }
            else
                Response.Redirect(ReturnUrl, true);
        }

        public string ReturnUrl { get; set; }
    }
}