using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BasicPlatform.Client
{
    internal static class UrlHelper
    {
        #region Url
        /// <summary>
        /// 获取request_token
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        internal static string GetRequestTokenUrl(string returnUrl)
        {
            return Config.authApi + "/requesttoken.aspx?client_identifier=" + HttpUtility.UrlEncode(Config.clientIdentifier) + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }

        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        internal static string GetAccessTokenUrl(string returnUrl, string secret)
        {
            return Config.authApi + "/accesstoken.aspx?client_identifier=" + HttpUtility.UrlEncode(Config.clientIdentifier) + "&secret=" + secret + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }

        /// <summary>
        /// 连接接入
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        internal static string GetConnectionUrl(string returnUrl)
        {
            return Config.authApi + "/connect.aspx?client_identifier=" + HttpUtility.UrlEncode(Config.clientIdentifier) + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }

        /// <summary>
        /// 别名设置
        /// </summary>
        /// <param name="username"></param>
        /// <param name="alias"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        internal static string GetSetAliasUrl(string username, string alias, string returnUrl)
        {
            return Config.authApi + "/SetAlias.aspx?client_identifier=" + HttpUtility.UrlEncode(Config.clientIdentifier) + "&u=" + username + "&a=" + alias + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }

        /// <summary>
        /// 用户组设置
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="groups"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        internal static string GetSetGroupsUrl(string alias, string[] groups, string returnUrl)
        {
            return Config.authApi + "/SetGroups.aspx?client_identifier=" + HttpUtility.UrlEncode(Config.clientIdentifier) + "&a=" + alias + 
                (groups == null ? null : ("&g=" + string.Join("&g=", groups.Select(g => HttpUtility.UrlEncode(g))))) + 
                "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal static string GetSignOutUrl(string parameters = null)
        {
            if (string.IsNullOrEmpty(parameters))
                return Config.authApi + "/signout.aspx";
            else return Config.authApi + "/signout.aspx?" + parameters;
        }

        /// <summary>
        /// 连接登录
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        internal static string GetConnectLoginUrl(string returnUrl)
        {
            return Config.connectUrl + "?return_url=" + HttpUtility.UrlEncode(returnUrl);
        }
        #endregion

    }
}
