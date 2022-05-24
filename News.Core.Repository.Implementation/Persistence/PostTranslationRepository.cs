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
    public class PostTranslationRepository : Repository<PostTranslation>, IPostTranslationRepository
    {
        public PostTranslationRepository(NewsDbContext db)
        : base(db)
        {

        }

        public NewsDbContext NewsDbContext => _db as NewsDbContext;

        public PostTranslation FindById(int postId, int languageId) => _dbSet.FirstOrDefault(x => x.PostId == postId && x.LanguageId==languageId);

    }
}
