using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace News.Core.Domain.Models
{
    public class User : IdentityUser //<int>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ProfilePhoto { get; set; }
        public string FacebookAccount { get; set; }
        public string YoutubeAccount { get; set; }
        public int Status { get; set; } = (int)Enums.Status.Active;

        //public virtual ICollection<IdentityUserRole<string>> Roles { get; set; } = new List<IdentityUserRole<string>>();
        //public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; } = new List<IdentityUserLogin<string>>();
        //public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } = new List<IdentityUserClaim<string>>();


        public virtual ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
        public virtual ICollection<UserClaim> Claims { get; set; } = new List<UserClaim>();
        public virtual ICollection<UserLogin> Logins { get; set; } = new List<UserLogin>();

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PostTranslation> PostTranslations { get; set; }

        //new

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Language> Languages { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }

        public virtual ICollection<Post> UpdatedPosts { get; set; }
        public virtual ICollection<PostTranslation> UpdatedPostTranslations { get; set; }
        public virtual ICollection<Tag> UpdatedTags { get; set; }
        public virtual ICollection<Language> UpdatedLanguages { get; set; }
        public virtual ICollection<Category> UpdatedCategories { get; set; }
        public virtual ICollection<CategoryTranslation> UpdatedCategoryTranslations { get; set; }

    }
}
