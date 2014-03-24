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
    public partial class CreateUser : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.ObjId).GetApp();
            if (!IsPostBack)
            {
                var r = Query.And(Query.In("_id", new BsonArray(App.Roles)), Query.EQ("a", App.Id)).GetRoles();
                if (ContextHelper.User.IsSysAdmin() || ContextHelper.User.IsAppAdmin(App))
                    r = r.Union(new Role[] { Role.AppAdmin });
                roles.DataSource = r; // 由于之前已经进行过AddRoles，所以不需要再判断Roles是否为null
                DataBind();
            }
        }
        protected void Create(object sender, EventArgs e)
        {
            Web.Models.User u = new Web.Models.User();
            u.Username = username.Text.ToLowerInvariant().Trim();
            u.Email = email.Text.Trim().ToLowerInvariant();
            PageResult.ok = true;

            if (!u.Username.GetUser().IsAnonymous())
            {
                PageResult.ok = false;
                PageResult.errors[username.ID] = "username already exists";
            }

            if (!u.Email.GetUser().IsAnonymous())
            {
                PageResult.ok = false;
                PageResult.errors[email.ID] = "email already exists";
            }


            if (PageResult.ok)
            {
                u.Label = label.Text.Trim();
                u.Password = password.Text;
                u.Email = email.Text.Trim().ToLowerInvariant();

                u.SetApps(App.Id);

                u = u.Save(true);

                var r = Request.Form.GetValues(roles.ID);

                if (r != null && r.Length > 0)
                {
                    var ar = new AppRole { User = u.Id, App = App.Id };
                    ar.SetRoles(r.Select(x => new ObjectId(x)));
                    ar.Save(false);
                }
                //u.Id.Connect(App.Id, alias.Text, TokenHelper.GenerateAccessToken(ContextHelper.User, App, UserToken), User.Identity.Name);

                PageResult.reload = true;
                PageResult.showtip = true;
                PageResult.resetbtn = false;
                PageResult.data = App.Url;
            }
            SendJson("Users.aspx?id=" + App.Id);
        }

        public App App { get; set; }
        public string UserToken { get { return Request[FormsAuthentication.FormsCookieName]; } }
    }
}