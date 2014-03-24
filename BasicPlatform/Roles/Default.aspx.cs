using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver.Builders;
using BasicPlatform.Web.Models;
using BasicPlatform.Core;

namespace BasicPlatform.Roles
{
    public partial class Default : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long totalRecords;
            rpt.DataSource = Query.And(ContextHelper.Filter, Query.NotExists("a")).GetRoles(pager.CurrentPage = ContextHelper.PageIndex, pager.PageSize, out totalRecords);
            pager.TotalRecords = totalRecords;
            pager.LinkPattern = ContextHelper.PagePattern;
            rpt.DataBind();
        }
    }
}