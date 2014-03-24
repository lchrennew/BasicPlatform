using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicPlatform.Client
{
    class GroupProcessor : IGroupProcessor
    {
        public Dictionary<string, string> GetAllGroups()
        {
            return new Dictionary<string, string>();
        }

        public void SetGroupsOfUser(string username, params string[] groups)
        {
        }


        public IEnumerable<string> GetGroupsOfUser(string username)
        {
            return new string[0];
        }
    }
}
