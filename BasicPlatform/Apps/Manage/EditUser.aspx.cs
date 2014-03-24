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
using BasicPlatform.Auth;
using System.Web.Security;

namespace BasicPlatform.Apps.Manage
{
    public partial class EditUser : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.ObjId).GetApp();
            //App.AddRoles(Role.AppAdmin.Id);
            if (!IsPostBack)
            {
                var u = new ObjectId(ContextHelper.Search).GetUser();
                if (u != null && u.Apps.Contains(App.Id))
                {
                    Username = u.Username;
                    var appRole = App.Id.GetAppRole(u.Id);
                    InitRoles = appRole == null ? new HashSet<ObjectId>() : appRole.Roles ?? new HashSet<ObjectId>();
                    var r = Query.And(Query.In("_id", new BsonArray(App.Roles)), Query.EQ("a", App.Id)).GetRoles();
                    if (ContextHelper.User.IsSysAdmin() || ContextHelper.User.IsAppAdmin(App))
                        r = r.Union(new Role[] { Role.AppAdmin });
                    else if (InitRoles.Contains(Role.AppAdmin.Id))
                        Response.Redirect("Users.aspx?id=" + App.Id, true);

                    roles.DataSource = r;

                    Connection con = u.Id.GetConnection(App.Id);
                    if (con != null)
                    {
                        ConnectionId = con.Id;
                        if (con != null) alias.Text = con.Alias;
                    }
                }
                DataBind();
            }
        }
        public App App { get; set; }
        public string Username { get; set; }
        public IEnumerable<ObjectId> InitRoles { get; set; }

        protected void Grant(object sender, EventArgs e)
        {
            var u = new ObjectId(ContextHelper.Search);
            var appRole = App.Id.GetAppRole(u) ?? new AppRole { App = App.Id, User = u };
            var r = Request.Form.GetValues(roles.ID);
            if (r == null || r.Length == 0)
            {
                if (appRole.Id != default(ObjectId)) appRole.Delete();
            }
            else
            {
                appRole.Roles = new HashSet<ObjectId>(r.Select(x => new ObjectId(x)));
                appRole.Save(false);
            }

            PageResult.ok = true;
            PageResult.reload = true;
            PageResult.showtip = true;
            PageResult.data = App.Url;
            SendJson("Users.aspx?id=" + App.Id);
        }
        public string UserToken { get { return Request[FormsAuthentication.FormsCookieName]; } }
        public ObjectId ConnectionId { get; set; }
    }
}