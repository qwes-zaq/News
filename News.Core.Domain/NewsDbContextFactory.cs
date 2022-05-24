using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using News.Core.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Domain
{
    public class NewsDbContextFactory : IDesignTimeDbContextFactory<NewsDbContext> 
    {
        public NewsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<NewsDbContext>();
            builder.UseSqlServer(
                "Server = QWERTY; Database = NewsSite15db; Trusted_Connection = True; MultipleActiveResultSets = true"

                );
            return new NewsDbContext(builder.Options);
        }
    }
}