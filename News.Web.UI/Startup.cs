using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using News.Core.Domain.Data;
using News.Core.Domain.Models;
using News.Core.Repository.Implementation.Persistence;
using News.Core.Repository.Implementation.Repositories;
using News.Core.Repository.Persistence;
using News.Core.Repository.Repositories;
using News.Core.UnitOfWork.Core;
using News.Core.UnitOfWork.Persistense;
using News.Web.UI.Localization;
using News.Web.UI.Localization.Implementation;
using News.Web.UI.Localization.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI
{
    public class Startup
    {

       // private readonly IUnitOfWork _unitOfWork;
        public Startup(IConfiguration configuration/*, IUnitOfWork unitOfWork*/)
        {
            Configuration = configuration;
            //_unitOfWork = unitOfWork;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryTranslationRepository, CategoryTranslationRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IPinnedPostRepository, PinnedPostRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostTagRepository, PostTagRepository>();
            services.AddScoped<IPostTranslationRepository, PostTranslationRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddTransient<IStringLocalizer, EFStringLocalizer>();
            services.AddTransient<ILocalizer, Localizer>();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCulture = new List<CultureInfo> {
                new CultureInfo("en"),
                new CultureInfo("ru"),
                new CultureInfo("pp-PP")
                    };

                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCulture;
                options.SupportedUICultures = supportedCulture;
            });

            services.AddDbContext<NewsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<NewsDbContext>();
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");

            //services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews()
                .AddViewLocalization()
                ;
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            

            ////foreach (var i in _unitOfWork.Languages.GetAll())
            //new RequestLocalizationOptions()
            //{
            //    DefaultRequestCulture = new RequestCulture(supportedCulture[1].Name),
            //    SupportedCultures = supportedCulture,
            //    SupportedUICultures = supportedCulture,
            //}

            app.UseRequestLocalization();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{language?}");

                endpoints.MapControllerRoute(
                    name: "Posts",
                    pattern: "{controller}/{action}/{id}/{language?}");


                //Admin
                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}/{language?}");
            });
        }
    }
}
