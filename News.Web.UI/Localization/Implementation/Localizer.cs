using News.Core.Domain.Models;
using News.Core.UnitOfWork.Core;
using News.Web.UI.Localization.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Localization.Implementation
{
    public class Localizer : ILocalizer
    {
        private IUnitOfWork _unitOfWork;
        public Localizer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CategoryTranslation CurrentCategoryTranslation(int id) => _unitOfWork
                .CategoryTranslations
                .FindById(id, _unitOfWork.Languages.FindByCodeOrDefault(CultureInfo.CurrentCulture.Name.Substring(0, 2)).Id);

        public PostTranslation CurrentPostTranslation(int id) => _unitOfWork
                .PostTranslations
                .FindById(id, _unitOfWork.Languages.FindByCodeOrDefault(CultureInfo.CurrentCulture.Name.Substring(0, 2)).Id);
    }
}
