using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LogAndReadBackEnd.Persistence
{
    public interface IRepository<T> where T : class
    {
        public ICollection<T> GetAll();
        
        public T Get(Expression<Func<T, bool>> filter);

        public void Update(T entity);

        public void Delete(T entity);

        public void Add(T entity);

        public void Save();
    }
}
