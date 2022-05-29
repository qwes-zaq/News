using Microsoft.AspNetCore.Mvc;
using News.Core.Domain.Models;
using News.Core.UnitOfWork.Core;
using News.Web.UI.Models.ViewModels.CategoryVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.ViewComponents
{
    public class CategoryList : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryList(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [Route("/[controller]/[action]/{language?}")]
        [Route("/[controller]/[action]/{id?}/{language?}")]
        public async Task<IViewComponentResult> InvokeAsync(string language="En")
        {
            List<CategoryVM> categoryList = new();
            //Language currentLanguage = _unitOfWork.Languages?.Get(x => x.LanguageCode.ToLower() == language.ToLower());
            var tmp = _unitOfWork.Languages.GetAll();

            Language currentLanguage = language != null ?
                tmp.FirstOrDefault(x => x.LanguageCode.ToLower() == language.ToLower()) ?? tmp.First()
                : tmp.First();

            int langId = currentLanguage.Id ;

            var x = _unitOfWork.Categories.GetAllCategories();

            foreach (var item in x)
            {
                var currentCategoryTranslation = item.CategoryTranslations.FirstOrDefault(x => x.LanguageId == langId);
                if (currentCategoryTranslation!= null)
                {
                    categoryList.Add(new() {CategoryName= currentCategoryTranslation.Title, CategoryId=item.Id});
                }
            }
            return View("Default", categoryList);
        }
    }
}
