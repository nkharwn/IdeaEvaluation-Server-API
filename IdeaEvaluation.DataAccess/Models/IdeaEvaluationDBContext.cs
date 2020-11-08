using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IdeaEvaluation.DataAccess.Models
{
    public partial class IdeaEvaluationDBContext : DbContext
    {
        public IdeaEvaluationDBContext()
        {
        }

        public IdeaEvaluationDBContext(DbContextOptions<IdeaEvaluationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Idea> Idea { get; set; }
        public virtual DbSet<IdeaEvaluationHistory> IdeaEvaluationHistory { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {  
                  optionsBuilder.UseSqlite("Data Source= c:\\Assignments\\IdeaEvaluation_Server\\IdeaEvaluation.DataAccess\\ApplicationData\\IdeaEvaluationDB;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Idea>(entity =>
            {
                entity.Property(e => e.IdeaId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("VARCHAR (500)");

                entity.Property(e => e.IdeaName)
                    .IsRequired()
                    .HasColumnType("VARCHAR (50)");
            });

            modelBuilder.Entity<IdeaEvaluationHistory>(entity =>
            {
                entity.HasKey(e => e.EvaluationId);

                entity.Property(e => e.EvaluationId).ValueGeneratedNever();

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.IdeaEvaluationHistory)
                    .HasForeignKey(d => d.IdeaId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IdeaEvaluationHistory)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("VARCHAR (50)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnType("VARCHAR (20)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
