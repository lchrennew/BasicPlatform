using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Web.Models;
using MongoDB.Bson;
using BasicPlatform.Core;
using MongoDB.Driver.Builders;

namespace BasicPlatform.Apps.Manage
{
    public partial class AppendUsers : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.ObjId).GetApp();
            //App.AddRoles(Role.AppAdmin.Id);
            if (!IsPostBack)
            {
                var r = Query.And(Query.In("_id", new BsonArray(App.Roles)), Query.EQ("a", App.Id)).GetRoles();
                if (ContextHelper.User.IsSysAdmin() || ContextHelper.User.IsAppAdmin(App))
                    r = r.Union(new Role[] { Role.AppAdmin });
                roles.DataSource = r;
                DataBind();
            }
        }
        public App App { get; set; }

        protected void Grant(object sender, EventArgs e)
        {
            var u = users.Text.Split(',').Select(x => new ObjectId(x));

            var r = Request.Form.GetValues(roles.ID);
            if (r != null && r.Length > 0)
                App.AddRoledUsersToApp(u, r.Select(x => new ObjectId(x)));
            else
                App.AddRoledUsersToApp(u);

            PageResult.ok = true;
            PageResult.reload = true;
            PageResult.showtip = true;
            SendJson("Users.aspx?id=" + App.Id);
        }
    }
}