using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicPlatform.Web.Models;
using System.Web.Security;
using System.Diagnostics;

namespace BasicPlatform.Auth
{
    public class DefaultTokenProvider : ITokenProvider
    {
        /// <summary>
        /// 生成请求令牌
        /// </summary>
        /// <param name="user"></param>
        /// <param name="app"></param>
        /// <param name="userToken"></param>
        /// <returns></returns>
        public string GenerateRequestToken(User user, App app, string userToken)
        {
            if (user != null && app != null)
            {
                Debug.WriteLine("bc:userToken=" + userToken);
                return FormsAuthentication.HashPasswordForStoringInConfigFile(user.Id.ToString() + app.Id.ToString() + userToken, "md5");
            }
            else return null;
        }

        /// <summary>
        /// 生成校验令牌
        /// </summary>
        /// <param name="user"></param>
        /// <param name="app"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public string GenerateAccessToken(User user, App app, string secret)
        {
            if (user != null && app != null)
            {
                string accessToken = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Id.ToString() + app.Id.ToString() + secret, "md5");
                new BasicPlatform.Web.Models.AccessToken
                {
                    Token = accessToken,
                    App = app.Id,
                    User = user.Id
                }.Save();
                return accessToken;
            }
            else return null;
        }

        /// <summary>
        /// 校验访问令牌
        /// </summary>
        /// <param name="user"></param>
        /// <param name="app"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool ValidateAccessToken(User user, App app, string accessToken)
        {
            if (user != null && app != null)
            {
                var t = user.Id.GetAccessToken(app.Id);
                return t != null && t.Token.CompareTo(accessToken) == 0;
            }
            else return false;
        }

        /// <summary>
        /// 校验请求令牌
        /// </summary>
        /// <param name="user"></param>
        /// <param name="app"></param>
        /// <param name="userToken"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public bool ValidateRequestToken(User user, App app, string userToken, string secret)
        {
            if (user != null && app != null)
            {
                
                string requestToken = GenerateRequestToken(user, app, userToken);
                return secret.CompareTo(FormsAuthentication.HashPasswordForStoringInConfigFile(requestToken + app.Secret, "md5")) == 0;
            }
            else return false;
        }
    }
}