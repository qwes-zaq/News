using News.Web.UI.Areas.Admin.Models.ViewModels.PostVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Models.ViewModels.PinnedPostVMs
{
    public class PostListVM
    {
        public List<PostVM> PinnedPostList { get; set; }
        public List<PostVM> PostList { get; set; }

    }
}
