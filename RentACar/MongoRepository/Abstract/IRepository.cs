using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository.Abstract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(ObjectId id);
        IEnumerable<T> Find(Expression<Func<T, bool>> filter);
        T FindItem(Expression<Func<T, bool>> filter);
        void Insert(T entity);
        void Update(ObjectId id, T entity);
        void Delete(ObjectId id);
    }
}
