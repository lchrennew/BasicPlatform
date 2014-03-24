using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;
using MongoDB.Bson;

namespace BasicPlatform.Roles
{
    public partial class Edit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Role role = new ObjectId(ContextHelper.ObjId).GetRole();
                if (role == null) Response.Redirect("add.aspx", true);
                else
                {
                    name.Text = role.Name;
                    label.Text = role.Label;
                }
            }
        }

        protected void Save(object sender, EventArgs e)
        {
            Role role = new ObjectId(ContextHelper.ObjId).GetRole();
            if (role == null) Response.Redirect("./", true);
            else
            {
                PageResult.ok = true;
                string l = label.Text.Trim();
                if (role.Label.CompareTo(l) != 0 && l.GetApp() != null)
                {
                    PageResult.ok = false;
                    PageResult.errors[label.ID] = "label exists";
                }
                else
                {
                    role.Name = name.Text.Trim();
                    role.Label = l;
                    role.Save();
                    PageResult.reload = true;
                    PageResult.resetbtn = false;
                    PageResult.showtip = true;
                    PageResult.url = "./";
                }
                SendJson();

            }
        }
    }
}