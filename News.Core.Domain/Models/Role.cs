using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace News.Core.Domain.Models
{
    public class Role : IdentityRole
    {
        public virtual ICollection<UserRole> Users { get; set; }
        public string Description { get; set; }
    }
}
