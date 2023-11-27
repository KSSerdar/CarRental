using MongoDB.Bson;
using MongoDB.Driver;
using MongoRepository.Abstract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        string _connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
        string _dataName;
        public GenericRepository(string collection)
        {
           
            MongoClient client = new MongoClient(_connectionString);
            _dataName=MongoUrl.Create(_connectionString).DatabaseName;
            IMongoDatabase mongoDatabase=client.GetDatabase(_dataName);
            _collection = mongoDatabase.GetCollection<T>(collection);
        }
        public IEnumerable<T> GetAll()
        {
            return _collection.Find(new BsonDocument()).ToList();
        }

        public T GetById(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return _collection.Find(filter).FirstOrDefault();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            return _collection.Find(filter).ToList();
        }

        public void Insert(T entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(ObjectId id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            _collection.ReplaceOne(filter, entity);
        }

        public void Delete(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            _collection.DeleteOne(filter);
        }

        public T FindItem(Expression<Func<T, bool>> filter)
        {
            return _collection.Find(filter).FirstOrDefault();
        }
    }
}
