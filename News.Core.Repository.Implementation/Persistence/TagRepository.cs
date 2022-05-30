using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using News.Core.Domain.Data;
using News.Core.Domain.Models;
using News.Core.Repository.Implementation.Repositories;
using News.Core.Repository.Persistence;

namespace News.Core.Repository.Implementation.Persistence
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(NewsDbContext db)
            : base(db)
        {

        }

        public NewsDbContext NewsDbContext => _db as NewsDbContext;

        public Tag FindById(int id) 
            => _dbSet
                .Include(x=>x.PostTags)
                    .ThenInclude(x=>x.Post)
                        .ThenInclude(x=>x.PostTranslations)
            .FirstOrDefault(x => x.Id == id);
    }
}
