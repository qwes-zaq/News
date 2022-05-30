using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using News.Core.Domain.Models;
using News.Core.UnitOfWork.Core;
using News.Web.UI.Areas.Admin.Models.ViewModels.PostVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/{controller}/{action}")]
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
        public IActionResult Index()
        {
            List<PostVM> postVMs = new();
            
            foreach (var i in _unitOfWork.Posts.GetAll(x => x.Status == (int)Core.Domain.Enums.Status.Active))
            {
                PostTranslation postTranslation = i.PostTranslations.First();
                Language language = postTranslation.Language;
                CategoryTranslation categoryTranslation = i.Category.CategoryTranslations.First(x => x.LanguageId == language.Id);
                postVMs.Add(new PostVM()
                {
                    PostId = i.Id,
                    CategoryName = categoryTranslation.Title,
                    PostTitle = postTranslation.Title,
                    LanguageCode = language.LanguageCode
                });
            }
            return View(postVMs);
        }
        [HttpGet]
        [Route("/[area]/[controller]/[action]/{language?}")]
        public IActionResult Create(string language)
        {
            var languages = _unitOfWork.Languages.GetAll();
            PostCreateVM postCreateVM = new() { PostTranslationList = new() };

            foreach (var i in languages)
            {
                postCreateVM.PostTranslationList.Add(new PostTranslationVM() { LanguageCode = i.LanguageCode, LanguageId = i.Id });
            }

            Language currentLanguage = _unitOfWork.Languages.Get(x => x.LanguageCode == language) ?? _unitOfWork.Languages.GetAll().First();
            var categories = _unitOfWork.Categories.GetAll();

            postCreateVM.CategoryList = categories
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.CategoryTranslations.FirstOrDefault(x => x.Language == currentLanguage).Title ?? x.CategoryTranslations.FirstOrDefault().Title
                });
            return View(postCreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/[area]/[controller]/[action]/{language?}")]
        public async Task<IActionResult> CreateAsync(PostCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                Post post = new()
                {
                    AddedBy = currentUser,
                    UpdatedUser = currentUser,
                    AddedDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Status = (int)Core.Domain.Enums.Status.Active,
                    CategoryId = model.CategoryId
                };

                //Translation adding
                foreach (var i in model.PostTranslationList)
                {
                    if (i.Title != null)
                    {
                        post.PostTranslations.Add(new()
                        {
                            Post = post,
                            AddedBy = currentUser,
                            UpdatedUser = currentUser,
                            AddedDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            LanguageId = i.LanguageId,
                            Title = i.Title,
                            ShortDescription = i.ShortDescription,
                            Body = i.Body
                        });
                    }
                }

                //Tags adding
                List<string> tagList = model.Tags.ToLower().Split(", ").ToList();
                
                for (int i = 0; i < tagList.Count; i++)
                {
                    
                    if (!_unitOfWork.Tags.GetAny(x => x.Title == tagList[i]))
                    {
                        _unitOfWork.Tags.Add(new() { 
                            AddedBy=currentUser,
                            UpdatedUser=currentUser,
                            AddedDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            Title= tagList[i],
                            Status = (int)Core.Domain.Enums.Status.Active
                        });

                    }
                }
                _unitOfWork.Complete();

                foreach (var i in tagList)
                {
                    Tag currentTag = _unitOfWork.Tags.Get(x=>x.Title==i);

                    _unitOfWork.PostTags.Add(new() { 
                        Post=post,
                        Tag=currentTag
                    });
                }

                //using (var transaction = _unitOfWork.Context.Database.BeginTransaction()) //?
                //{
                //try
                //{


                _unitOfWork.Posts.Add(post);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();
                //}
                //catch (Exception ex)
                //{
                //    transaction.Rollback();
                //}


                return RedirectToAction(nameof(Index));
            }

            var categories = _unitOfWork.Categories.GetAll();
            model.CategoryList = categories
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.CategoryTranslations.FirstOrDefault().Title
                });
            return View(model);
        }


        [HttpGet]
        [Route("/[area]/[controller]/[action]/{id}/{language?}")]
        public IActionResult Edit(int id, string language)
        {
            Post post = _unitOfWork.Posts.FindById(id);

            if (post == null || post.Status != (int)Core.Domain.Enums.Status.Active)
            {
                ViewData["Info"] = "There is not a such post"; //?
                return RedirectToAction(nameof(Index));
            }

            Category category = _unitOfWork.Categories.Get(x => x.Id == post.CategoryId);
            var languages = _unitOfWork.Languages.GetAll();
            CategoryTranslation categoryTranslation = category.CategoryTranslations.FirstOrDefault(x => x.Language.LanguageCode.ToLower() == language?.ToLower()) ?? category.CategoryTranslations.FirstOrDefault();


            PostEditVM postEditVM = new()
            {
                PostId = post.Id,
                LangInUrl = language,
                CategoryId = post.CategoryId,
                PostTranslationList = new(),
                SelectedCategory = categoryTranslation.Title
            };

            //Translations
            foreach (var i in languages)
            {
                var tmp = post.PostTranslations.FirstOrDefault(x => x.LanguageId == i.Id);
                postEditVM.PostTranslationList.Add(new()
                {
                    Title = tmp?.Title,
                    LanguageId = i.Id,
                    ShortDescription = tmp?.ShortDescription,
                    LanguageCode = i.LanguageCode,
                    Body = tmp?.Body
                });
            }


            //Tags
            foreach (var i in post.PostTags)
            {
                postEditVM.Tags += i.Tag.Title + ", ";
            }

            if (postEditVM.Tags != null)
            {
                postEditVM.Tags = postEditVM.Tags[0..^2]; // Deleting last ", "
            }

            postEditVM.CategoryList = _unitOfWork.Categories.GetAll()
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.CategoryTranslations.FirstOrDefault(x => x.Language.LanguageCode == language).Title ?? x.CategoryTranslations.FirstOrDefault().Title
                });
            return View(postEditVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/[area]/[controller]/[action]/{id}/{language?}")]
        public async Task<IActionResult> EditAsync(PostEditVM model)
        {
            if (ModelState.IsValid)
            {
                Post post = _unitOfWork.Posts.FindById(model.PostId);
                //Language language = _unitOfWork.Languages.GetAll().FirstOrDefault(x => x.LanguageCode.ToLower() == model.LangInUrl.ToLower()); 
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                post.Category = _unitOfWork.Categories.FindById(model.CategoryId);

                //Translation edit
                foreach (var i in model.PostTranslationList)
                {
                    PostTranslation tmp = post.PostTranslations.FirstOrDefault(x => x.LanguageId == i.LanguageId);
                    if (tmp == null)
                    {
                        tmp = new()
                        {
                            Post = post,
                            AddedBy = currentUser,
                            AddedDate = DateTime.Now,
                            LanguageId = i.LanguageId,
                            Title = i.Title,
                            ShortDescription = i.ShortDescription,
                            Body = i.Body
                        };

                    }
                    tmp.UpdatedUser = currentUser;
                    tmp.UpdateDate = DateTime.Now;
                    tmp.Title = i.Title;
                    tmp.ShortDescription = i.ShortDescription;
                    tmp.Body = i.Body;
                    post.PostTranslations.Add(tmp);
                }

                //Tags edit
                List<string> oldTags = new();
                                       //post.PostTags.Select(x => x.Tag.Title).ToList();  
                List<string> newTags = model.Tags.ToLower().Split(", ").ToList();
                List<string> tmpTags = new();


                foreach (var i in post.PostTags)
                {
                    oldTags.Add(i.Tag.Title);
                }

                //Tag1, Tag2, Tag3   --- oldTags  
                //Tag1, Tag2, Tag4   --- newTags
                //Tag1, Tag2         --- tmpTags

                foreach (var i in oldTags)
                {
                    if (newTags.Contains(i))
                    {
                        tmpTags.Add(i);
                    }
                }

                foreach (var i in tmpTags)
                { 
                    newTags.Remove(i);
                    oldTags.Remove(i);
                }


                //Deleting old tags
                foreach (var i in oldTags)
                {
                    var tmpTag = _unitOfWork.Tags.Get(x => x.Title == i);
                    _unitOfWork.PostTags.Remove(_unitOfWork.PostTags.FindById (post.Id,tmpTag.Id));
                }

                //Adding new tags
                for (int i = 0; i < newTags.Count; i++)
                {

                    if (!_unitOfWork.Tags.GetAny(x => x.Title == newTags[i]))
                    {
                        _unitOfWork.Tags.Add(new()
                        {
                            AddedBy = currentUser,
                            UpdatedUser = currentUser,
                            AddedDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            Title = newTags[i],
                            Status = (int)Core.Domain.Enums.Status.Active
                        });

                    }
                }
                _unitOfWork.Complete();

                foreach (var i in newTags)
                {
                    Tag currentTag = _unitOfWork.Tags.Get(x => x.Title == i);

                    _unitOfWork.PostTags.Add(new()
                    {
                        Post = post,
                        Tag = currentTag
                    });
                }
                //using (var transaction = _unitOfWork.Context.Database.BeginTransaction()) //?
                //{
                //    try
                //    {
                _unitOfWork.Posts.Update(post);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return RedirectToAction(nameof(Index));
                //    }
                //    catch (Exception ex)
                //    {
                //        transaction.Rollback();
                //    }
                //}
            }


            model.CategoryList = _unitOfWork.Categories.GetAll()
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.CategoryTranslations.FirstOrDefault(x => x.Language.LanguageCode == model.LangInUrl).Title ?? x.CategoryTranslations.FirstOrDefault().Title
                });
            return View(model);
        }

        [HttpGet]
        [Route("/[area]/[controller]/[action]/{id}/{language?}")]
        public IActionResult Delete(int id, string language)
        {
            Post post = _unitOfWork.Posts.FindById(id);

            if (post == null || post.Status != (int)Core.Domain.Enums.Status.Active)
            {
                ViewData["Info"] = "There is not a such post"; //?
                return RedirectToAction(nameof(Index));
            }

            Category category = _unitOfWork.Categories.Get(x => x.Id == post.CategoryId);
            var languages = _unitOfWork.Languages.GetAll();
            CategoryTranslation categoryTranslation = category.CategoryTranslations.FirstOrDefault(x => x.Language.LanguageCode.ToLower() == language?.ToLower()) ?? category.CategoryTranslations.FirstOrDefault();


            PostInfoVM postInfoVM = new()
            {
                PostId = post.Id,
                PostTranslationList = new(),
                PostCategory = categoryTranslation.Title
            };

            foreach (var i in languages)
            {
                var tmp = post.PostTranslations.FirstOrDefault(x => x.LanguageId == i.Id);
                postInfoVM.PostTranslationList.Add(new()
                {
                    Title = tmp?.Title,
                    LanguageId = i.Id,
                    ShortDescription = tmp?.ShortDescription,
                    LanguageCode = i.LanguageCode,
                    Body = tmp?.Body
                });
            }

            return View(postInfoVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/[area]/[controller]/[action]/{id}/{language?}")]
        public async Task<IActionResult> Delete(int id)
        {

            Post post = _unitOfWork.Posts.FindById(id);
            ////User
            post.Status = (int)Core.Domain.Enums.Status.Blocked;
            _unitOfWork.Posts.Update(post);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();

            return RedirectToAction(nameof(Index));
        }
    }
}
