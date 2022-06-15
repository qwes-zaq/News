using News.Core.Domain.Models;
using News.Core.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Repository.Implementation.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {

        public IQueryable<Category> GetAllCategories();
        public IQueryable<Category> GetFullAllCategories();
        public IQueryable<Category> FindWithPosts(Expression<Func<Category, bool>> predicate);
        public IQueryable<Category> GetAll(Expression<Func<Category, bool>> predicate);
        public Category FindById(int id);
    }
}
