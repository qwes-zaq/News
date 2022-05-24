using System;
using System.Collections.Generic;

namespace News.Core.Domain.Models
{
    public class Tag
    {
        public Tag()
        {
            PostTags = new HashSet<PostTag>();
        }
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Title { get; set; }
        public int Status { get; set; } = (int)Enums.Status.Active;

        public string AddedById { get; set; }
        public User AddedBy { get; set; }
        public string UpdatedById { get; set; }
        public User UpdatedUser { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
