using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using System.Text;
using MongoDB.Driver.Builders;
using BasicPlatform.Web.Models;
using MongoDB.Bson;

namespace BasicPlatform.Users
{
    public partial class Add : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                roles.DataSource = Query.And(Query.NE("l", "AppAdmin"), Query.NotExists("a")).GetRoles();
                roles.DataBind();
                apps.DataSource = Query.Null.GetApps();
                apps.DataBind();
            }
        }

        protected void Save(object sender, EventArgs e)
        {
            Web.Models.User u = new Web.Models.User { Id = ObjectId.GenerateNewId() };
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

            if (roles.Visible)
            {
                var r = Request.Form.GetValues(roles.ID);
                if (r != null && r.Length > 0)
                    u.SetRoles(r.Select(x => new ObjectId(x)));
            }

            if (PageResult.ok)
            {
                u.Label = label.Text.Trim();
                u.Password = password.Text;
                u.Email = email.Text.Trim().ToLowerInvariant();
                u.Save(true);

                if (apps.Visible)
                {
                    var a = Request.Form.GetValues(apps.ID);
                    if (a == null) u.Apps = null;
                    else
                    {
                        u.SetApps(a.Select(x => new ObjectId(x)));
                        foreach (string appId in a)
                        {
                            var r = Request.Form["roles" + appId];

                            if (!string.IsNullOrEmpty(r))
                            {
                                var roleIds = r.Split(',').Select(x => new ObjectId(x));
                                var ar = new AppRole { User = u.Id, App = new ObjectId(appId) };
                                ar.SetRoles(roleIds);
                                ar.Save(false);
                            }
                        }

                    }
                }
                u.Save(false);

                PageResult.reload = true;
                PageResult.showtip = true;
                PageResult.resetbtn = false;
                PageResult.url = "./";
            }
            SendJson();
        }
    }
}