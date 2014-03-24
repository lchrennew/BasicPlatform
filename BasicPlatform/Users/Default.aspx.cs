using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;

namespace BasicPlatform.Users
{
    public partial class Default : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long totalRecords;
            rpt.DataSource = ContextHelper.Filter.GetUsers(pager.CurrentPage = ContextHelper.PageIndex, pager.PageSize, out totalRecords);
            pager.TotalRecords = totalRecords;
            pager.LinkPattern = ContextHelper.PagePattern;
            rpt.DataBind();
        }
    }
}