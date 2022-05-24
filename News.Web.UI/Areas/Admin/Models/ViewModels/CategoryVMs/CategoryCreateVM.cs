using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Models.ViewModels.CategoryVMs
{
    
    public class CategoryCreateVM
    {

        [Required]
        public int LanguageId { get; set; }
        public IEnumerable<SelectListItem> LanguagesList { get; set; }

        [Required]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
    
    public class CategoryCreateVM_v1
    {
        public int LanguageId { get; set; }
        public string LanguagesName { get; set; }
        public string CategoryName { get; set; }
    }

}
