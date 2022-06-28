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
    public interface IPostRepository : IRepository<Post>
    {
        Post FindById(int id);
        IQueryable<Post> GetPosts();
        IQueryable<Post> GetAll(Expression<Func<Post, bool>> predicate);
        IQueryable<Post> GetPostWithTranslations(Expression<Func<Post, bool>> predicate);
        IQueryable<Post> FindForNews(Expression<Func<Post, bool>> predicate);
        IQueryable<Post> FindForIndex(Expression<Func<Post, bool>> predicate, int count);
    }
}
