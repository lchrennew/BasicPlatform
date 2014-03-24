using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace BasicPlatform.Apps
{
    public partial class Edit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                App app = new ObjectId(ContextHelper.ObjId).GetApp();
                if (app == null) Response.Redirect("./", true);
                else
                {
                    name.Text = app.Name;
                    label.Text = app.Label;
                    url.Text = app.Url;
                    secret.Text = app.Secret;
                    connectUrl.Text = app.ConnectUrl;
                    selfConnectable.SelectedIndex = app.SelfConnectable ? 0 : 1;
                    accessable.SelectedIndex = app.Accessable ? 0 : 1;
                }
            }
        }
        public HashSet<ObjectId> InitRoles { get; set; }
        protected void Save(object sender, EventArgs e)
        {
            App app = new ObjectId(ContextHelper.ObjId).GetApp();
            if (app == null) Response.Redirect("./", true);
            else
            {
                PageResult.ok = true;
                string l = label.Text.Trim().ToLowerInvariant();
                if (app.Label.CompareTo(l) != 0 && (app.Label = l).GetApp() != null)
                {
                    PageResult.ok = false;
                    PageResult.errors[label.ID] = "label exists";
                }
                else
                {

                    app.Name = name.Text.Trim();
                    app.Url = url.Text.Trim();
                    app.Secret = secret.Text.Trim();
                    app.ConnectUrl = connectUrl.Text.Trim();
                    app.SelfConnectable = selfConnectable.SelectedValue == "1";
                    app.Accessable = accessable.SelectedValue == "1";
                    app.Save();
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