using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LogAndReadBackEnd.Persistence;

namespace UnitTest
{
    public class MockRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _list = new ();
        public ICollection<T> GetAll()
        {
            return _list;
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _list.Where(filter.Compile()).SingleOrDefault();
        }

        public void Update(T entity)
        {
        }

        public void Delete(T entity)
        {
            _list.Remove(entity);
        }

        public void Add(T entity)
        {
            _list.Add(entity);
        }

        public void Save()
        {
            return;
        }
    }
}
