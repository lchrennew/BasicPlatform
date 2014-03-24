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
    public partial class CreateRole : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.ObjId).GetApp();
            DataBind();
        }
        public App App { get; set; }

        protected void Create(object sender, EventArgs e)
        {
            PageResult.ok = true;

            Role role = new Role();
            role.Id = ObjectId.GenerateNewId();
            role.Name = name.Text.Trim();
            role.App = App.Id;
            if ((role.Label = label.Text.Trim()).GetRole(App.Id) != null)
            {
                PageResult.ok = false;
                PageResult.errors[label.ID] = "label already exists";
            }
            if (PageResult.ok)
            {
                role.Save();
                App.AddRoles(role.Id);
                App.Save();
                PageResult.reload = true;
                PageResult.showtip = true;
                PageResult.resetbtn = false;
                PageResult.url = "Roles.aspx?id=" + App.Id;
            }
            SendJson();
        }
    }
}