using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using System.Configuration;

namespace BasicPlatform.Config
{
    public static class Databases
    {
        static MongoClient client;
        static Databases()
        {
            client = new MongoClient(ConfigurationManager.ConnectionStrings["Bp.Mongo"].ConnectionString);
            Mongo = client.GetServer().GetDatabase("bp");
        }
        public static MongoDatabase Mongo { get; private set; }
    }
}
