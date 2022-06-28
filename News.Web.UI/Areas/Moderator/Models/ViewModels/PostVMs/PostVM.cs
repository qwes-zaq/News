using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Moderator.Models.ViewModels.PostVMs
{
    public class PostVM
    {
        public int PostId { get; set; }
        public string CategoryName { get; set; }
        public string PostTitle { get; set; }
        public string LanguageCode { get; set; }
        public bool HaveAccess { get; set; } = false;
        public bool CanEdit { get; set; } = true;
        public bool IsDenied { get; set; } = false;
    }
}
