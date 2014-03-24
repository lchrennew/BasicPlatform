using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using MongoDB.Bson;
using BasicPlatform.Web.Models;
using BasicPlatform.Auth;
using System.Web.Security;

namespace BasicPlatform.Ajax.Apps
{
    public partial class Connect : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var app = AppId.GetApp();
            var u = UserId.GetUser();
            if (app != null && !string.IsNullOrEmpty(app.ConnectUrl) && !u.IsAnonymous())
            {
                Username = u.Username;
                Connection con = u.Id.GetConnection(app.Id);
                if (con != null) apps_connect_alias.Text = con.Alias;
                AppUrl = app.Url;
            }
        }

        public string AppUrl { get; set; }
        public string Username { get; set; }
        public ObjectId UserId { get { return new ObjectId(ContextHelper.Search); } }
        public ObjectId AppId { get { return new ObjectId(ContextHelper.ObjId); } }
    }
}