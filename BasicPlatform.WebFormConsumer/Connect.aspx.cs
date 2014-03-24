using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace BasicPlatform.WebFormConsumer
{
    public partial class Connect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(username.Text, true);
            Response.Redirect(FormsAuthentication.LoginUrl + "?return_url=" + HttpUtility.UrlEncode(Request.QueryString["return_url"]), true);
        }
    }
}