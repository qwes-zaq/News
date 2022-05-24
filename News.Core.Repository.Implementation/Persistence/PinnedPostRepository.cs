using Microsoft.EntityFrameworkCore;
using System.Linq;
using News.Core.Domain.Data;
using News.Core.Domain.Models;
using News.Core.Repository.Implementation.Repositories;
using News.Core.Repository.Persistence;

namespace News.Core.Repository.Implementation.Persistence
{
    public class PinnedPostRepository : Repository<PinnedPost>, IPinnedPostRepository
    {

        public PinnedPostRepository(NewsDbContext db)
            : base(db)
        {

        }
        public NewsDbContext NewsDbContext => _db as NewsDbContext; 

        public PinnedPost FindById(int id) => _dbSet.FirstOrDefault(x => x.Id == id);

        public IQueryable<Post> GetAllPosts()
        {
            return _dbSet.Include(x=>x.Post).Select(x=>x.Post);
        }


    }
}
