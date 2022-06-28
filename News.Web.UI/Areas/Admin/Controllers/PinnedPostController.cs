using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using News.Core.Domain.Models;
using News.Core.UnitOfWork.Core;
using News.Web.UI.Areas.Admin.Models.ViewModels.PinnedPostVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class PinnedPostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _environment;
        private readonly UserManager<User> _userManager;


        public PinnedPostController(IUnitOfWork unitOfWork, IHostingEnvironment environment, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _userManager = userManager;
        }

        [Route("{language?}")]
        public IActionResult Index(string language)
        {
            PostListVM model = new()
            {
                PostList = new(),
                PinnedPostList = new()
            };


            var tmp = _unitOfWork.Languages.GetAll();

            Language currentLanguage = language != null ?
                tmp.FirstOrDefault(x => x.LanguageCode.ToLower() == language.ToLower()) ?? tmp.First()
                : tmp.First();

            var allPinnedPosts = _unitOfWork.PinnedPosts.GetAllPosts().ToList();
            var allPosts = _unitOfWork.Posts.GetAll(x => x.Status == (int)Core.Domain.Enums.Status.Active).ToList().Except(allPinnedPosts);

            foreach (var i in allPinnedPosts)
            {
                var postTitle = i.PostTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id).Title;
                var categoryTitle = i.Category.CategoryTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id).Title;

                if (postTitle != null && categoryTitle != null)
                    model.PinnedPostList.Add(new()
                    {
                        PostId = i.Id,
                        PostTitle = postTitle,
                        CategoryName = categoryTitle
                    });
            }

            foreach (var i in allPosts)
            {
                var postTitle = i.PostTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id)?.Title;
                var categoryTitle = i.Category.CategoryTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id)?.Title;

                if (postTitle != null && categoryTitle != null)
                    model.PostList.Add(new()
                    {
                        PostId = i.Id,
                        PostTitle = postTitle,
                        CategoryName = categoryTitle
                    });
            }

            return View(model);
        }


        [HttpPost]
        [Route("{postId?}")]
        public IActionResult Pin(int postId)
        {

            PinnedPost currentPinnedPost = new()
            {
                PostId = postId,
                AddedDate = DateTime.Today,
                UpdateDate = DateTime.Today
            };
            _unitOfWork.PinnedPosts.Add(currentPinnedPost);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("{postId?}")]
        public IActionResult UnPin(int postId)
        {
            PinnedPost currentPinnedPost = _unitOfWork.PinnedPosts.Get(x => x.PostId == postId);
            _unitOfWork.PinnedPosts.Remove(currentPinnedPost);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return RedirectToAction(nameof(Index));
        }
    }
}
