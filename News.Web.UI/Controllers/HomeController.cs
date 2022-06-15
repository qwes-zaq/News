using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using News.Core.Domain.Models;
using News.Core.UnitOfWork.Core;
using News.Web.UI.Models;
using News.Web.UI.Models.ViewModels.HomeVMs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _environment;
        private readonly UserManager<User> _userManager;

        public HomeController(
            ILogger<HomeController> logger, 
            IUnitOfWork unitOfWork, 
            IHostingEnvironment environment, 
            UserManager<User> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _environment = environment;
            _userManager = userManager;
        }


        [Route("/{language?}")]
        [Route("/[controller]/[action]/{language?}")]
        public IActionResult Index(string language)
        {
            IndexPageVM model = new() {
                Sliders = new(),
                PinnedPostSlider = new(),
                MostViewedPosts = new(),
                LatestPosts = new()
            };
            var tmp = _unitOfWork.Languages.GetAll();

            Language currentLanguage = language != null ?
                tmp.FirstOrDefault(x => x.LanguageCode.ToLower() == language.ToLower()) ?? tmp.First()
                : tmp.First();

            //Category sliders
            var allCategories = _unitOfWork.Categories.GetFullAllCategories();

            foreach (var i in allCategories)
            {
                CategorySlider currentCategorySlider = new() {
                    Items = new(),
                    CategoryName = i.CategoryTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id)?.Title
                };
                if (currentCategorySlider.CategoryName != null)
                {
                    foreach (var j in i.Posts)
                    {
                        var currentPostTranslation = j.PostTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id);
                        if(currentPostTranslation!=null)
                        {
                            SliderItem sliderItem = new(){ 
                                PostId=j.Id,
                                PostPhotoPath= j.PhotoPath ?? "/ui/img/news-450x350-2.jpg",
                                PostTitle=currentPostTranslation.Title
                            };
                            currentCategorySlider.Items.Add(sliderItem);
                        }

                    }

                    if (currentCategorySlider.Items.Count > 1)
                    {
                        model.Sliders.Add(currentCategorySlider);
                    }
                
                }
            
            }

            //Pinned post slider
            var pinnedPosts = _unitOfWork.PinnedPosts.GetAllPostsWithTranslations();
            foreach(var i in pinnedPosts)
            {
                var postTitle = i.PostTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id).Title;
                var categoryTitle = i.Category.CategoryTranslations.FirstOrDefault(x => x.LanguageId == currentLanguage.Id).Title;

                if (postTitle != null && categoryTitle != null)
                    model.PinnedPostSlider.Add(new()
                    {
                        PostId = i.Id,
                        PostTitle = postTitle,
                        PostPhotoPath= i.PhotoPath ?? "/ui/img/news-450x350-2.jpg"
                    });

            }

            // Most Viewed Posts
            var postTranslations = _unitOfWork
                .PostTranslations
                .GetPostTranlations(x => x.LanguageId == currentLanguage.Id && x.Title!=null);

            foreach (var i in postTranslations.OrderBy(x => x.ViewCount).Reverse())
            {
                if (i.Post.Status == (int)Core.Domain.Enums.Status.Active)
                {
                    model.MostViewedPosts.Add(new()
                    {
                        PhotoPath = i.Post.PhotoPath ?? "/ui/img/news-450x350-2.jpg",
                        PostId = i.PostId,
                        PostTitle = i.Title
                    });
                }

                if (model.MostViewedPosts.Count != 6)
                {
                    continue;
                }
                break;
            }

            //Latest posts
            foreach (var i in postTranslations.OrderBy(x => x.UpdateDate).Reverse())
            {
                if (i.Post.Status == (int)Core.Domain.Enums.Status.Active)
                {
                    model.LatestPosts.Add(new()
                    {
                        PhotoPath = i.Post.PhotoPath ?? "/ui/img/news-450x350-2.jpg",
                        PostId = i.PostId,
                        PostTitle = i.Title
                    });
                }

                if (model.LatestPosts.Count == 6)
                {
                    break;
                }
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
