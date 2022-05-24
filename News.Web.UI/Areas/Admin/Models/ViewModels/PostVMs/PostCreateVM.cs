using Microsoft.AspNetCore.Mvc.Rendering;
using News.Web.UI.Areas.Admin.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Models.ViewModels.PostVMs
{
    public class PostCreateVM
    {
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public int CategoryId { get; set; }

        [PostTranslationList]
        public List<PostTranslationVM> PostTranslationList { get; set; }
    }


}
