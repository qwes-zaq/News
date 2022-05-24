using System;
using System.Collections.Generic;

namespace News.Core.Domain.Models
{
    public class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
            CategoryTranslations = new HashSet<CategoryTranslation>();
        }
        public int Id { get; set; }
        public int OrderIndex { get; set; }
        public string CoverPhotoPath { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        //public int Status { get; set; } = Status.Active;
        public int Status { get; set; } = (int)Enums.Status.Active;


        public string AddedById { get; set; }
        public User AddedBy { get; set; }
        public string UpdatedById { get; set; }
        public User UpdatedUser { get; set; }

        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
