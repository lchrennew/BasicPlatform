using BasicPlatform.Core;
using BasicPlatform.Web.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BasicPlatform.Apps.Manage
{
    public partial class EditRole : PageBase
    {
        public App App { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            App = new ObjectId(ContextHelper.ObjId).GetApp();
            role = new ObjectId(ContextHelper.Search).GetRole();
            if (role != null && role.App == App.Id)
            {
                if (!IsPostBack)
                {
                    name.Text = role.Name;
                    label.Text = role.Label;
                    DataBind();
                }
            }
            else
            {
                Response.Redirect("Roles.aspx?id=" + App.Id, true);
            }
        }

        Role role;

        public void Save(object sender, EventArgs e)
        {
            role.Name = name.Text;
            role.Save();
            PageResult.ok = true;
            PageResult.reload = true;
            PageResult.showtip = true;
            PageResult.resetbtn = false;
            PageResult.url = "Roles.aspx?id=" + App.Id;
            SendJson();
        }
    }
}