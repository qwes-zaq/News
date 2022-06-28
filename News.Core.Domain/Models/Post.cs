using System;
using System.Collections.Generic;

namespace News.Core.Domain.Models
{
    public class Post
    {
        public Post()
        {
            PostTranslations = new HashSet<PostTranslation>();
            PostTags = new HashSet<PostTag>();
        }
        public int Id { get; set; }
        public int OrderIndex { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PhotoPath { get; set; }
        public string CoverPhoto { get; set; }
        public int Status { get; set; } = (int)Enums.Status.Active;

        public string AddedById { get; set; }
        public User AddedBy { get; set; }
        public string UpdatedById { get; set; }
        public User UpdatedUser { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public PinnedPost PinnedPost { get; set; }

        public virtual ICollection<PostTag> PostTags{ get; set; }
        public virtual ICollection<PostTranslation> PostTranslations { get; set; }


}
}
