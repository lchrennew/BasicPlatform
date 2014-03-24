using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Web.Models;
using BasicPlatform.Core;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace BasicPlatform.Apps.Manage
{
    public partial class Roles : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.ObjId).GetApp();
            if (App != null && App.Roles != null && App.Roles.Count > 0)
            {
                rpt.DataSource = Query.And(Query.In("_id", new BsonArray(App.Roles)), Query.EQ("a", App.Id)).GetRoles();
            }
            DataBind();
        }
        public App App { get; set; }
    }
}