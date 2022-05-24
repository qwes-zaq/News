using Microsoft.EntityFrameworkCore;
using News.Core.Domain.Data;
using News.Core.Domain.Models;
using News.Core.Repository.Implementation.Repositories;
using News.Core.Repository.Persistence;
using System.Linq;

namespace News.Core.Repository.Implementation.Persistence
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(NewsDbContext db)
            : base(db)
        {

        }

        public NewsDbContext NewsDbContext => _db as NewsDbContext;

        public User FindById(string id) => _dbSet.FirstOrDefault(x => x.Id == id);

        public IQueryable<Post> GetPosts(string id)
        {
            return (IQueryable<Post>)_dbSet.Include(x=>x.Posts).FirstOrDefault(x=>x.Id==id).Posts;
        }
    }
}
