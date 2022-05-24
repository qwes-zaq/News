using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Models.ViewModels.LanguageVMs
{
    public class LanguageVM
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }
    }
    public class LanguagesVM
    {
        public List<LanguageVM> LanguageList { get; set; }
    }
}
