using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;
using MongoDB.Driver.Builders;

namespace BasicPlatform.Ajax.Roles
{
    public partial class Gets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Query.And(ContextHelper.Filter, Query.NotExists("a")).GetRoles().ToJson());
            Response.End();
        }
    }
}