using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Models.ViewModels.PostVMs
{
    public class PostTranslationVM
    {
        public int LanguageId { get; set; }
        public string LanguageCode { get; set; }

        //public string Slug { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        //public string TitleType { get; set; }
        [Display(Name = "ShortDescription")]
        public string ShortDescription { get; set; }
        [Display(Name = "Body")]
        public string Body { get; set; }
    }
}
