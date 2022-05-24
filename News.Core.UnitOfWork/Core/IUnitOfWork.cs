using News.Core.Domain.Data;
using News.Core.Repository.Implementation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.UnitOfWork.Core
{
    public interface IUnitOfWork : IDisposable
    {
        NewsDbContext Context { get; }
        ICategoryRepository Categories { get; }
        ICategoryTranslationRepository CategoryTranslations { get; }
        ILanguageRepository Languages { get; }
        IPinnedPostRepository PinnedPosts { get; }
        IPostRepository Posts { get; }
        IPostTagRepository PostTags { get; }
        IPostTranslationRepository PostTranslations { get; }
        ITagRepository Tags { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}
