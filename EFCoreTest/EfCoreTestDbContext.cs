using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EFCoreTest
{
    public partial class EfCoreTestDbContext : DbContext
    {
        public EfCoreTestDbContext()
        {
        }

        public EfCoreTestDbContext(DbContextOptions<EfCoreTestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dependent> Dependents { get; set; }
        public virtual DbSet<DependentAttribute> DependentAttributes { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=EFCoreTest;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Dependent>(entity =>
            {
                entity.ToTable("Dependent");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Dependents)
                    .HasForeignKey(d => d.ParentId)
                    // .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dependent_Parent");
            });

            modelBuilder.Entity<DependentAttribute>(entity =>
            {
                entity.ToTable("DependentAttribute");

                entity.HasOne(d => d.Dependent)
                    .WithMany(p => p.DependentAttributes)
                    .HasForeignKey(d => d.DependentId)
                    // .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DependentAttribute_Dependent");
            });

            modelBuilder.Entity<Parent>(entity =>
            {
                entity.ToTable("Parent");

                entity.HasIndex(e => e.Name, "UQ_Parent")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
