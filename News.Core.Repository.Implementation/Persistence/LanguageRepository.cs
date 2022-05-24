using Microsoft.EntityFrameworkCore;
using News.Core.Domain.Data;
using News.Core.Domain.Models;
using News.Core.Repository.Implementation.Repositories;
using News.Core.Repository.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Repository.Implementation.Persistence
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        public LanguageRepository(NewsDbContext db)
        : base(db)
        {

        }

        public NewsDbContext NewsDbContext => _db as NewsDbContext;

        public int AllCount() => _dbSet.Count();
        public Language FindById(int id) => _dbSet.Include(x => x.PostTranslations).Include(x => x.CategoryTranslations).FirstOrDefault(x => x.Id == id);
        public IQueryable<Language> FindForNews(Expression<Func<Language, bool>> predicate) => _dbSet.Include(x => x.PostTranslations).Where(predicate);
    }
}
