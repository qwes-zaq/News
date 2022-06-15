using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Models.ViewModels.HomeVMs
{
    public class CategorySlider
    {
        public string CategoryName { get; set; }
        public List<SliderItem> Items { get; set; }
    }
}
