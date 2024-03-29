﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using News.Core.Domain.Models;
using News.Core.UnitOfWork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using News.Web.UI.Models.ViewModels.PostVMs;
using System.Globalization;
using News.Web.UI.Localization.Interface;

namespace News.Web.UI.Controllers
{
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _environment;
        private readonly UserManager<User> _userManager;
        private readonly ILocalizer _localizer;


        public PostController(IUnitOfWork unitOfWork, IHostingEnvironment environment, UserManager<User> userManager, ILocalizer localizer)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _userManager = userManager;
            _localizer = localizer;
        }


        [Route("/[controller]/[action]/{id}/{language?}")]
        public IActionResult Index(int id, string language)
        {
            int categoryId = id;

            List<PostVM> postList = new();
            var tmp = _unitOfWork.Languages.GetAll();

            Language currentLanguage = language != null ?
                tmp.FirstOrDefault(x => x.LanguageCode.ToLower() == language.ToLower()) ?? tmp.First()
                : tmp.First();

            var categoryWithPosts = _unitOfWork.Categories.FindWithPosts(x => x.Id == categoryId).First();

            if (categoryWithPosts == null || categoryWithPosts.Status != (int)Core.Domain.Enums.Status.Active)
            {
                return RedirectToAction("Index", "Home", new { language });
            }

            foreach (var i in categoryWithPosts.Posts)
            {
                if (i.Status == (int)Core.Domain.Enums.Status.Active)
                {
                    postList.Add(new()
                    {
                        PostId = i.Id,
                        PostTitle = i.PostTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id).Title,
                        PhotoPath = i.PhotoPath ?? "/ui/img/news-450x350-2.jpg"
                    });
                }
            }

            return View(postList);
        }


        [Route("/[controller]/[action]/{id}/{language?}")]
        public IActionResult Read(int id, string language)
        {
            int postId = id;
            Post currentPost = _unitOfWork.Posts.FindById(postId);

            if (currentPost == null || currentPost.Status != (int)Core.Domain.Enums.Status.Active)
            {
                return RedirectToAction("Index", "Home", new { language });
            }

            Category currentCategory = _unitOfWork.Categories.FindById(currentPost.CategoryId);

            PostTranslation currentPostTranslation = _localizer.CurrentPostTranslation(currentPost.Id);

            if (currentPostTranslation == null)
            {
                return RedirectToAction("Index", "Home", new { language });
            }

            PostReadVM postReadVM = new()
            {
                PostBody = currentPostTranslation.Body,
                PostTitle = currentPostTranslation.Title,
                AuthorName = currentPost.UpdatedUser.UserName,
                Date = currentPost.AddedDate,
                PhotoPath = currentPost.PhotoPath ?? "/ui/img/news-450x350-2.jpg",
                ViewCount=currentPostTranslation.ViewCount,
                PostList = new(),
                TagList = new()
            };


            foreach (var i in currentPost.PostTags)
            {
                postReadVM.TagList.Add(new()
                {
                    TagTitle = ToTitleCase(i.Tag.Title),
                    TagId = i.TagId
                });
            }
            var list = currentCategory.Posts
                .Where(x => x.Status == (int)Core.Domain.Enums.Status.Active)
                .OrderBy(x => x.AddedDate)
                .Reverse()
                .ToList();

            if (!list.Remove(currentPost))
            {
                list.Remove(currentPost);
            }

            foreach (var i in list)
            {
                var postTranslation = _localizer.CurrentPostTranslation(i.Id);
                if (postTranslation != null && postTranslation.Title != null)
                {
                    postReadVM.PostList.Add(new()
                    {
                        PostId = i.Id,
                        PostTitle = postTranslation.Title,
                        PhotoPath = i.PhotoPath ?? "/ui/img/news-450x350-2.jpg",

                    });
                }
                if (postReadVM.PostList.Count > 4)
                {
                    break;
                }
            }

            currentPostTranslation.ViewCount++;
            _unitOfWork.PostTranslations.Update(currentPostTranslation);
            _unitOfWork.Complete();

            return View(postReadVM);
        }

        public static string ToTitleCase(string title)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());
        }
    }
}
