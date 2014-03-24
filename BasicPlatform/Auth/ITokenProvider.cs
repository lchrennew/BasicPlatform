using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicPlatform.Web.Models;

namespace BasicPlatform.Auth
{
    public interface ITokenProvider
    {

        string GenerateRequestToken(User user, App app, string userToken);

        string GenerateAccessToken(User user, App app, string requestToken);

        bool ValidateAccessToken(User user, App app, string accessToken);

        bool ValidateRequestToken(User user, App app, string userToken, string secret);
    }
}