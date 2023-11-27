using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoRepository.Context
{
    public class MongoConnection
    {
        string url = "mongodb://localhost:27017";
    
        public MongoConnection()
        {
            var cnn = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
            var dbName = MongoUrl.Create(cnn).DatabaseName;
            var client=new MongoClient(cnn);
            var getdb=client.GetDatabase(dbName);
        }
    }
}
