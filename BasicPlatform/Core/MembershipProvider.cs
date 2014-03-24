using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicPlatform.Web.Models;
using MongoDB.Bson;
using System.Web.Security;
using MongoDB.Driver.Builders;

namespace BasicPlatform.Core
{
    public static class MembershipUserExtension
    {
        public static MembershipUser AsUser(this User u, string appName)
        {
            if (u == null) return null;
            return new MembershipUser(appName, u.Username, u.Id, u.Email, string.Empty, string.Empty, true, false, default(DateTime), default(DateTime), default(DateTime), default(DateTime), default(DateTime));
        }
    }
    public class MembershipProvider : System.Web.Security.MembershipProvider
    {

        public override string ApplicationName { get; set; }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var u = username.GetUser();
            if (u.IsAnonymous()) return false;
            else if (u.CheckPassword(oldPassword))
            {
                u.Password = newPassword;
                u.Save(true);
                return true;
            }
            else return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            if (!((ObjectId)providerUserKey).GetUser().IsAnonymous()) status = MembershipCreateStatus.DuplicateProviderUserKey;
            else if (!username.GetUser().IsAnonymous()) status = MembershipCreateStatus.DuplicateUserName;
            else if (!email.GetUser().IsAnonymous()) status = MembershipCreateStatus.DuplicateEmail;
            else
            {
                var u = new User { Username = username, Password = password, Email = email, Label = "无名氏", Id = (ObjectId)providerUserKey };
                u.Save(true);
                status = MembershipCreateStatus.Success;
                return u.AsUser(this.Name);
            }
            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            username.GetUser().Id.DeleteUser();
            return true;
        }

        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return true; }
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            long totalRecords64;
            var c = new MembershipUserCollection();
            var r = Query.EQ("e", emailToMatch).GetUsers(pageIndex, pageSize, out totalRecords64).Select(x => { c.Add(x.AsUser(this.Name)); return x; });
            totalRecords = (int)totalRecords64;
            return c;
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            long totalRecords64;
            var c = new MembershipUserCollection();
            var r = Query.EQ("n", usernameToMatch).GetUsers(pageIndex, pageSize, out totalRecords64).Select(x => { c.Add(x.AsUser(this.Name)); return x; });
            totalRecords = (int)totalRecords64;
            return c;
        }

        public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            long totalRecords64;
            var c = new MembershipUserCollection();
            var r = Query.Null.GetUsers(pageIndex, pageSize, out totalRecords64).Select(x => { c.Add(x.AsUser(this.Name)); return x; });
            totalRecords = (int)totalRecords64;
            return c;
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            return username.GetUser().AsUser(this.Name);
        }

        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return ((ObjectId)providerUserKey).GetUser().AsUser(this.Name);
        }

        public override string GetUserNameByEmail(string email)
        {
            var u = email.GetUser();
            if (u.IsAnonymous()) return null;
            else return u.Username;
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 0; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 0; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 0; }
        }

        public override System.Web.Security.MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Encrypted; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return null; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            return !username.ValidateUser(password).IsAnonymous();
        }
    }
}