using HepsiYemek.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiYemek.DataAccess
{
    public class MongoDataContext
    {
        private readonly IMongoDatabase _db;

        public MongoDataContext(IMongoClient client, string dbName)
        {
            _db = client.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }

        public IMongoCollection<Product> Products => _db.GetCollection<Product>("products");
        public IMongoCollection<Product> Categories => _db.GetCollection<Product>("categories");
    }
}
