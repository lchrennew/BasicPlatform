using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using BasicPlatform.Web.Models;

namespace BasicPlatform.Core
{
    [ToolboxData("<{0}:PlaceHolder runat=\"server\"> </{0}:PlaceHolder>")]
    public class PlaceHolder : System.Web.UI.WebControls.PlaceHolder
    {
        [Category("Behavior")]
        [PersistenceMode(PersistenceMode.Attribute)]
        [TypeConverter(typeof(StringArrayConverter))]
        public string[] Actions { get; set; }

        [Category("Behavior")]
        [PersistenceMode(PersistenceMode.Attribute)]
        [TypeConverter(typeof(StringArrayConverter))]
        public string[] ExcludeActions { get; set; }

        [Category("Behavior")]
        [PersistenceMode(PersistenceMode.Attribute)]
        public string App { get; set; }

        public override bool Visible
        {
            get
            {
                if (Actions == null || Actions.Length == 0 || ContextHelper.User.IsSysAdmin())
                    return (ExcludeActions == null || ExcludeActions.Length == 0) && base.Visible;
                else if (base.Visible)
                {
                    ObjectId app;
                    if (ExcludeActions == null) ExcludeActions = new string[0];

                    if (string.IsNullOrEmpty(App) || !ObjectId.TryParse(App, out app))
                    {
                        var actions = ContextHelper.Actions;
                        return base.Visible
                            = (actions != null
                            && !ExcludeActions.Any(x => actions.Contains(x))
                            && Actions.Any(x => actions.Contains(x)));
                    }
                    else
                    {
                        if (ContextHelper.User.Id.IsAppAdmin(app)) 
                            return (ExcludeActions == null || ExcludeActions.Length == 0) && base.Visible;
                        else
                        {
                            var actions = ContextHelper.GetActions(app);
                            return base.Visible
                                = (actions.ContainsKey(app) && actions[app] != null
                                && !ExcludeActions.Any(x => actions[app].Contains(x))
                                && Actions.Any(x => actions[app].Contains(x)));
                        }
                    }
                }
                else return false;
            }
            set
            {
                base.Visible = value;
            }
        }
    }
}