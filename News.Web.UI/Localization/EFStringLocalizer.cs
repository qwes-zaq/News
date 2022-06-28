using Microsoft.Extensions.Localization;
using News.Core.UnitOfWork.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Localization
{
    public class EFStringLocalizer : IStringLocalizer
    {
        private readonly IUnitOfWork _unitOfWork;

        public EFStringLocalizer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public LocalizedString this[string name]
        {
            get
            {
                throw new Exception();
            }
            
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                string title=null;
                int id = (int)arguments[0];

                switch (name)
                {
                    case "Post":
                        var z = _unitOfWork.Languages.FindByCodeOrDefault(CultureInfo.CurrentCulture.Name.Substring(0, 2));
                        title = _unitOfWork.PostTranslations.FindById(id, z.Id).Title ?? "";
                        if (arguments.Length > 1 && (string)arguments[1] == "PostBody")
                        {
                            string body = _unitOfWork.PostTranslations.FindById(id, _unitOfWork.Languages.FindByCodeOrDefault(CultureInfo.CurrentCulture.Name.Substring(0, 2)).Id).Body ?? "";
                            return new LocalizedString(name, body, title == null); // _localizer["Post", id, "PostBody"]
                        }
                        break;

                    case "Category":
                        title = _unitOfWork.CategoryTranslations.FindById(id, _unitOfWork.Languages.FindByCodeOrDefault(CultureInfo.CurrentCulture.Name.Substring(0, 2)).Id).Title ?? "";

                        break;
                }

                if (title == null)
                {
                    throw new Exception();
                }
                return new LocalizedString(name, title, resourceNotFound: title ==null); // _localizer["Post/Category", id]
            }
        }
        public string GetCurrentLanguage()
        {
            return CultureInfo.CurrentCulture.Name;
        }
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }
    }
}