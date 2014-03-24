using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Web.Models;
using MongoDB.Driver.Builders;
using BasicPlatform.Core;
using MongoDB.Bson;

namespace BasicPlatform
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var u = ContextHelper.User;
            if (u.IsSysAdmin())
            {
                rpt.DataSource = Query.Null.GetApps();
                rpt.DataBind();
            }
            else if (u.Apps != null)
            {
                ContextHelper.GetActions(u.Apps.ToArray());
                rpt.DataSource = Query.In("_id", new BsonArray(u.Apps)).GetApps();
                rpt.DataBind();
            }

            if (u.Roles != null)
            {
                roles.DataSource = Query.And(Query.In("_id", new BsonArray(u.Roles)), Query.NotExists("a")).GetRoles();
                roles.DataBind();
            }
        }
    }
}