using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BasicPlatform.Core;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using BasicPlatform.Web.Models;
using BasicPlatform.Auth;

namespace BasicPlatform
{
    public partial class Prepare : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnUrl = Request.QueryString["returnurl"] ?? Request.QueryString["return_url"] ?? FormsAuthentication.DefaultUrl;
            if (User.Identity.IsAuthenticated)
            {
                ReturnUrl = Request.QueryString["returnurl"] ?? Request.QueryString["return_url"] ?? FormsAuthentication.DefaultUrl;
                if (!string.IsNullOrEmpty(ClientIdentifier))
                {
                    if (IsConnect)
                    {
                        ReturnUrl = Auth.UrlHelper.GetAuthUrlForConnect(ClientIdentifier, ReturnUrl);
                    }
                    else if (!string.IsNullOrEmpty(Secret))
                    {
                        ReturnUrl = Auth.UrlHelper.GetAuthUrlForAccessToken(ClientIdentifier, Secret, ReturnUrl);
                    }
                    else
                    {
                        ReturnUrl = Auth.UrlHelper.GetAuthUrlForRequestToken(ClientIdentifier, ReturnUrl);
                    }
                }

                var apps = ContextHelper.User.IsSysAdmin() ? Query.EQ("a", true).GetApps() : Query.And(Query.In("_id", new BsonArray(ContextHelper.User.Apps)), Query.EQ("a", true)).GetApps();
                if (apps.Any())
                {
                    rpt.DataSource = apps;
                    rpt.DataBind();
                }
                else
                    Response.Redirect(ReturnUrl, true);
            }
            else
                Response.Redirect(FormsAuthentication.DefaultUrl, true);
        }

        public string ReturnUrl { get; set; }

        public string ClientIdentifier { get { return Request.QueryString["client_identifier"]; } }
        public string Secret { get { return Request.QueryString["secret"]; } }
        public bool IsConnect { get { return Request.QueryString["connect"] == "1"; } }
    }
}