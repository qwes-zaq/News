using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Moderator.Models.ViewModels.PostVMs
{
    public class PostInfoVM
    {
        public int PostId { get; set; }
        public string PostCategory { get; set; }
        public List<PostTranslationVM> PostTranslationList { get; set; }
    }
}
