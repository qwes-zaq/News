using System;

namespace News.Core.Domain.Models
{
    public class PostTranslation
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }


        public string AddedById { get; set; }
        public User AddedBy { get; set; }
        public string UpdatedById { get; set; }
        public User UpdatedUser { get; set; }


        public DateTime AddedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public string Slug { get; set; }
        public string Title { get; set; }
        public string TitleType { get; set; }
        public string ShortDescription { get; set; }
        public string Body { get; set; }
        public int ViewCount { get; set; }
        public int ReadTime { get; set; }
    }
}
