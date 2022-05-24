using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Domain.Models
{
    public class Language
    {
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Name { get; set; }
        public string LanguageCode { get; set; }
        public string Flag { get; set; }
        public string AddedById { get; set; }
        public int Status { get; set; } = (int)Enums.Status.Active;

        public User AddedBy { get; set; }
        public string UpdatedById { get; set; }
        public User UpdatedUser { get; set; }

        public virtual ICollection<PostTranslation> PostTranslations { get; set; }
        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
