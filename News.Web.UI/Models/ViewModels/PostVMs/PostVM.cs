using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Models.ViewModels.PostVMs
{
    public class PostVM
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        //public string PostBody { get; set; }
        public string PhotoPath { get; set; } 
    }
}
