using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;

namespace BasicPlatform.Roles
{
    public partial class Add : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Save(object sender, EventArgs e)
        {
            PageResult.ok = true;
            Role role = new Role();
            role.Name = name.Text.Trim();
            if ((role.Label = label.Text.Trim()).GetRole() != null)
            {
                PageResult.ok = false;
                PageResult.errors[label.ID] = "label already exists";
            }
            if (PageResult.ok)
            {
                role.Save();
                PageResult.reload = true;
                PageResult.showtip = true;
                PageResult.resetbtn = false;
                PageResult.url = "./";
            }
            SendJson();
        }
    }
}