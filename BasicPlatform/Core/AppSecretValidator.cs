using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Selectors;
using BasicPlatform.Web.Models;
using System.IdentityModel.Tokens;

namespace BasicPlatform.Core
{
    public class AppSecretValidator : UserNamePasswordValidator
    {
        public override void Validate(string appKey, string appSecret)
        {
            if (!string.IsNullOrEmpty(appKey) && !string.IsNullOrEmpty(appSecret))
            {
                if (!appKey.CheckApp(appSecret))
                {
                    throw new SecurityTokenException("appKey or appSecret is not correct");
                }
            }
            else
            {
                throw new ArgumentNullException("appKey or appSecret cannot be empty");
            }
        }
    }
}