using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Models.ViewModels.PostVMs
{
    public class PostEditVM
    {
        public int PostId { get; set; }
        public string LangInUrl { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public int CategoryId { get; set; }
        public string SelectedCategory { get; set; }
        public List<PostTranslationVM> PostTranslationList { get; set; }
    }
}
