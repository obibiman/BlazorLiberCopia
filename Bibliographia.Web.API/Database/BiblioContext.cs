using System;
using System.Collections.Generic;
using System.Security.Policy;
using Bibliographia.Web.API.Models.ApiGateway;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bibliographia.Web.API.Models.Domain
{
    public partial class BiblioContext : IdentityDbContext<ApiUser>
    {
        public BiblioContext()
        {
        }

        public BiblioContext(DbContextOptions<BiblioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Publisher> Publishers { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.Bio).HasMaxLength(500);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Genre).HasMaxLength(25);

                entity.Property(e => e.ImageUrl).HasMaxLength(100);

                entity.Property(e => e.Isbn).HasMaxLength(15);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.Year).HasMaxLength(4);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Book_Author");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("Publisher");

                entity.Property(e => e.CompanyUrl).HasMaxLength(100);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            //do identityrole matter here
            modelBuilder.Entity<IdentityRole>().HasData
              (
               new IdentityRole
               {
                   Name = "User",
                   NormalizedName = "USER",
                   Id = "e312ad81-8e51-4256-a660-df740d8c5c88"
               },
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = "e4866a3d-ee37-498a-b68c-3aa641cb51f5"
                }
               );

            //do users matter here
            PasswordHasher<ApiUser>? hasher = new PasswordHasher<ApiUser>();
            modelBuilder.Entity<ApiUser>().HasData
              (
               new ApiUser
               {
                   Id = "7f3a7775-8726-4b1e-b3e1-2dd2e3a162b9",
                   Email = "user@bookstore.com",
                   NormalizedEmail = "USER@BOOKSTORE.COM",
                   UserName = "user@bookstore.com",
                   NormalizedUserName = "USER@BOOKSTORE.COM",
                   FirstName = "System",
                   LastName = "User",
                   PasswordHash = hasher.HashPassword(null, "P@ssword1")
               },
                new ApiUser
                {
                    Id = "64701336-a647-426a-afab-ff3efb23a443",
                    Email = "admin@bookstore.com",
                    NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                    UserName = "admin@bookstore.com",
                    NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1")
                }
               );
            //linking roles to users
            modelBuilder.Entity<IdentityUserRole<string>>().HasData
              (
            new IdentityUserRole<string>
            {
                RoleId = "e312ad81-8e51-4256-a660-df740d8c5c88",
                UserId = "7f3a7775-8726-4b1e-b3e1-2dd2e3a162b9"
            },
            new IdentityUserRole<string>
            {
                RoleId = "e4866a3d-ee37-498a-b68c-3aa641cb51f5",
                UserId = "64701336-a647-426a-afab-ff3efb23a443"
            }
            );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
