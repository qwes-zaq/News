using Microsoft.EntityFrameworkCore;
using News.Core.Domain.Data;
using News.Core.Repository.Implementation.Persistence;
using News.Core.Repository.Implementation.Repositories;
using News.Core.UnitOfWork.Core;

namespace News.Core.UnitOfWork.Persistense
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NewsDbContext _context;
        public UnitOfWork(NewsDbContext context)
        {
            _context = context;
            Context = context;
            Categories = new CategoryRepository(_context);
            CategoryTranslations = new CategoryTranslationRepository(_context); 
            Languages = new LanguageRepository(_context);
            PinnedPosts = new PinnedPostRepository(_context);
            Posts = new PostRepository(_context);
            PostTags = new PostTagRepository(_context);
            PostTranslations = new PostTranslationRepository(_context);
            Tags = new TagRepository(_context);
            Users = new UserRepository(_context);
        }
        public NewsDbContext Context { get; }
        public ICategoryRepository Categories { get; }
        public ICategoryTranslationRepository CategoryTranslations { get; }
        public ILanguageRepository Languages { get; }
        public IPinnedPostRepository PinnedPosts { get; }
        public IPostRepository Posts { get; }
        public IPostTagRepository PostTags { get; }
        public IPostTranslationRepository PostTranslations { get; }
        public ITagRepository Tags { get; }
        public IUserRepository Users { get; }


        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        _disposed = true;
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //    }
        //}

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

    }
}
