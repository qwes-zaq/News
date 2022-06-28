using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Models.ViewModels.PostOrderVMs
{
    public class PostOrderVM
    {
        public int PostId { get; set; }
        public int PostOrderIndex { get; set; }
        public string CategoryName { get; set; }
        public string PostTitle { get; set; }
        public string LanguageCode { get; set; }

    }
}
