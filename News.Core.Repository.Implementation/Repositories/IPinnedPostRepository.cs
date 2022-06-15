using News.Core.Domain.Models;
using News.Core.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Repository.Implementation.Repositories
{
    public interface IPinnedPostRepository : IRepository<PinnedPost>
    {
        PinnedPost FindById(int id);
        IQueryable<Post> GetAllPosts();
        IQueryable<Post> GetAllPostsWithTranslations();
    }
}
