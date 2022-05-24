using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using News.Core.Domain.Models;
using News.Core.UnitOfWork.Core;
using News.Core.UnitOfWork.Persistense;
using News.Web.UI.Areas.Admin.Models.ViewModels.CategoryVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/{controller}/{action}")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _environment;
        private readonly UserManager<User> _userManager;


        public CategoryController(IUnitOfWork unitOfWork, IHostingEnvironment environment, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            List<CategoryVM> categoryVMs = new List<CategoryVM>() { };


            foreach (var z in _unitOfWork.Categories.Find(x => true))///// GetAll
            {
                var currentCategory = z;
                var currenrTranslation = z.CategoryTranslations.FirstOrDefault();
                var currentLanguage = currenrTranslation.Language.LanguageCode;

                categoryVMs.Add(new CategoryVM()
                {
                    CategoryId = currentCategory.Id,
                    CategoryLanguage = currentLanguage,
                    CategoryName = currenrTranslation.Title
                });
            }
            return View(categoryVMs);
        }

        [HttpGet]
        public IActionResult Create()
        {

            CategoryCreateVM categoryCreateVM = new()
            {
                LanguagesList = _unitOfWork.Languages.GetAll().Select(a => new SelectListItem { Text = a.LanguageCode, Value = a.Id.ToString() })
            };


            return View(categoryCreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]   
        public async Task<IActionResult> CreateAsync(CategoryCreateVM categoryCreateVm)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                Category category = new()
                {
                    AddedBy = currentUser,
                    UpdatedUser = currentUser,
                    AddedDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                    
                };

                category.CategoryTranslations.Add(new()
                {
                    AddedBy = currentUser,
                    UpdatedUser = currentUser,
                    AddedDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Category = category,
                    LanguageId = categoryCreateVm.LanguageId,
                    Title = categoryCreateVm.Title,
                    Description = categoryCreateVm.Description,
                    Slug = categoryCreateVm.Slug
                });
                _unitOfWork.Categories.Add(category);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return RedirectToAction(nameof(Index));
            }
            categoryCreateVm.LanguagesList = _unitOfWork.Languages.GetAll().Select(a => new SelectListItem { Text = a.LanguageCode, Value = a.Id.ToString() });


            return View(categoryCreateVm);
        }


        [HttpGet]
        [Route("/[area]/[controller]/[action]/{id}")]
        public IActionResult Edit(int id)//?
        {
            Category currentCategory = _unitOfWork.Categories.Get(z => z.Id == id);

            if (currentCategory == null)
            {
                ViewBag.Info = "There is not a such category"; //?
                return RedirectToAction(nameof(Index));
            }

            List<Language> languages = _unitOfWork.Languages.GetAll().ToList();
            CategoryEditListVM categoryEditListVM = new()
            {
                CategoryId = id,
                OrderIndex = currentCategory.OrderIndex,
                CategoryTranslationList = new() //= currentCategory.CategoryTranslations.Select(x => new CategoryEditVM() { LanguageId = x.LanguageId, Description = x.Description, Slug = x.Slug, Title = x.Title, LanguageCode = x.Language.LanguageCode }).ToList()
            };

            foreach (var i in languages)
            {
                var tmp = currentCategory.CategoryTranslations.FirstOrDefault(x => x.CategoryId == currentCategory.Id && x.LanguageId == i.Id);
                categoryEditListVM.CategoryTranslationList.Add(new CategoryEditVM()
                {
                    LanguageId = i.Id,
                    Description = tmp?.Description,
                    Slug = tmp?.Slug,
                    Title = tmp?.Title,
                    LanguageCode = i.LanguageCode
                });
            }

            return View(categoryEditListVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/[area]/[controller]/[action]/{id}")]
        public async Task<IActionResult> EditAsync(CategoryEditListVM model)
        {
            bool modelIsValid = false;
            foreach (var i in model.CategoryTranslationList)
            {
                if (i.Slug != null && i.Title != null)
                {
                    modelIsValid = true;
                    break;
                }
            }
            if (!modelIsValid)
            {
                ViewBag.Edit = "Model is not valid";
                return View(model);
            }


            Category category = _unitOfWork.Categories.Get(x => x.Id == model.CategoryId);
            //category.CategoryTranslations = new List<CategoryTranslation>();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            foreach (var i in model.CategoryTranslationList)
            {
                var tmp = category.CategoryTranslations.FirstOrDefault(x=>x.LanguageId== i.LanguageId && x.CategoryId==model.CategoryId);
                if (i.Slug != null && i.Title != null)
                {
                    if (tmp == null)
                    {
                        tmp = new()
                        {
                            AddedBy = currentUser,
                            AddedDate = DateTime.Now,
                            LanguageId = i.LanguageId,
                            CategoryId = model.CategoryId

                        };
                        category.CategoryTranslations.Add(tmp);
                    }

                    tmp.UpdateDate = DateTime.Now;
                    tmp.UpdatedUser = currentUser;
                    tmp.Slug = i.Slug;
                    tmp.Description = i.Description;
                    tmp.Title = i.Title;
                }
                else if (tmp?.Slug != null && tmp?.Title != null && i.Slug == null && i.Title == null)
                {
                    var deletedCAtegoryTranslation = category.CategoryTranslations.First(x=>x.CategoryId==model.CategoryId);
                    category.CategoryTranslations.Remove(deletedCAtegoryTranslation);
                }

                
            }
            _unitOfWork.Categories.Update(category);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));

        }
    }
}
