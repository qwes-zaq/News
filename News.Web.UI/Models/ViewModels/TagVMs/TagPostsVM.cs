using News.Web.UI.Models.ViewModels.PostVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Models.ViewModels.TagVMs
{
    public class TagPostsVM
    {
        public string TagTitle { get; set; }
        public List<PostVM> PostList { get; set; }
    }
}
