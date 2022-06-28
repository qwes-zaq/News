using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using News.Core.Domain.Models;
using News.Core.UnitOfWork.Core;
using News.Web.UI.Areas.Admin.Models.ViewModels.PostOrderVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class PostOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;


        public PostOrderController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        [Route("{language?}")]
        public IActionResult Index(string language)
        {
            List<PostOrderVM> postVMs = new();

            var tmp = _unitOfWork.Languages.GetAll();
            Language currentLanguage = language != null ?
                tmp.FirstOrDefault(x => x.LanguageCode.ToLower() == language.ToLower()) ?? tmp.First()
                : tmp.First();

            foreach (var i in _unitOfWork.Posts.GetAll(x => x.Status == (int)Core.Domain.Enums.Status.Active))
            {
                PostTranslation postTranslation = i.PostTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id);
                if (postTranslation.Title == null)
                {
                    postTranslation = i.PostTranslations.First(x => x.Title != null);
                }
                CategoryTranslation categoryTranslation = i.Category.CategoryTranslations.First(x => x.LanguageId == postTranslation.LanguageId);

                postVMs.Add(new PostOrderVM()
                {
                    PostId = i.Id,
                    CategoryName = categoryTranslation.Title,
                    PostTitle = postTranslation.Title,
                    PostOrderIndex = i.OrderIndex,
                    LanguageCode = postTranslation.Language.LanguageCode
                });
            }
            return View(postVMs);
        }
        [HttpGet]
        [Route("{language?}")]
        public IActionResult Create(string language)
        {
            PostOrderCreateVM model = new() { 
                PostList1 = new(),
            };

            var tmp = _unitOfWork.Languages.GetAll();
            Language currentLanguage = language != null ?
                tmp.FirstOrDefault(x => x.LanguageCode.ToLower() == language.ToLower()) ?? tmp.First()
                : tmp.First();

            foreach (var i in _unitOfWork.Posts.GetAll(x => x.Status == (int)Core.Domain.Enums.Status.Active))
            {
                PostTranslation postTranslation = i.PostTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id);
                if (postTranslation.Title == null)
                {
                    postTranslation = i.PostTranslations.First(x => x.Title != null);
                }
                model.PostList1.Add(new SelectListItem()
                {
                    Value = i.Id.ToString(),
                    Text = postTranslation.Title
                });
            }

            model.PostList2 = model.PostList1;
            model.PostList3 = model.PostList1;
            model.PostList4 = model.PostList1;
            model.PostList5 = model.PostList1;
            return View(model);
        }


        [HttpPost]
        public IActionResult Create(PostOrderCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var oldPostList = _unitOfWork.Posts.Find(x => x.OrderIndex != 0);
                foreach (var i in oldPostList)
                {
                    i.OrderIndex = 0;
                }


                //Change
                Post firstPost = _unitOfWork.Posts.FindById(model.PostId1);
                firstPost.OrderIndex = 1;

                Post secondPost = _unitOfWork.Posts.FindById(model.PostId2);
                secondPost.OrderIndex = 2;

                Post thirdPost = _unitOfWork.Posts.FindById(model.PostId3);
                thirdPost.OrderIndex = 3;

                Post fourthPost = _unitOfWork.Posts.FindById(model.PostId4);
                fourthPost.OrderIndex = 4;

                Post fifthPost = _unitOfWork.Posts.FindById(model.PostId5);
                fifthPost.OrderIndex = 5;

                _unitOfWork.Posts.UpdateRange(oldPostList);
                _unitOfWork.Posts.Update(firstPost);
                _unitOfWork.Posts.Update(secondPost);
                _unitOfWork.Posts.Update(thirdPost);
                _unitOfWork.Posts.Update(fourthPost);
                _unitOfWork.Posts.Update(fifthPost);
                _unitOfWork.Complete();


                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
