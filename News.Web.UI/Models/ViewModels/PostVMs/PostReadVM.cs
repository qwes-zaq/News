using News.Web.UI.Models.ViewModels.TagVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Models.ViewModels.PostVMs
{
    public class PostReadVM
    {
        public int PostId { get; set; }
        public int ViewCount { get; set; }
        public string PostTitle { get; set; }
        public string PostBody { get; set; }
        public string PhotoPath { get; set; }
        public string AuthorName { get; set; }
        public DateTime Date { get; set; }

        public List<TagVM> TagList { get; set; }
        public List<PostVM> PostList { get; set; }
    }
}
