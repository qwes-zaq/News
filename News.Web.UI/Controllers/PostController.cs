using Microsoft.AspNetCore.Mvc;
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


        [Route("/[controller]/[action]/{categoryId}/{lang?}")]
        public IActionResult Index(int categoryId, string lang)
        {
            List<PostVM> postList = new();
            var tmp = _unitOfWork.Languages.GetAll();

            Language currentLanguage = lang != null ? 
                tmp.FirstOrDefault(x => x.LanguageCode.ToLower() == lang.ToLower()) ?? tmp.First()
                : tmp.First();

            var categoryWithPosts = _unitOfWork.Categories.FindWithPosts(x=>x.Id==categoryId ).First();
            
            if (categoryWithPosts == null || categoryWithPosts.Status != (int)Core.Domain.Enums.Status.Active)
            {
                return RedirectToAction("Index", "Home", new {@language=lang  });
            }

            foreach (var i in categoryWithPosts.Posts)
            {
                if (i.Status == (int)Core.Domain.Enums.Status.Active)
                {
                    postList.Add(new() {
                        PostId=i.Id,
                        PostTitle=i.PostTranslations.FirstOrDefault(x=>x.LanguageId==currentLanguage.Id).Title,
                        PhotoPath = "/ui/img/news-450x350-2.jpg"

                    });
                
                }
            }

            return View(postList);
        }
    }
}
