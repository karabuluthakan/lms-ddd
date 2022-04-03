﻿// <auto-generated />
using System;
using Infrastructure.DataAccess.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.DataAccess.EntityFramework.Migrations
{
    [DbContext(typeof(LmsDbContext))]
    [Migration("20220403115543_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<Guid>("CoursesId")
                        .HasColumnType("uuid")
                        .HasColumnName("coursesId");

                    b.Property<Guid>("StudentsId")
                        .HasColumnType("uuid")
                        .HasColumnName("studentsId");

                    b.HasKey("CoursesId", "StudentsId")
                        .HasName("pK_courseStudent");

                    b.HasIndex("StudentsId")
                        .HasDatabaseName("iX_courseStudent_studentsId");

                    b.ToTable("courseStudent", "public");
                });

            modelBuilder.Entity("Domain.Entities.Courses.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CoursePrefix")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar")
                        .HasColumnName("coursePrefix");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("time with time zone")
                        .HasColumnName("createdAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pK_courses");

                    b.HasIndex("CoursePrefix")
                        .IsUnique()
                        .HasDatabaseName("iX_courses_coursePrefix");

                    b.ToTable("courses", "public");
                });

            modelBuilder.Entity("Domain.Entities.Students.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birthDate");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("time with time zone")
                        .HasColumnName("createdAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar")
                        .HasColumnName("firstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar")
                        .HasColumnName("lastName");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("passwordHash");

                    b.HasKey("Id")
                        .HasName("pK_students");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("iX_students_email");

                    b.ToTable("students", "public");
                });

            modelBuilder.Entity("Domain.Entities.SystemUsers.SystemUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("time with time zone")
                        .HasColumnName("createdAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar")
                        .HasColumnName("email");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("passwordHash");

                    b.HasKey("Id")
                        .HasName("pK_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("iX_users_email");

                    b.ToTable("users", "public");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("Domain.Entities.Courses.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_courseStudent_courses_coursesId");

                    b.HasOne("Domain.Entities.Students.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_courseStudent_students_studentsId");
                });
#pragma warning restore 612, 618
        }
    }
}
