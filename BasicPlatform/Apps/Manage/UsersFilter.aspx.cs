using BasicPlatform.Core;
using BasicPlatform.Web.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BasicPlatform.Apps.Manage
{
    public partial class UsersFilter : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.ObjId).GetApp();
            var r = Query.And(Query.In("_id", new BsonArray(App.Roles)), Query.EQ("a", App.Id)).GetRoles();
            if (ContextHelper.User.IsSysAdmin() || ContextHelper.User.IsAppAdmin(App)) r = new[] { Role.AppAdmin }.Union(r);
            roles.DataSource = r;
            roles.DataBind();
        }

        public App App { get; set; }
    }
}