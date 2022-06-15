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
    public interface IPostTranslationRepository : IRepository<PostTranslation>
    {
        PostTranslation FindById(int postId, int languageId);
        IQueryable<PostTranslation> GetPostTranlations(Expression<Func<PostTranslation, bool>> predicate);
    }
}
