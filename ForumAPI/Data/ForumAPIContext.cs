using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ForumAPI.Models;

namespace ForumAPI.Data
{
    public class ForumAPIContext : DbContext
    {
        public ForumAPIContext (DbContextOptions<ForumAPIContext> options)
            : base(options)
        {
        }

        public DbSet<ForumAPI.Models.Category> Category { get; set; }

        public DbSet<ForumAPI.Models.SubCategory> SubCategory { get; set; }

        public DbSet<ForumAPI.Models.Post> Post { get; set; }

        public DbSet<ForumAPI.Models.Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubCategory>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Post>()
                .HasOne<SubCategory>()
                .WithMany()
                .HasForeignKey(p => p.SubCatId);

            modelBuilder.Entity<Comment>()
                .HasOne<Post>()
                .WithMany()
                .HasForeignKey(p => p.PostId);
        }
    }
}
