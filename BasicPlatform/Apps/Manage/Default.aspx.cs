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
    public partial class Default : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //App = new ObjectId(ContextHelper.ObjId).GetApp();
            //DataBind();
            Response.Redirect("Users.aspx?id=" + new ObjectId(ContextHelper.ObjId), true);
        }
        public App App { get; set; }
    }
}