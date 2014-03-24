using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicPlatform.Client
{
    class UserProcessor : IUserProcessor
    {
        public bool Exists(string username)
        {
            return false;
        }

        public void CreateIfNotExists(string username)
        {
        }
    }
}
