using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using News.Core.Domain.Data;

namespace News.Core.Domain
{
    public class Startup
    {
        public static class ServiceCollectionExtensions
        {
            public static void AddEntityFramework(IServiceCollection services, string connectionString)
            {
                services.AddDbContext<NewsDbContext>(options =>
                        options.UseSqlServer(connectionString));
            }
        }
    }
}