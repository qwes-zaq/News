using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using News.Core.Domain.Data;
using News.Core.Domain.Models;
using News.Core.Repository.Implementation.Repositories;
using News.Core.Repository.Persistence;

namespace News.Core.Repository.Implementation.Persistence
{
    public class PostTagRepository : Repository<PostTag>, IPostTagRepository
    {
        public PostTagRepository(NewsDbContext db)
            : base(db)
        {

        }

        public NewsDbContext NewsDbContext => _db as NewsDbContext;

        public PostTag FindById(int postId, int tagId) => _dbSet.FirstOrDefault(x => x.TagId == tagId && x.PostId == postId);


        public IQueryable<PostTag> FindFull(Expression<Func<PostTag, bool>> predicate) 
            => _dbSet
                .Include(x => x.Tag)
                .Include(x=>x.Post)
                    .ThenInclude(x=>x.PostTranslations);

        public new IQueryable<PostTag> Find(Expression<Func<PostTag, bool>> predicate) => _dbSet.Include(x=>x.Post).Include(x=>x.Tag).Where(predicate);
    }
}
