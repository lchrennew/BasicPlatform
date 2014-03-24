using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BasicPlatform.Auth
{
    public static class UrlHelper
    {
        /// <summary>
        /// 获取用于请求RequestToken的登录地址
        /// </summary>
        /// <param name="clientIdentifier"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public static string GetLoginUrlForRequestToken(string clientIdentifier, string returnUrl)
        {
            return FormsAuthentication.LoginUrl + "?client_identifier=" + HttpUtility.UrlEncode(clientIdentifier) + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }

        /// <summary>
        /// 获取请求RequestToken地址
        /// </summary>
        /// <param name="clientIdentifier"></param>
        /// <param name="requestToken"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public static string GetAuthUrlForRequestToken(string clientIdentifier, string returnUrl)
        {
            return "/Auth/RequestToken.aspx?client_identifier=" + HttpUtility.UrlEncode(clientIdentifier) + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }

        /// <summary>
        /// 获取请求RequestToken的访问拒绝地址
        /// </summary>
        /// <param name="ClientIdentifier"></param>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        public static string GetAccessDeniedUrl(string clientIdentifier)
        {
            return "/Auth/AppAccessDenied.aspx?client_identifier=" + HttpUtility.UrlEncode(clientIdentifier);
        }

        internal static string GetAppEntryForRequestToken(string appUrl, string requestToken, string returnUrl)
        {
            if (!appUrl.StartsWith("?")) appUrl = appUrl.TrimEnd('?');
            if (!appUrl.Contains('?'))
            {
                return appUrl + "?request_token=" + requestToken + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
            }
            else
            {
                return appUrl + "&request_token=" + requestToken + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
            }
        }

        /// <summary>
        /// 获取验证RequestToken地址
        /// </summary>
        /// <param name="clientIdentifier"></param>
        /// <param name="requestToken"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public static string GetAuthUrlForAccessToken(string clientIdentifier, string requestToken, string returnUrl)
        {
            return "/Auth/AccessToken.aspx?client_identifier=" + HttpUtility.UrlEncode(clientIdentifier) + "&secret=" + requestToken + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }

        /// <summary>
        /// 获取用于验证RequestToken的登录地址
        /// </summary>
        /// <param name="clientIdentifier"></param>
        /// <param name="requestToken">通过APP传递的请求token，用于验证APP和权限，登录后会回到请求RequestToken页面</param>
        /// <param name="returnUrl">通过APP传递的返回地址，当所有认证处理完成后的最后一次跳转到的页面</param>
        /// <returns></returns>
        public static string GetLoginUrlForAccessToken(string clientIdentifier, string requestToken, string returnUrl)
        {
            return FormsAuthentication.LoginUrl + "?client_identifier=" + HttpUtility.UrlEncode(clientIdentifier) + "&secret=" + requestToken + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }

        /// <summary>
        /// 获取验证RequestToken验证成功后的App返回地址
        /// </summary>
        /// <param name="appUrl"></param>
        /// <param name="accessToken"></param>
        /// <param name="returnUrl">通过APP传递的返回地址，当所有认证处理完成后的最后一次跳转到的页面</param>
        /// <returns></returns>
        public static string GetAppEntryForAccessToken(string appUrl, string accessToken, string returnUrl)
        {
            if (!appUrl.StartsWith("?")) appUrl = appUrl.TrimEnd('?');
            if (!appUrl.Contains('?'))
            {
                return appUrl + "?access_token=" + accessToken + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
            }
            else
            {
                return appUrl + "&access_token=" + accessToken + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
            }
        }

        /// <summary>
        /// 获取接入验证地址
        /// </summary>
        /// <param name="clientIdentifer"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        internal static string GetAuthUrlForConnect(string clientIdentifier, string returnUrl)
        {
            return "/Auth/Connect.aspx?client_identifier=" + HttpUtility.UrlEncode(clientIdentifier) + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }

        /// <summary>
        /// 连接登录地址
        /// </summary>
        /// <param name="clientIdentifier"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        internal static string GetLoginUrlForConnect(string clientIdentifier, string returnUrl)
        {
            return FormsAuthentication.LoginUrl + "?connect=1&client_identifier=" + HttpUtility.UrlEncode(clientIdentifier) + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }

        /// <summary>
        /// 连接app登录入口
        /// </summary>
        /// <param name="appUrl"></param>
        /// <param name="accessToken"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        internal static string GetAppEntryForConnect(string appUrl, string accessToken, string returnUrl)
        {
            return appUrl + "?access_token=" + accessToken + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }

        /// <summary>
        /// 绑定别名app入口
        /// </summary>
        /// <param name="appUrl"></param>
        /// <param name="accessToken"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        internal static string GetAppEntryForSetAlias(string appUrl, string accessToken, string username, string alias, string returnUrl)
        {
            return appUrl + "?setalias=1&access_token=" + accessToken + "&u=" + username + "&a=" + alias + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }

        /// <summary>
        /// 设置用户组app入口
        /// </summary>
        /// <param name="appUrl"></param>
        /// <param name="accessToken"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        internal static string GetAppEntryForSetGroups(string appUrl, string accessToken, string alias, string[] groups, string returnUrl)
        {
            return appUrl + "?setgroups=1&access_token=" + accessToken + "&a=" + alias + (groups == null ? null : ("&g=" + string.Join("&g=", groups.Select(g => HttpUtility.UrlEncode(g))))) + "&return_url=" + HttpUtility.UrlEncode(returnUrl);
        }
    }
}