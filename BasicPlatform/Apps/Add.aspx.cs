using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace BasicPlatform.Apps
{
    public partial class Add : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Save(object sender, EventArgs e)
        {
            PageResult.ok = true;
            App app = new App();
            app.Name = name.Text.Trim();
            var l = label.Text.Trim().ToLowerInvariant();
            if ((app.Label = l).GetApp() != null)
            {
                PageResult.ok = false;
                PageResult.errors[label.ID] = "label already exists";
            }

            if (PageResult.ok)
            {
                app.Url = url.Text.Trim();
                app.Secret = secret.Text.Trim();
                app.ConnectUrl = connectUrl.Text;
                app.SelfConnectable = selfConnectable.SelectedValue == "1";
                app.Accessable = accessable.SelectedValue == "1";
                app.Save();
                PageResult.reload = true;
                PageResult.showtip = true;
                PageResult.resetbtn = false;
                PageResult.url = "./";
            }
            SendJson();
        }
    }
}