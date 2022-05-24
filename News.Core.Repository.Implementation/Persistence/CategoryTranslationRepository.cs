using News.Core.Domain.Data;
using News.Core.Domain.Models;
using News.Core.Repository.Implementation.Repositories;
using News.Core.Repository.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Repository.Implementation.Persistence
{
    public class CategoryTranslationRepository : Repository<CategoryTranslation> , ICategoryTranslationRepository
    {
        public CategoryTranslationRepository(NewsDbContext db)
        : base(db)
        {

        }

        public NewsDbContext NewsDbContext => _db as NewsDbContext;

        public CategoryTranslation FindById(int categoryId, int languageId) => _dbSet.FirstOrDefault(x => x.CategoryId == categoryId && x.LanguageId == languageId);

    }
}
