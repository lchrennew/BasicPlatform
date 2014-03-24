using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;
using MongoDB.Bson;

namespace BasicPlatform.Ajax.Roles
{
    public partial class Add : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Role role = new Role();
                role.Id = ObjectId.GenerateNewId();
                role.Name = rolename.Text.Trim();
                role.Label = rolelabel.Text.Trim();
                role.Save();
                PageResult.ok = true;
                PageResult.reload = false;
                PageResult.showtip = false;
                PageResult.resetbtn = false;
                PageResult.data = new { id = role.Id.ToString(), n = role.Name, l = role.Label };
                SendJson();
            }
        }
    }
}