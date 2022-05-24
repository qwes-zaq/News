using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using News.Core.Domain.Data;
using News.Core.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Repository.Persistence
{

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly NewsDbContext _db;

        protected DbSet<T> _dbSet;

        public Repository(NewsDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public void Add(T entity) => _dbSet.Add(entity);
        public void AddRange(IEnumerable<T> entities) => _dbSet.AddRange(entities);
        public int Count(Expression<Func<T, bool>> predicate) => _dbSet.Count(predicate);
        //public int Count(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes) => includes(_dbSet.AsQueryable()).Count(predicate);
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);
        //public IQueryable<T> Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes) => includes(_dbSet.AsQueryable()).Where(predicate);
        public T Get(Expression<Func<T, bool>> predicate) => _dbSet.FirstOrDefault(predicate);
        //public T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes) => includes(_dbSet.AsQueryable()).FirstOrDefault(predicate);
        public IQueryable<T> GetAll() => _dbSet;
        //public IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes) => includes(_dbSet.AsQueryable());
        public bool GetAny(Expression<Func<T, bool>> predicate) => _dbSet.Any(predicate);
        public Task<T> GetAsync(Expression<Func<T, bool>> predicate) => _dbSet.FirstOrDefaultAsync(predicate);
        //public Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes) => includes(_dbSet.AsQueryable()).FirstOrDefaultAsync(predicate);
        public void Remove(T entity) => _dbSet.Remove(entity);
        public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
        public void Update(T entity) => _dbSet.Update(entity);
        public void UpdateRange(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);
    }
}
