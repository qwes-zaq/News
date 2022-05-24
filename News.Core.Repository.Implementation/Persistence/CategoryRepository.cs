using Microsoft.EntityFrameworkCore;
using News.Core.Domain.Data;
using News.Core.Domain.Models;
using News.Core.Repository.Implementation.Repositories;
using News.Core.Repository.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace News.Core.Repository.Implementation.Persistence
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(NewsDbContext db)
        : base(db)
        {

        }

        public NewsDbContext NewsDbContext => _db as NewsDbContext;

        // public NewsDbContext NewsDbContext => _db; //?
        public IQueryable<Category> GetAllCategories() 
        {
            return _dbSet
            .Include(x => x.CategoryTranslations)
            .ThenInclude(t => t.Language);
        }
        public Category FindById(int id) => _dbSet.FirstOrDefault(x => x.Id == id);


        public IEnumerable<Category> GetMany(Expression<Func<Category, bool>> predicate)   //return all categories where predicate is true
        {
            return Queryable.Where(_dbSet, predicate);
        }
        public IQueryable<Category> GetAll(Expression<Func<Category, bool>> predicate) 
        {
            return _dbSet
            .Include(x => x.CategoryTranslations)
            .ThenInclude(t => t.Language)
            .Where(predicate);
        }
        public new IQueryable<Category> Find(Expression<Func<Category, bool>> predicate)   // ?   // return all categories with translations where predicate is true
        {
            return _dbSet
            .Include(x => x.CategoryTranslations)
            .ThenInclude(t => t.Language)
            .Where(predicate);
        }
        public IQueryable<Category> FindWithPosts(Expression<Func<Category, bool>> predicate) // return all categories with translations and Posts where predicate is true
        {
            return _dbSet
            .Include(x => x.CategoryTranslations)
            .ThenInclude(t => t.Language)
            .Include(x => x.Posts).ThenInclude(x => x.PostTranslations).ThenInclude(t => t.Language)
            .Where(predicate);
        }

        public new Category Get(Expression<Func<Category, bool>> predicate) // return first or default category where predicate is true
        {
            return _dbSet
            .Include(x => x.CategoryTranslations)
            .ThenInclude(t => t.Language)
            .FirstOrDefault(predicate);
        }



        //public IDTPagedList<CategoryDTViewModel> FindDataTable(Expression<Func<Category, bool>> predicate, string lang, int pageNumber, int pageSize, string sortColumn, string sortColumnDirection, string searchValue)
        //{
        //    var recordsTotal = _dbSet.Where(predicate).Count();



        //    var list = _dbSet
        //    .Include(x => x.CategoryTranslations).ThenInclude(x => x.Language)
        //    .Where(predicate)
        //    .Select(x => new CategoryDTViewModel
        //    {
        //        Id = x.Id,
        //        Name = !(x.CategoryTranslations.Any(y => y.Language.LanguageCode == "az")) ? "---" : x.CategoryTranslations.FirstOrDefault(y => y.Language.LanguageCode == lang).Name,
        //        OrderIndex = Convert.ToInt32(x.OrderIndex),
        //        AddedDateStr = x.AddedDate.ToString("dd/MM/yyyy"),
        //        AddedDate = x.AddedDate
        //    });



        //    //Sorting
        //    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        //    {
        //        if (sortColumnDirection == "asc")   //?
        //        {
        //            switch (sortColumn)
        //            {
        //                case nameof(CategoryDTViewModel.Name):
        //                    list = list.OrderBy(p => p.Name);
        //                    break;
        //                case nameof(CategoryDTViewModel.OrderIndex):
        //                    list = list.OrderBy(p => p.OrderIndex);
        //                    break;
        //                case nameof(CategoryDTViewModel.AddedDateStr):
        //                    list = list.OrderBy(p => p.AddedDate);
        //                    break;
        //                default:
        //                    list = list.OrderBy(p => p.OrderIndex);
        //                    break;
        //            }
        //        }
        //        else if (sortColumnDirection == "desc")    //?
        //        {
        //            switch (sortColumn)
        //            {
        //                case nameof(CategoryDTViewModel.Name):
        //                    list = list.OrderByDescending(p => p.Name);
        //                    break;
        //                case nameof(CategoryDTViewModel.OrderIndex):
        //                    list = list.OrderByDescending(p => p.OrderIndex);
        //                    break;
        //                case nameof(CategoryDTViewModel.AddedDateStr):
        //                    list = list.OrderByDescending(p => p.AddedDate);
        //                    break;
        //                default:
        //                    list = list.OrderByDescending(p => p.OrderIndex);
        //                    break;
        //            }
        //        }
        //    }



        //    //search
        //    if (!string.IsNullOrEmpty(searchValue))
        //    {
        //        searchValue = searchValue.ToLower();



        //        list = list.Where(m =>
        //        m.Name.ToLower().Contains(searchValue) ||
        //        m.OrderIndex.ToString().Contains(searchValue) ||
        //        m.AddedDateStr.ToLower().Contains(searchValue)
        //        );
        //    }



        //    //total number of rows count
        //    var recordsFiltered = list.Count();



        //    //Paging
        //    var data = list.Skip(pageNumber).Take(pageSize).ToList();




        //    return new DTPagedList<CategoryDTViewModel>(data, recordsTotal, recordsFiltered);




        //}

        #region

        //public IQueryable<CategoryTranslation> GetAllTranslations(int id)
        //{
        //    //IQueryable < CategoryTranslation >

        //    //return _dbSet
        //    //    .Include(x=>x.CategoryTranslations)
        //    //    .FirstOrDefault(x=> x.Id== id);

        //    return _db.Set<CategoryTranslation>()
        //        .Where(x => x.CategoryId == id);
        //}

        //public IQueryable<Post> GetAllPosts(int id)
        //{
        //    return _db.Set<Post>()
        //        .Include(x => x.PostTranslations)
        //        .Where(x => x.CategoryId == id);
        //}

        //public void AddTranslation(CategoryTranslation categoryTranslation) => _db.Set<CategoryTranslation>().Add(categoryTranslation);

        //public IQueryable<CategoryTranslation> GetTranslationsList(int id) => _db.Set<CategoryTranslation>().Where(x => x.CategoryId == id);

        //public void UpdateTranslation(CategoryTranslation categoryTranslation) => _db.Set<CategoryTranslation>().Update(categoryTranslation);

        //public void DeleteTranslation(CategoryTranslation categoryTranslation) //?
        //{
        //    _db.Set<CategoryTranslation>().Remove(categoryTranslation);

        //}
        #endregion
    }
}
