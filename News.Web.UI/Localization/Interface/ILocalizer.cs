using News.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Localization.Interface
{
    public interface ILocalizer
    {
        public PostTranslation CurrentPostTranslation(int id);
        public CategoryTranslation CurrentCategoryTranslation(int id);
        //public string PostTitle(int id);
        //public string PostBody(int id);
        //public string CategoryTitle(int id);
    }
}
