using News.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Models.ViewModels.CategoryVMs
{
    public class CategoryEditListVM
    {
        public int CategoryId { get; set; }
        public int? OrderIndex { get; set; }
        public List<CategoryEditVM> CategoryTranslationList { get; set; }

    }
    public class CategoryEditVM
    {
        public int LanguageId { get; set; }
        public string LanguageCode { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }


}
