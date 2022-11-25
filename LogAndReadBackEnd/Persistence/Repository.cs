namespace LogAndReadBackEnd.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;

    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly DataContext _db;
        private readonly DbSet<T> _dbSet;

        public Repository(DataContext dataContext)
        {
            this._db = dataContext;
            this._dbSet = this._db.Set<T>();
        }

        public ICollection<T> GetAll()
        {
            return this._dbSet.ToList();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = this._dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Update(T entity)
        {
            this._dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            this._dbSet.Remove(entity);
        }

        public void Add(T entity)
        {
            this._dbSet.Add(entity);
        }

        public void Save()
        {
            this._db.SaveChanges();
        }
    }
}