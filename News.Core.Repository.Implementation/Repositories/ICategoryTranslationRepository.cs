using News.Core.Domain.Models;
using News.Core.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Repository.Implementation.Repositories
{
    public interface ICategoryTranslationRepository : IRepository<CategoryTranslation>
    {
        CategoryTranslation FindById(int categoryId, int languageId);
    }
}
