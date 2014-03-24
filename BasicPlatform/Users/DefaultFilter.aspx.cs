using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver.Builders;
using BasicPlatform.Web.Models;

namespace BasicPlatform.Users
{
    public partial class DefaultFilter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            apps.DataSource = Query.Null.GetApps();
            roles.DataSource = Query.And(Query.NE("l", "AppAdmin"), Query.NotExists("a")).GetRoles();
            apps.DataBind();
            roles.DataBind();
        }
    }
}