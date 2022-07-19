﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebDevelopment.Infrastructure;

#nullable disable

namespace WebDevelopment.Infrastructure.Migrations
{
    [DbContext(typeof(WebDevelopmentContext))]
    [Migration("20220718190623_CreateTasksAndUsertasksTables")]
    partial class CreateTasksAndUsertasksTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Alpha3Code")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ShortName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.SalaryRange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int")
                        .HasColumnName("CountryID");

                    b.Property<DateTimeOffset?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("(sysdatetimeoffset())");

                    b.Property<decimal?>("FinishRange")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.Property<decimal?>("StartRange")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("PositionId");

                    b.ToTable("SalaryRange", (string)null);
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTimeOffset?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("(sysdatetimeoffset())");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SecondName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.UserPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("StartDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("(sysdatetimeoffset())");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PositionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPositions");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.UsersSalary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTimeOffset?>("ChangeTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("(sysdatetimeoffset())");

                    b.Property<decimal?>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UsersSalary", (string)null);
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.UserTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTimeOffset?>("FinishDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("StartDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("(sysdatetimeoffset())");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ValidTill")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("DATEADD(day, 7, sysdatetimeoffset())");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTasks");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.SalaryRange", b =>
                {
                    b.HasOne("WebDevelopment.Infrastructure.Models.Country", "Country")
                        .WithMany("SalaryRanges")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK__SalaryRan__Count__5EBF139D");

                    b.HasOne("WebDevelopment.Infrastructure.Models.Position", "Position")
                        .WithMany("SalaryRanges")
                        .HasForeignKey("PositionId")
                        .HasConstraintName("FK__SalaryRan__Posit__412EB0B6");

                    b.Navigation("Country");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.UserPosition", b =>
                {
                    b.HasOne("WebDevelopment.Infrastructure.Models.Department", "Department")
                        .WithMany("UserPositions")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK__UserPosit__Depar__49C3F6B7");

                    b.HasOne("WebDevelopment.Infrastructure.Models.Position", "Position")
                        .WithMany("UserPositions")
                        .HasForeignKey("PositionId")
                        .HasConstraintName("FK__UserPosit__Posit__48CFD27E");

                    b.HasOne("WebDevelopment.Infrastructure.Models.User", "User")
                        .WithMany("UserPositions")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__UserPosit__UserI__47DBAE45");

                    b.Navigation("Department");

                    b.Navigation("Position");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.UsersSalary", b =>
                {
                    b.HasOne("WebDevelopment.Infrastructure.Models.User", "User")
                        .WithMany("UsersSalaries")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__UsersSala__UserI__440B1D61");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.UserTask", b =>
                {
                    b.HasOne("WebDevelopment.Infrastructure.Models.Task", "Task")
                        .WithMany("UserTasks")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserTasks_Tasks_TaskId");

                    b.HasOne("WebDevelopment.Infrastructure.Models.User", "User")
                        .WithMany("UserTasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserTasks_Users_UserId");

                    b.Navigation("Task");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.Country", b =>
                {
                    b.Navigation("SalaryRanges");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.Department", b =>
                {
                    b.Navigation("UserPositions");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.Position", b =>
                {
                    b.Navigation("SalaryRanges");

                    b.Navigation("UserPositions");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.Task", b =>
                {
                    b.Navigation("UserTasks");
                });

            modelBuilder.Entity("WebDevelopment.Infrastructure.Models.User", b =>
                {
                    b.Navigation("UserPositions");

                    b.Navigation("UserTasks");

                    b.Navigation("UsersSalaries");
                });
#pragma warning restore 612, 618
        }
    }
}