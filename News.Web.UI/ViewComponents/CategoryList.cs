using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using News.Core.Domain.Models;
using News.Core.UnitOfWork.Core;
using News.Web.UI.Localization.Interface;
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
        private readonly ILocalizer _localizer;

        public CategoryList(IUnitOfWork unitOfWork, ILocalizer localizer)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }


        [Route("/[controller]/[action]/{language?}")]
        [Route("/[controller]/[action]/{id?}/{language?}")]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CategoryVM> categoryList = new();
            //Language currentLanguage = _unitOfWork.Languages?.Get(x => x.LanguageCode.ToLower() == language.ToLower());
            //var tmp = _unitOfWork.Languages.GetAll();

            //Language currentLanguage = language != null ?
            //    tmp.FirstOrDefault(x => x.LanguageCode.ToLower() == language.ToLower()) ?? tmp.First()
            //    : tmp.First();

            //int langId = currentLanguage.Id ;

            var x = _unitOfWork.Categories.GetAllCategories();

            foreach (var item in x)
            {
                if (_localizer.CurrentCategoryTranslation(item.Id) != null)
                {
                    categoryList.Add(new() {CategoryName= _localizer.CurrentCategoryTranslation(item.Id).Title, CategoryId=item.Id});
                }
            }
            return View("Default", categoryList);
        }
    }
}
