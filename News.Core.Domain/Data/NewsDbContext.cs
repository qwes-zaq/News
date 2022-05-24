using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using News.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Domain.Data
{
    public class NewsDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, IdentityRoleClaim<string>, IdentityUserToken<string>>   
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<PinnedPost> PinnedPosts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<PostTranslation> PostTranslations { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Category>().HasKey(sc => sc.Id);
            modelBuilder.Entity<PinnedPost>().HasKey(sc => sc.Id);
            modelBuilder.Entity<Post>().HasKey(sc => sc.Id);
            modelBuilder.Entity<Tag>().HasKey(sc => sc.Id);
            modelBuilder.Entity<Language>().HasKey(sc => sc.Id);
            modelBuilder.Entity<Role>().HasKey(sc => sc.Id);


            modelBuilder.Entity<PostTag>().HasKey(sc => new { sc.PostId, sc.TagId });
            modelBuilder.Entity<PostTranslation>().HasKey(sc => new { sc.PostId, sc.LanguageId });
            modelBuilder.Entity<CategoryTranslation>().HasKey(sc => new { sc.CategoryId, sc.LanguageId });
            modelBuilder.Entity<UserRole>().HasKey(sc => new { sc.UserId, sc.RoleId });

            modelBuilder.Entity<PostTag>()
                .HasOne(sc => sc.Post)
                .WithMany(s => s.PostTags)
                .HasForeignKey(sc => sc.PostId);
            modelBuilder.Entity<PostTag>()
                .HasOne(sc => sc.Tag)
                .WithMany(s => s.PostTags)
                .HasForeignKey(sc => sc.TagId);

            modelBuilder.Entity<Post>()
               .HasOne(s => s.Category)
               .WithMany(g => g.Posts)
               .HasForeignKey(s => s.CategoryId);
            modelBuilder.Entity<PinnedPost>()
                .HasOne(s => s.Post)
                .WithOne(s => s.PinnedPost)
                .HasForeignKey<PinnedPost>(s => s.PostId);

            modelBuilder.Entity<PostTranslation>()
                .HasOne(sc => sc.Post)
                .WithMany(s => s.PostTranslations)
                .HasForeignKey(sc => sc.PostId);
            modelBuilder.Entity<PostTranslation>()
               .HasOne(sc => sc.Language)
               .WithMany(s => s.PostTranslations)
               .HasForeignKey(sc => sc.LanguageId);

            modelBuilder.Entity<CategoryTranslation>()
                .HasOne(sc => sc.Category)
                .WithMany(s => s.CategoryTranslations)
                .HasForeignKey(sc => sc.CategoryId);
            modelBuilder.Entity<CategoryTranslation>()
               .HasOne(sc => sc.Language)
               .WithMany(s => s.CategoryTranslations)
               .HasForeignKey(sc => sc.LanguageId);



            modelBuilder.Entity<UserRole>()
                .HasOne(s => s.User)
                .WithMany(s => s.Roles)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(s => s.Role)
                .WithMany(s => s.Users)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<UserClaim>()
                .HasOne(s => s.User)
                .WithMany(s => s.Claims)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserLogin>()
                .HasOne(s => s.User)
                .WithMany(s => s.Logins)
                .HasForeignKey(x => x.UserId);

            //new added

            modelBuilder.Entity<Post>()  //
               .HasOne(s => s.AddedBy)
               .WithMany(g => g.Posts)
               .HasForeignKey(s => s.AddedById);

            modelBuilder.Entity<PostTranslation>() //
                .HasOne(sc => sc.AddedBy)
                .WithMany(s => s.PostTranslations)
                .HasForeignKey(sc => sc.AddedById);

            modelBuilder.Entity<Tag>()
               .HasOne(s => s.AddedBy)
               .WithMany(g => g.Tags)
               .HasForeignKey(s => s.AddedById);

            modelBuilder.Entity<Language>()
               .HasOne(s => s.AddedBy)
               .WithMany(g => g.Languages)
               .HasForeignKey(s => s.AddedById);

            modelBuilder.Entity<Category>()
               .HasOne(s => s.AddedBy)
               .WithMany(g => g.Categories)
               .HasForeignKey(s => s.AddedById);

            modelBuilder.Entity<CategoryTranslation>()
               .HasOne(s => s.AddedBy)
               .WithMany(g => g.CategoryTranslations)
               .HasForeignKey(s => s.AddedById);

            //////
            modelBuilder.Entity<Post>()
               .HasOne(s => s.UpdatedUser)
               .WithMany(g => g.UpdatedPosts)
               .HasForeignKey(s => s.UpdatedById);

            modelBuilder.Entity<PostTranslation>()
               .HasOne(s => s.UpdatedUser)
               .WithMany(g => g.UpdatedPostTranslations)
               .HasForeignKey(s => s.UpdatedById);

            modelBuilder.Entity<Tag>()
               .HasOne(s => s.UpdatedUser)
               .WithMany(g => g.UpdatedTags)
               .HasForeignKey(s => s.UpdatedById);

            modelBuilder.Entity<Language>()
               .HasOne(s => s.UpdatedUser)
               .WithMany(g => g.UpdatedLanguages)
               .HasForeignKey(s => s.UpdatedById);

            modelBuilder.Entity<Category>()
               .HasOne(s => s.UpdatedUser)
               .WithMany(g => g.UpdatedCategories)
               .HasForeignKey(s => s.UpdatedById);

            modelBuilder.Entity<CategoryTranslation>()
               .HasOne(s => s.UpdatedUser)
               .WithMany(g => g.UpdatedCategoryTranslations)
               .HasForeignKey(s => s.UpdatedById);



        }
    }
}
