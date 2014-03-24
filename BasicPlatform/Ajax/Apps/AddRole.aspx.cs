using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;
using MongoDB.Bson;

namespace BasicPlatform.Ajax.Apps
{
    public partial class AddRole : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.Search).GetApp();
            if (IsPostBack)
            {
                Role role = rolelabel.Text.Trim().GetRole(App.Id);
                if (role == null)
                {
                    role = new Role();
                    role.Id = ObjectId.GenerateNewId();
                    role.Name = rolename.Text.Trim();
                    role.Label = rolelabel.Text.Trim();
                    role.App = App.Id;
                    role.Save();
                }
                App.AddRoles(role.Id);
                App.Save();
                PageResult.ok = true;
                PageResult.reload = false;
                PageResult.showtip = false;
                PageResult.resetbtn = false;
                PageResult.data = new { id = role.Id.ToString(), n = role.Name, l = role.Label };
                SendJson();
            }
        }
        public App App { get; set; }
    }
}