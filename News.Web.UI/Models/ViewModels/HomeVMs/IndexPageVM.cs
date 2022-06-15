using News.Web.UI.Models.ViewModels.PostVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Models.ViewModels.HomeVMs
{
    public class IndexPageVM
    {
        public List<SliderItem> PinnedPostSlider { get; set; }
        public List<CategorySlider> Sliders { get; set; }
        public List<PostVM> MostViewedPosts { get; set; }
        public List<PostVM> LatestPosts { get; set; }
    }
}
