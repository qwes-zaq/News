using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using News.Core.Domain.Data;
using News.Core.Domain.Models;
using News.Core.Repository.Implementation.Repositories;
using News.Core.Repository.Persistence;

namespace News.Core.Repository.Implementation.Persistence
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(NewsDbContext db) 
            :base(db)
        {

        }

        public NewsDbContext NewsDbContext => _db as NewsDbContext;

        public IQueryable<Post> GetPosts()
        {
            return _dbSet
                .Include(x => x.PostTranslations)
                .ThenInclude(x => x.Language)
                .Include(x => x.Category)
                .ThenInclude(x => x.CategoryTranslations);
        }
        public IQueryable<Post> GetAll(Expression<Func<Post, bool>> predicate)
        {
            return _dbSet
                .Include(x => x.PostTranslations)
                .ThenInclude(x => x.Language)
                .Include(x => x.Category)
                .ThenInclude(x => x.CategoryTranslations)
                .Where(predicate);
        }
        public Post FindById(int id) 
            => _dbSet
                .Include(x=>x.PostTranslations)
                .Include(x => x.UpdatedUser)
                .Include(x => x.AddedBy)
                .Include(x=>x.Category)
                    .ThenInclude(x=> x.CategoryTranslations)
                .FirstOrDefault(x => x.Id == id);

        public IQueryable<Post> FindForIndex(Expression<Func<Post, bool>> predicate, int count) => _dbSet.Where(predicate).Take(count);

        public IQueryable<Post> FindForNews(Expression<Func<Post, bool>> predicate)
        => _dbSet.Include(x => x.PostTranslations).Where(predicate);
    }
}
