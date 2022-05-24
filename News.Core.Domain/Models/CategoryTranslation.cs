
using System;

namespace News.Core.Domain.Models
{
    public class CategoryTranslation
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public string AddedById { get; set; }
        public User AddedBy { get; set; }
        public string UpdatedById { get; set; }
        public User UpdatedUser { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
