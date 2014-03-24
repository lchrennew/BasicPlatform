using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver.Builders;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;
using MongoDB.Bson;

namespace BasicPlatform.Profile
{
    public partial class Connections : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long totalRecords;
            var u = ContextHelper.User;
            if (u.Apps != null)
            {
                var connections = Query.And(Query.EQ("u", u.Id), Query.In("a", new BsonArray(u.Apps))).GetConnections(pager.CurrentPage = ContextHelper.PageIndex, pager.PageSize, out totalRecords);
                apps = Query.In("_id", new BsonArray(connections.Select(x => x.App))).GetApps().ToDictionary(x => x.Id);
                rpt.DataSource = connections;
                pager.LinkPattern = ContextHelper.PagePattern;
                pager.TotalRecords = totalRecords;
                rpt.DataBind();
            }
        }
        Dictionary<ObjectId, App> apps;
        protected string GetAppName(ObjectId app)
        {
            if (apps.ContainsKey(app)) return apps[app].Name;
            else return null;
        }

        protected string GetAppConnectUrl(ObjectId app)
        {
            if (apps.ContainsKey(app)) return apps[app].ConnectUrl;
            else return null;
        }

        protected string GetAppUrl(ObjectId app)
        {
            if (apps.ContainsKey(app)) return apps[app].Url;
            else return null;
        }
    }
}