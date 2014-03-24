using System;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Net;

namespace BasicPlatform.Core
{
    /// <summary>
    /// Request帮助类
    /// </summary>
    public static class RequestHelper
    {
        /// <summary>
        /// 根据路径获取映射路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(path);
            }
            else //非web程序引用
            {
                path = Regex.Replace(path, @"\A~?/", "").Replace("/", @"\");
                return Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, path);
            }
        }

        /// <summary>
        /// 根据key获取url参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Query(string key, HttpContext context = null)
        {
            context = context ?? HttpContext.Current;
            var str = context.Request.QueryString[key];
            return String.IsNullOrEmpty(str) ? string.Empty : HttpUtility.UrlDecode(Safe(str.Trim()));
        }

        /// <summary>
        /// 获取原始url
        /// </summary>
        /// <returns></returns>
        public static string RawUrl()
        {
            string rawUrl = HttpContext.Current.Request.ServerVariables["HTTP_X_ORIGINAL_URL"] ?? HttpContext.Current.Request.ServerVariables["HTTP_X_REWRITE_URL"];
            if (string.IsNullOrEmpty(rawUrl))
            {
                rawUrl = HttpContext.Current.Request.RawUrl;
            }
            return string.Format("http://{0}{1}", HttpContext.Current.Request.Url.Host, rawUrl);

        }

        /// <summary>
        /// 获取当前url参数部分
        /// </summary>
        /// <returns></returns>
        public static string UrlParams()
        {
            var url = RawUrl();
            return url.Substring(url.IndexOf('?') + 1);
        }

        /// <summary>
        /// 根据参数key获取参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Params(string key)
        {
            var val = HttpContext.Current.Request.Params[key];
            return val ?? string.Empty;
        }

        /// <summary>
        /// 获取key获取Form值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Form(string key)
        {
            var str = HttpContext.Current.Request.Form[key];
            return str == null ? string.Empty : str.Trim();
        }

        /// <summary>
        /// url安全过滤
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Safe(string str)
        {
            return str.Replace("'", string.Empty);
        }

        /// <summary>
        /// 移除Html标签
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NonHtml(string str)
        {
            return Regex.Replace(str, @"</?[^<]+>", "");
        }

        /// <summary>
        /// 获取客户端ip
        /// </summary>
        /// <returns></returns>
        public static string GetIp(HttpContext context = null)
        {
            context = context ?? HttpContext.Current;
            if (context == null) return "127.0.0.1";
            string HTTP_X_FORWARDED_FOR = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? context.Request.Headers["X-Forwarded-For"];
            var remoteAddr = context.Request.ServerVariables["REMOTE_ADDR"];
            var httpVia = context.Request.ServerVariables["HTTP_VIA"];
            return HTTP_X_FORWARDED_FOR ?? (String.IsNullOrEmpty(httpVia) ? remoteAddr : httpVia);
        }

        /// <summary>
        /// 从页面请求获取整型
        /// </summary>
        /// <param name="str">请求key</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetInt(string str, int defaultValue = 0, HttpContext context = null)
        {
            context = context ?? HttpContext.Current;
            int result;
            if (!int.TryParse(context.Request[str], out result)) result = defaultValue;
            return result;
        }

        public static int[] GetIntArray(string str, HttpContext context = null)
        {
            context = context ?? HttpContext.Current;
            string[] forms = context.Request.Form.GetValues(str) ?? context.Request.QueryString.GetValues(str);
            if (forms != null)
            {
                List<int> intValues = new List<int>();
                int v = 0;
                foreach (var f in forms)
                {
                    if (int.TryParse(f, out v))
                        intValues.Add(v);
                }
                return intValues.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从页面请求获取时间
        /// </summary>
        /// <param name="str">请求key</param>
        /// <param name="defatulValue">默认值</param>
        /// <returns>获得的日期</returns>
        public static DateTime GetDate(string str, DateTime defatulValue)
        {
            DateTime date;
            if (!DateTime.TryParse(HttpContext.Current.Request[str], out date)) date = defatulValue;
            return date;
        }

        /// <summary>
        /// 301跳转
        /// </summary>
        /// <param name="url">要跳转的地址</param>
        public static void Rediret301(string url)
        {
            Rediret301(HttpContext.Current, url);
        }

        /// <summary>
        /// 301跳转
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <param name="url">要跳转的地址</param>
        public static void Rediret301(HttpContext context, string url)
        {
            context.Response.Clear();
            context.Response.Status = "301 Moved Permanently";
            context.Response.AddHeader("Location", url);
            context.Response.End();
        }

        public static string PickImageUrl(string[] urls, HttpContext context = null)
        {
            context = context ?? HttpContext.Current;
            if (context != null)
            {
                int i = Convert.ToInt32(context.Items["pu"] ?? urls.Length);
                i++;
                i %= urls.Length;
                context.Items["pu"] = i;
                return urls[i];
            }
            else
            {
                return urls[new Random().Next(0, urls.Length - 1)];
            }

        }
        static Regex domainNamePattern = new Regex(@"\A((?=[a-z0-9-]{1,63}\.)[a-z0-9]+(-[a-z0-9]+)*\.)+[a-z]{2,63}(?::(?:6553[0-5]|655[0-2]\d|65[0-4]\d{2}|6[0-4]\d{3}|[0-5]?\d{1,4}))?\Z", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        static Regex ipPattern = new Regex(@"\A(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4]\d|[01]?\d{1,2})(?::(?:6553[0-5]|655[0-2]\d|65[0-4]\d{2}|6[0-4]\d{3}|[0-5]?\d{1,4}))?\Z");
        /// <summary>
        /// 是否字符串是域名
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static bool IsDomainName(this string domain)
        {
            return domainNamePattern.IsMatch(domain) || ipPattern.IsMatch(domain);
        }
    }
}