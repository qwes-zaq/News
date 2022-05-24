using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Models.ViewModels.PostVMs
{
    public class PostVM
    {
        public int PostId { get; set; }
        public string CategoryName { get; set; }
        public string PostTitle { get; set; }
        public string LanguageCode { get; set; }

    }
}
