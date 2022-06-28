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
    public interface ILanguageRepository: IRepository<Language>
    {
        Language FindById(int id);
        Language FindByCodeOrDefault(string code);
        int AllCount();
        IQueryable<Language> FindForNews(Expression<Func<Language, bool>> predicate);
    }
}
