﻿using Microsoft.EntityFrameworkCore;
using WebDevelopment.Infrastructure.Models;
using WebDevelopment.Infrastructure.Models.Auth;

namespace WebDevelopment.Infrastructure
{
    public class WebDevelopmentContext : DbContext
    {
        public WebDevelopmentContext()
        {
        }

        public WebDevelopmentContext(DbContextOptions<WebDevelopmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthUserModel> AuthUserModels { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<SalaryRange> SalaryRanges { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserPosition> UserPositions { get; set; } = null!;
        public virtual DbSet<UsersSalary> UsersSalaries { get; set; } = null!;
        public virtual DbSet<Models.Task> Tasks { get; set; } = null!;
        public virtual DbSet<UserTask> UserTasks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new ArgumentException("ConnectionString is ;not configured properly", nameof(optionsBuilder));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthUserModel>(entity =>
            {
                entity.HasIndex(e => e.UserName, "UC_User_Name")
                    .IsUnique();

                entity.HasIndex(e => e.SecuredPassword, "UC_Secured_Password")
                    .IsUnique();

                entity.HasIndex(e => e.UserEmail, "UC_User_Email")
                    .IsUnique();

                entity.Property(e => e.SecuredPassword).HasMaxLength(255);

                entity.Property(e => e.UserName).HasMaxLength(25);
                entity.Property(e => e.UserEmail).HasMaxLength(255);

                entity.HasOne(e => e.User)
                    .WithOne(u => u.AuthUserModel)
                    .HasForeignKey<AuthUserModel>(u => u.UserId);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.HasIndex(e => e.Alpha3Code, "UC_Country_Code")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UC_Country_Name")
                    .IsUnique();

                entity.Property(e => e.Alpha3Code).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ShortName).HasMaxLength(255);
            });

            modelBuilder.Entity<SalaryRange>(entity =>
            {
                entity.ToTable("SalaryRange");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.FinishRange).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StartRange).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.SalaryRanges)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__SalaryRan__Count__5EBF139D");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.SalaryRanges)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK__SalaryRan__Posit__412EB0B6");
            });

            modelBuilder.Entity<Models.Task>(entity =>
            {
                entity.Property(e => e.CreationDate).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserEmail, "UC_User_Email")
                    .IsUnique();

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.SecondName).HasMaxLength(255);

                entity.Property(e => e.UserEmail).HasMaxLength(255);
            });

            modelBuilder.Entity<UserPosition>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.PositionId, e.DepartmentId, e.StartDate }, "UC_Person")
                    .IsUnique();

                entity.Property(e => e.StartDate).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.UserPositions)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__UserPosit__Depar__49C3F6B7");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.UserPositions)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK__UserPosit__Posit__48CFD27E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPositions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserPosit__UserI__47DBAE45");
            });

            modelBuilder.Entity<UserTask>(entity =>
            {
                entity.HasIndex(e => e.TaskId, "IX_UserTasks_TaskId");

                entity.HasIndex(e => e.UserId, "IX_UserTasks_UserId");

                entity.Property(e => e.StartDate).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.ValidTill).HasDefaultValueSql("(dateadd(day,(7),sysdatetimeoffset()))");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.UserTasks)
                    .HasForeignKey(d => d.TaskId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTasks)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UsersSalary>(entity =>
            {
                entity.ToTable("UsersSalary");

                entity.Property(e => e.ChangeTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersSalaries)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UsersSala__UserI__440B1D61");
            });
            
        }
    }
}
