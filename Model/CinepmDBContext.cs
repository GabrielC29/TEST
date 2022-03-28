using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TEST.Model
{
    public partial class CinepmDBContext : DbContext
    {
        public CinepmDBContext()
        {
        }

        public CinepmDBContext(DbContextOptions<CinepmDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Database=CinepmDB;username=postgres;password=newpassword");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Sex)
                    .HasMaxLength(10)
                    .HasColumnName("sex");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, null, true);

                entity.Property(e => e.Datebirth).HasColumnName("datebirth");

                entity.Property(e => e.Dni)
                    .HasMaxLength(8)
                    .HasColumnName("dni");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(40)
                    .HasColumnName("firstname");

                entity.Property(e => e.Genre).HasColumnName("genre");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(40)
                    .HasColumnName("lastname");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(9)
                    .HasColumnName("phone");

                entity.HasOne(d => d.GenreNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Genre)
                    .HasConstraintName("fk_users_genre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
