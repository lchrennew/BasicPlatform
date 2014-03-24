using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using MongoDB.Bson;
using BasicPlatform.Web.Models;

namespace BasicPlatform.Ajax.Apps
{
    public partial class Add : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                PageResult.ok = true;
                App app = new App();
                app.Id = ObjectId.GenerateNewId();
                app.Name = appname.Text.Trim();
                var l = applabel.Text.Trim().ToLowerInvariant();
                if ((app.Label = l).GetApp() != null)
                {
                    PageResult.ok = false;
                    PageResult.errors[applabel.ID] = "label already exists";
                }
                app.Label = applabel.Text.Trim();
                app.Url = appurl.Text.Trim();
                app.Secret = appsecret.Text;
                app.ConnectUrl = connectUrl.Text.Trim();
                app.SelfConnectable = selfConnectable.Checked;
                app.Save();
                PageResult.reload = false;
                PageResult.showtip = false;
                PageResult.resetbtn = false;
                PageResult.data = new { id = app.Id.ToString(), n = app.Name, l = app.Label, url = !string.IsNullOrEmpty(app.ConnectUrl) ? app.Url : null };
                SendJson();
            }
        }
    }
}