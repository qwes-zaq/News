using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using News.Core.Domain.Models;
using News.Core.UnitOfWork.Core;
using News.Web.UI.Models.ViewModels.TagVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Controllers
{
    public class TagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _environment;
        private readonly UserManager<User> _userManager;

        public TagController(IUnitOfWork unitOfWork, IHostingEnvironment environment, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _userManager = userManager;
        }

        [Route("/[controller]/[action]/{id}/{language?}")]
        public IActionResult PostList(int id, string language)
        {
            Tag currentTag = _unitOfWork.Tags.FindById(id);
            
            if (currentTag == null || currentTag.Status != (int)Core.Domain.Enums.Status.Active)
            {
                return RedirectToAction("Index", "Home", new { @language = language });
            }

            var tmp = _unitOfWork.Languages.GetAll();

            Language currentLanguage = language != null ?
                tmp.FirstOrDefault(x => x.LanguageCode.ToLower() == language.ToLower()) ?? tmp.First()
                : tmp.First();

            
            TagPostsVM tagPostVM = new() {
                PostList=new(),
                TagTitle=currentTag.Title
            };
            tagPostVM.TagTitle = char.ToUpper(tagPostVM.TagTitle[0]) + tagPostVM.TagTitle.Substring(1);
            foreach (var i in currentTag.PostTags)
            {
                var currentPostTranslation = i.Post.PostTranslations.FirstOrDefault(x=>x.LanguageId==currentLanguage.Id);
                if (currentPostTranslation != null)
                {
                    tagPostVM.PostList.Add(new()
                    {
                        PostId = i.PostId,
                        PostTitle = currentPostTranslation.Title,
                        PhotoPath="/ui/img/news-450x350-2.jpg"
                    });
                }
            }

            return View(tagPostVM);
        }
    }
}
