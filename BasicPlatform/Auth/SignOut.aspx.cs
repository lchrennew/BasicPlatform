using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BasicPlatform.Core;
using MongoDB.Driver.Builders;
using BasicPlatform.Web.Models;
using MongoDB.Bson;

namespace BasicPlatform.Auth
{
    public partial class SignOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Request.QueryString["all"] == "1")
                {
                    var apps = ContextHelper.User.IsSysAdmin() ? Query.EQ("a", true).GetApps() : Query.And(Query.In("_id", new BsonArray(ContextHelper.User.Apps)), Query.EQ("a", true)).GetApps();
                    FormsAuthentication.SignOut();
                    if (apps.Count() > 0)
                    {
                        rpt.DataSource = apps;
                        rpt.DataBind();
                    }
                    else
                        Response.Redirect("/", true);
                }
                else
                    Response.Redirect("/", true);
            }
            else
                Response.Redirect("/", true);
        }
    }
}