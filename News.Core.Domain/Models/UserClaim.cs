﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Domain.Models
{
    public class UserClaim : IdentityUserClaim<string>
    {
        public User User { get; set; }
    }
}
