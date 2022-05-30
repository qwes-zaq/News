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
    public interface IPostTagRepository : IRepository<PostTag>
    {
        public PostTag FindById(int postId, int tagId);
        public IQueryable<PostTag> FindFull(Expression<Func<PostTag, bool>> predicate);
    }
}
