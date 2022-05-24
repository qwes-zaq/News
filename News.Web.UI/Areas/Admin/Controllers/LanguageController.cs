using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using News.Core.Domain.Models;
using News.Core.UnitOfWork.Core;
using News.Web.UI.Areas.Admin.Models.ViewModels.LanguageVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/{controller}/{action}/{id?}")]
    public class LanguageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _environment;
        private readonly UserManager<User> _userManager;


        public LanguageController(IUnitOfWork unitOfWork, IHostingEnvironment environment, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            LanguagesVM tmp = new LanguagesVM()
            {
                LanguageList = _unitOfWork
                                    .Languages
                                    .GetAll()
                                    .Select(a=> new LanguageVM() {LanguageName=a.LanguageCode, Id=a.Id })
                                    .ToList()
            };
            return View(tmp);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {

            return View(new LanguageCreateVm());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(LanguageCreateVm languageCreateVm)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                _unitOfWork.Languages.Add(new Language()
                {
                    LanguageCode = languageCreateVm.LanguageCode,
                    Name = languageCreateVm.LanguageName,
                    AddedBy = currentUser,
                    UpdatedUser = currentUser,
                    AddedDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(languageCreateVm);
        }

    }
}
