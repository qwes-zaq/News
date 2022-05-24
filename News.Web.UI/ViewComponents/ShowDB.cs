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
    public class ShowDB : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShowDB(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [Route("/[controller]/[action]/{language?}")]
        public async Task<IViewComponentResult> InvokeAsync(string language="En")
        {
            List<CategoryVM> categoryList = new();// ;
            Language currentLanguage = _unitOfWork.Languages?.Get(x => x.LanguageCode.ToLower() == language.ToLower());
            int langId = currentLanguage?.Id ?? _unitOfWork.Languages.GetAll().FirstOrDefault().Id;

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
