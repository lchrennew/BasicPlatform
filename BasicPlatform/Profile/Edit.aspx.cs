using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using BasicPlatform.Core;
using BasicPlatform.Web.Models;

namespace BasicPlatform.Profile
{
    public partial class Edit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Web.Models.User u = ContextHelper.User;
                username.Text = u.Username;
                label.Text = u.Label;
                email.Text = u.Email;
            }
        }

        protected void Save(object sender, EventArgs e)
        {

            Web.Models.User u = ContextHelper.User;
            if (u.IsAnonymous())
            {
                PageResult.ok = true;
                PageResult.showtip = false;
                PageResult.resetbtn = false;
                PageResult.reload = true;
                PageResult.url = "/";
            }
            else
            {
                string em = email.Text.Trim().ToLowerInvariant();

                if (u.Email.CompareTo(em) != 0 && !u.Email.GetUser().IsAnonymous())
                {
                    PageResult.ok = false;
                    PageResult.errors[email.ID] = "email already exists";
                }
                else u.Email = em;


                PageResult.ok = true;
                if (PageResult.ok)
                {
                    u.Label = label.Text.Trim();
                    if (string.IsNullOrEmpty(password.Text))
                        u.Save();
                    else
                    {
                        u.Password = password.Text;
                        u.Save(true);
                    }
                    PageResult.reload = true;
                    PageResult.showtip = true;
                    PageResult.resetbtn = false;
                    PageResult.url = "/";
                }
            }

            SendJson();
        }

    }
}