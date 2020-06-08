﻿// <auto-generated />
using System;
using DataCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataCore.Migrations
{
    [DbContext(typeof(SPCContext))]
    [Migration("20200607154023_Add_MAxPoints_to_Laboratory_table")]
    partial class Add_MAxPoints_to_Laboratory_table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-preview.4.20220.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataCore.EntityModels.Exam", b =>
                {
                    b.Property<int>("ExamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("ExamId");

                    b.HasIndex("SubjectId")
                        .IsUnique();

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("DataCore.EntityModels.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CuratorId")
                        .HasColumnType("int");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId");

                    b.HasIndex("CuratorId")
                        .IsUnique();

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DataCore.EntityModels.Laboratory", b =>
                {
                    b.Property<int>("LaboratoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxPoints")
                        .HasColumnType("int");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Task")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LaboratoryId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Laboratories");
                });

            modelBuilder.Entity("DataCore.EntityModels.Module", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.HasKey("ModuleId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TestId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("DataCore.EntityModels.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DataCore.EntityModels.StudentGrade", b =>
                {
                    b.Property<int>("StudentGradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ExamId")
                        .HasColumnType("int");

                    b.Property<int?>("LaboratoryId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("TestId")
                        .HasColumnType("int");

                    b.HasKey("StudentGradeId");

                    b.HasIndex("ExamId");

                    b.HasIndex("LaboratoryId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TestId");

                    b.ToTable("StudentGrades");
                });

            modelBuilder.Entity("DataCore.EntityModels.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExmaId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectInfoId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("SubjectId");

                    b.HasIndex("GroupId");

                    b.HasIndex("SubjectInfoId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("DataCore.EntityModels.SubjectInfo", b =>
                {
                    b.Property<int>("SubjectInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubjectInfoId");

                    b.ToTable("SubjectInfos");
                });

            modelBuilder.Entity("DataCore.EntityModels.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeacherId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("DataCore.EntityModels.TeacherSubjectInfo", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectInfoId")
                        .HasColumnType("int");

                    b.HasKey("TeacherId", "SubjectInfoId");

                    b.HasIndex("SubjectInfoId");

                    b.ToTable("TeacherSubjectInfo");
                });

            modelBuilder.Entity("DataCore.EntityModels.Test", b =>
                {
                    b.Property<int>("TestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("TestId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("DataCore.EntityModels.Exam", b =>
                {
                    b.HasOne("DataCore.EntityModels.Subject", "Subject")
                        .WithOne("Exam")
                        .HasForeignKey("DataCore.EntityModels.Exam", "SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataCore.EntityModels.Group", b =>
                {
                    b.HasOne("DataCore.EntityModels.Teacher", "Curator")
                        .WithOne("Group")
                        .HasForeignKey("DataCore.EntityModels.Group", "CuratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataCore.EntityModels.Laboratory", b =>
                {
                    b.HasOne("DataCore.EntityModels.Module", "Module")
                        .WithMany("Laboratories")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataCore.EntityModels.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataCore.EntityModels.Module", b =>
                {
                    b.HasOne("DataCore.EntityModels.Subject", null)
                        .WithMany("Modules")
                        .HasForeignKey("SubjectId");

                    b.HasOne("DataCore.EntityModels.Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataCore.EntityModels.Student", b =>
                {
                    b.HasOne("DataCore.EntityModels.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataCore.EntityModels.StudentGrade", b =>
                {
                    b.HasOne("DataCore.EntityModels.Exam", "Exam")
                        .WithMany("StudentGrades")
                        .HasForeignKey("ExamId");

                    b.HasOne("DataCore.EntityModels.Laboratory", "Laboratory")
                        .WithMany("StudentGrades")
                        .HasForeignKey("LaboratoryId");

                    b.HasOne("DataCore.EntityModels.Student", null)
                        .WithMany("StudentGrades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataCore.EntityModels.Test", "Test")
                        .WithMany("StudentGrades")
                        .HasForeignKey("TestId");
                });

            modelBuilder.Entity("DataCore.EntityModels.Subject", b =>
                {
                    b.HasOne("DataCore.EntityModels.Group", "Group")
                        .WithMany("Subjects")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataCore.EntityModels.SubjectInfo", "SubjectInfo")
                        .WithMany("Subjects")
                        .HasForeignKey("SubjectInfoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataCore.EntityModels.Teacher", "Teacher")
                        .WithMany("AssignedSubjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataCore.EntityModels.TeacherSubjectInfo", b =>
                {
                    b.HasOne("DataCore.EntityModels.SubjectInfo", "SubjectInfo")
                        .WithMany("TeacherSubjectInfos")
                        .HasForeignKey("SubjectInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataCore.EntityModels.Teacher", "Teacher")
                        .WithMany("TeacherSubjectInfos")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
