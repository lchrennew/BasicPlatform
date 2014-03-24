using System;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.IO;
using System.Web.Configuration;
using System.Diagnostics;
using System.Web.UI;
using System.Collections;
using System.Linq;
using System.Web.Script.Serialization;

namespace BasicPlatform.Client
{
    /// <summary>
    /// 认证处理页面
    /// </summary>
    public sealed class AuthHandler : Page
    {
        static IUserProcessor up = Activator.CreateInstance(Type.GetType(Config.iUserProcessor, false, true) ?? typeof(UserProcessor)) as IUserProcessor ?? new UserProcessor();
        static IGroupProcessor gp = Activator.CreateInstance(Type.GetType(Config.iGroupProcessor, false, true) ?? typeof(GroupProcessor)) as IGroupProcessor ?? new GroupProcessor();
        static JavaScriptSerializer json = new JavaScriptSerializer();

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            string accessToken = Request.QueryString["access_token"];
            string requestToken = Request.QueryString["request_token"];
            string returnUrl = Request.QueryString["return_url"] ?? Request.QueryString["returnurl"];
            string signOutValue = Request.QueryString["logout"];
            bool signOut = !string.IsNullOrEmpty(signOutValue);
            bool signOutSilent = signOutValue == "silent";
            bool signOutAll = signOutValue == "all";
            bool signOutJsonp = signOut && signOutValue.StartsWith("js.");
            bool connect = Config.connected && !signOut;
            bool bindAlias = Request.QueryString.AllKeys.Contains("setalias");
            bool setGroups = Request.QueryString.AllKeys.Contains("setgroups");
            bool getGroups = Request.QueryString.AllKeys.Contains("getgroups");
            bool getVersion = Request.QueryString.AllKeys.Contains("version");
            string initUser = Request.QueryString["u"];
            string aliasUser = Request.QueryString["a"];
            string[] userGroups = Request.QueryString.GetValues("g");
            Response.AppendHeader("P3P", "CP=CAO PSA OUR");
            if (getVersion)
            {
                Response.Write(typeof(AuthHandler).Assembly.GetName().Version);
                Response.End();
            }

            if (!string.IsNullOrEmpty(initUser)) initUser = initUser.ToLowerInvariant();
            if (!string.IsNullOrEmpty(aliasUser)) aliasUser = aliasUser.ToLowerInvariant();

            if (getGroups) // 不需要登录，直接出数据
            {

                Response.ContentType = "text/javascript";
                if (!string.IsNullOrEmpty(aliasUser))
                    Response.Write(string.Format("$(window).trigger($.extend($.Event('apps.usergroups.loaded'), {0}))",
                        json.Serialize(
                        new
                        {
                            id = Config.apiKey,
                            groups = gp.GetGroupsOfUser(aliasUser).ToArray()
                        })));
                else
                    Response.Write(string.Format("$(window).trigger($.extend($.Event('apps.groups.loaded'), {0}))",
                        json.Serialize(
                        new
                        {
                            id = Config.apiKey,
                            groups = gp.GetAllGroups().Select(x => new { k = x.Key, v = x.Value }).ToArray()
                        })));
                Response.End();
            }
            else if (setGroups)
            {

                SetGroups(accessToken, aliasUser, userGroups, returnUrl);
            }
            else if (bindAlias)
            {
                SetAlias(accessToken, initUser, aliasUser, returnUrl);
            }
            else if (connect) // 如果采用链接接入
            {
                Connect(accessToken, returnUrl);
            }
            else if (signOut) // 如果登出
                SignOut(returnUrl, signOutAll, signOutSilent, signOutJsonp ? signOutValue : null);
            else // 默认访问，直接进入通过request_token和access_token登录的标准交互流程
                SignIn(requestToken, accessToken, returnUrl);
            base.OnLoad(e);
        }

        void SetGroups(string accessToken, string alias, string[] groups, string returnUrl)
        {
            /* 绑定流程 ************************
             * 
             * 1 bpo.aspx?setgroups=1&a=z&g=m&g=n
             * 2 setgroups.aspx?a=z&g=m&g=n
             * 3 bpo.aspx?setgroups=1&accesstoken=yyy&a=z&g=m&g=n 跳转到connecturl，如果未登录，则登录；如果已经登录 调用IUserProcessor.CreateIfNotExists,并进行绑定
             * 3.1未登录：setalias.aspx?u=m&a=z 重新回到3
             * 4 connect.aspx?u=z
             *************************************/
            if (string.IsNullOrEmpty(alias))
#if D
                throw new ArgumentNullException("username 或 alias 参数不能为null或空字符串 ");
#else
            {
                Response.ContentType = "text/javascript";
                Response.Write("$(window).trigger('group.null')");
                Response.End();
            }
#endif

            bool ok;
            if (!User.Identity.IsAuthenticated) // 如果未登陆，则登录
            {
                if (accessToken == null) // 如果不带有access_token，则跳转到setgroups.aspx，去获取新的access_token
                {
                    Forward(UrlHelper.GetSetGroupsUrl(alias, groups, null));
                }
                var user = Bpc.GetAliasByAccessToken(accessToken, out ok);
                if (!ok)
                {
                    // accesstoken无效
                    Forward(UrlHelper.GetSetGroupsUrl(alias, groups, null));
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(user, true);
                    gp.SetGroupsOfUser(alias, groups);
                    Response.ContentType = "text/javascript";
                    Response.Write(string.Format("$(window).trigger($.extend($.Event('apps.setgroup.ok'), {0}))", json.Serialize(new { id = Config.apiKey, alias = alias })));
                    Response.End();
                }
            }
            else  // 如果已经登录，则设置分组
            {
                gp.SetGroupsOfUser(alias, groups);
                Response.ContentType = "text/javascript";
                Response.Write(string.Format("$(window).trigger($.extend($.Event('apps.setgroup.ok'), {0}))", json.Serialize(new { id = Config.apiKey, alias = alias })));
                Response.End();
            }

        }

        /// <summary>
        /// 绑定
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="username"></param>
        /// <param name="alias"></param>
        /// <param name="returnUrl"></param>
        void SetAlias(string accessToken, string username, string alias, string returnUrl)
        {
            /* 绑定流程 ************************
             * 
             * 1 bpo.aspx?setalias=1&u=m&a=z
             * 2 setalias.aspx?u=m&a=z
             * 3 bpo.aspx?setalias=1&accesstoken=yyy&u=m&a=z&returnurl=connecturl 跳转到connecturl，如果未登录，则登录；如果已经登录 调用IUserProcessor.CreateIfNotExists,并进行绑定
             * 3.1未登录：setalias.aspx?u=m&a=z 重新回到3
             * 4 connect.aspx?u=z
             *************************************/
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(alias))
#if D
                throw new ArgumentNullException("username 或 alias 参数不能为null或空字符串 ");
#else
            {
                Response.ContentType = "text/javascript";
                Response.Write("$(window).trigger('bind.null')");
                Response.End();
            }
#endif
            else alias = alias;

            bool ok;
            if (accessToken == null) // 如果接入此页，并且不带有access_token，则跳转到setalias.aspx，去获取新的access_token
            {
                Forward(UrlHelper.GetSetAliasUrl(username, alias, null));
            }
            else if (!User.Identity.IsAuthenticated)
            {
                var user = Bpc.GetAliasByAccessToken(accessToken, out ok);
                if (!ok)
                {
                    // accesstoken无效
                    Forward(UrlHelper.GetSetAliasUrl(username, alias, null));
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(user, true);
                    Forward(UrlHelper.GetSetAliasUrl(username, alias, null));
                }
            }
            else  // 如果传入了access_token，则开始绑定
            {
                // 如果传入了alias，则创建用户并设置连接
                up.CreateIfNotExists(alias);
                Bpc.SetAliasOfUserByAccessToken(accessToken, username, alias, out ok);
                if (!ok)
                    // accesstoken无效
                    Forward(UrlHelper.GetSetAliasUrl(username, alias, null));
                else
                {
                    // 如果不使用jsonp的形式，并且需要采用直接跳转的形式，那就直接跳转到connecturl
                    //Forward(returnUrl + "?u=" + HttpUtility.UrlEncode(alias));

                    // 如果使用jsonp的形式，就使用下面的两行
                    Response.ContentType = "text/javascript";
                    Response.Write(string.Format("$(window).trigger($.extend($.Event('bind.ok'), {0}))", json.Serialize(new { id = Config.apiKey, alias = alias })));
                    Response.End();
                }

            }
        }

        void Connect(string accessToken, string returnUrl)
        {
            if (accessToken == null) // 如果接入此页，并且不带有access_token，则跳转到connecturl，去获取新的access_token
            {
                Forward(UrlHelper.GetConnectionUrl(returnUrl));
            }
            else // 如果传入了access_token，则开始绑定
            {
                bool ok;
                if (!User.Identity.IsAuthenticated) // 如果没有登录，则检查是否已经绑定
                {
                    string alias = Bpc.GetAliasByAccessToken(accessToken, out ok);
                    if (!ok) // 如果access_token无效，则重新接入
                    {
                        Forward(UrlHelper.GetConnectionUrl(returnUrl));
                    }
                    else if (string.IsNullOrEmpty(alias)) // 如果帐号未绑定
                    {
                        if (Config.selfConnectable) // 如果可以自行接入，则去登录进行绑定
                            Forward(UrlHelper.GetConnectLoginUrl(returnUrl)); // 从登录页登录成功后，跳转到本页 bpo.aspx，重新获取access_token
                        else
                        {
                            Response.StatusCode = 403;
                            Response.SubStatusCode = 21;
                            Response.End();
                        }
                    }
                    else // 如果已经绑定，则使用绑定帐号登录
                    {
                        FormsAuthentication.SetAuthCookie(alias, false);
                        // 登录完成，跳转到返回页或默认页
                        if (!string.IsNullOrEmpty(returnUrl))
                            Forward(returnUrl);
                        else
                            Forward(FormsAuthentication.DefaultUrl);
                    }
                }
                else
                {
                    // 如果已经登录，由于此时access_token可用，所以使用access_token进行帐号绑定
                    Bpc.SetAliasByAccessToken(accessToken, User.Identity.Name, out ok);
                    if (ok) // 如果access_token有效
                    {
                        // 绑定完成后，跳转到返回页或默认页
                        if (!string.IsNullOrEmpty(returnUrl))
                            Forward(returnUrl);
                        else
                            Forward(FormsAuthentication.DefaultUrl);
                    }
                    else
                    {
                        // 如果access_token无效，则重新接入
                        Forward(UrlHelper.GetConnectionUrl(returnUrl));
                    }
                }
            }
        }
        void SignOut(string returnUrl, bool signOutAll, bool silent = false, string js = null)
        {
            FormsAuthentication.SignOut();
            if (silent) // 静默
            {
                Response.StatusCode = 204;
                Response.End();
            }
            else if (!string.IsNullOrEmpty(js)) // 脚本
            {
                Response.ContentType = "text/javascript";
                Response.Write(string.Format("$(window).trigger('{0}')", js));
                Response.End();
            }
            else // 跳转
            {
                if (signOutAll) returnUrl = UrlHelper.GetSignOutUrl("all=1");
                else returnUrl = UrlHelper.GetSignOutUrl();
                Forward(returnUrl);
            }
        }

        void SignIn(string requestToken, string accessToken, string returnUrl)
        {

            if (accessToken == null && requestToken == null) // 获取request_token流程
                Forward(UrlHelper.GetRequestTokenUrl(returnUrl));
            else if (!string.IsNullOrEmpty(requestToken)) // 如果接收到传入request_token，则开始验证request_token并获取access_token
                Forward(UrlHelper.GetAccessTokenUrl(returnUrl, FormsAuthentication.HashPasswordForStoringInConfigFile(requestToken + Config.apiSecret, "md5")));
            else if (!string.IsNullOrEmpty(accessToken)) // 如果传入access_token，则开始后端登录
            {
                var u = Bpc.GetUserByAccessToken(accessToken);
                if (u == null) // 如果access_token无效，则重新进入交互流程
                    Forward(UrlHelper.GetRequestTokenUrl(returnUrl));
                else // 如果OK，则调到默认页或返回页
                {
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    if (!string.IsNullOrEmpty(returnUrl))
                        Forward(returnUrl);
                    else
                        Forward(FormsAuthentication.DefaultUrl);
                }
            }
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="url"></param>
        void Forward(string url)
        {
#if D
            Response.Write(url);
            Response.End();
#else
            Response.Redirect(url, true);
#endif
        }



    }
}
