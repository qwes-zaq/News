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

namespace News.Web.UI.Controllers
{
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _environment;
        private readonly UserManager<User> _userManager;


        public PostController(IUnitOfWork unitOfWork, IHostingEnvironment environment, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _userManager = userManager;
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
                return RedirectToAction("Index", "Home", new { @language = language });
            }

            foreach (var i in categoryWithPosts.Posts)
            {
                if (i.Status == (int)Core.Domain.Enums.Status.Active)
                {
                    postList.Add(new()
                    {
                        PostId = i.Id,
                        PostTitle = i.PostTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id).Title,
                        //PostBody= i.PostTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id).Body,
                        PhotoPath = "/ui/img/news-450x350-2.jpg"

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
                return RedirectToAction("Index", "Home", new { @language = language });
            }


            var tmp = _unitOfWork.Languages.GetAll();

            Language currentLanguage = language != null ?
                tmp.FirstOrDefault(x => x.LanguageCode.ToLower() == language.ToLower()) ?? tmp.First()
                : tmp.First();

            Category currentCategory = _unitOfWork.Categories.FindById(currentPost.CategoryId);
            PostTranslation currentPostTranslation = currentPost
               .PostTranslations
               .FirstOrDefault(x => x.LanguageId == currentLanguage.Id);

            if (currentPostTranslation == null)
            {
                return RedirectToAction("Index", "Home", new { @language = language });
            }

            PostReadVM postReadVM = new()
            {
                PostBody = currentPostTranslation.Body,
                PostTitle = currentPostTranslation.Title,
                AuthorName = currentPost.UpdatedUser.UserName,
                Date = currentPost.AddedDate,
                PhotoPath = "/ui/img/news-450x350-2.jpg",
                PostList = new()
            };

            var list = currentCategory.Posts                
                .Where(x=>x.Status == (int)Core.Domain.Enums.Status.Active)
                .OrderBy(x => x.AddedDate)
                .Reverse()
                .Take(6)
                .ToList()
                ;

            if (!list.Remove(currentPost))
            {
                list.Remove(list.First());    
            }
            foreach(var i in list)
            {
                    postReadVM.PostList.Add(new()
                    {
                        PostId = i.Id,
                        PostTitle = i.PostTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id).Title,
                        PhotoPath = "/ui/img/news-450x350-2.jpg"

                    });
            }

            return View(postReadVM);
        }
    }
}
