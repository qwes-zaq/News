using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Models.ViewModels.LanguageVMs
{
    public class LanguageCreateVm
    {
        [Required]
        [Display(Name = "Name")]
        public string LanguageName { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string LanguageCode { get; set; }
    }
}
