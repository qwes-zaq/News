using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Models.ViewModels.PostOrderVMs
{
    public class PostOrderCreateVM
    {
        [Display(Name = "First Post")]
        public List<SelectListItem> PostList1 { get; set; }
        public int PostId1 { get; set; }

        [Display(Name = "Second Post")]
        public List<SelectListItem> PostList2 { get; set; }
        public int PostId2 { get; set; }

        [Display(Name ="Third Post")]
        public List<SelectListItem> PostList3 { get; set; }
        public int PostId3 { get; set; }

        [Display(Name = "Fourth Post")]
        public List<SelectListItem> PostList4 { get; set; }
        public int PostId4 { get; set; }

        [Display(Name = "Fifth Post")]
        public List<SelectListItem> PostList5 { get; set; }
        public int PostId5 { get; set; }
    }
}
