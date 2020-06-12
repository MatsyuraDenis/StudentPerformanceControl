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
    [Migration("20200612163427_Add_common_id_field_to_Student_table")]
    partial class Add_common_id_field_to_Student_table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-preview.4.20220.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataCore.EntityModels.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CuratorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeactivatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupTypeId")
                        .HasColumnType("int");

                    b.HasKey("GroupId");

                    b.HasIndex("GroupTypeId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DataCore.EntityModels.GroupType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GroupType");
                });

            modelBuilder.Entity("DataCore.EntityModels.HomeworkInfo", b =>
                {
                    b.Property<int>("HomeworkInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxPoints")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("SubjectSettingId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HomeworkInfoId");

                    b.HasIndex("SubjectSettingId");

                    b.ToTable("HomeworkInfo");
                });

            modelBuilder.Entity("DataCore.EntityModels.HomeworkResult", b =>
                {
                    b.Property<int>("HomeworkResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HomeworkInfoId")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("StudentPerformanceId")
                        .HasColumnType("int");

                    b.HasKey("HomeworkResultId");

                    b.HasIndex("StudentPerformanceId");

                    b.ToTable("HomeworkResults");
                });

            modelBuilder.Entity("DataCore.EntityModels.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CommonId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("DataCore.EntityModels.StudentPerformance", b =>
                {
                    b.Property<int>("StudentPerformanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExamPoints")
                        .HasColumnType("int");

                    b.Property<int>("Module1TestPoints")
                        .HasColumnType("int");

                    b.Property<int>("Module2TestPoints")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("StudentPerformanceId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("StudentPerformance");
                });

            modelBuilder.Entity("DataCore.EntityModels.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectInfoId")
                        .HasColumnType("int");

                    b.Property<int?>("SubjectSettingId")
                        .HasColumnType("int");

                    b.Property<int?>("TeacherId")
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

            modelBuilder.Entity("DataCore.EntityModels.SubjectSetting", b =>
                {
                    b.Property<int>("SubjectSettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExamMaxPoints")
                        .HasColumnType("int");

                    b.Property<int>("Module1TestMaxPoints")
                        .HasColumnType("int");

                    b.Property<int>("Module2TestMaxPoints")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("SubjectSettingId");

                    b.HasIndex("SubjectId")
                        .IsUnique();

                    b.ToTable("SubjectSetting");
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

                    b.HasIndex("GroupId")
                        .IsUnique()
                        .HasFilter("[GroupId] IS NOT NULL");

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

            modelBuilder.Entity("DataCore.EntityModels.Group", b =>
                {
                    b.HasOne("DataCore.EntityModels.GroupType", "GroupType")
                        .WithMany("Groups")
                        .HasForeignKey("GroupTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataCore.EntityModels.HomeworkInfo", b =>
                {
                    b.HasOne("DataCore.EntityModels.SubjectSetting", "SubjectSetting")
                        .WithMany("HomeworkInfos")
                        .HasForeignKey("SubjectSettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataCore.EntityModels.HomeworkResult", b =>
                {
                    b.HasOne("DataCore.EntityModels.StudentPerformance", "StudentPerformance")
                        .WithMany("HomeworkResults")
                        .HasForeignKey("StudentPerformanceId")
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

            modelBuilder.Entity("DataCore.EntityModels.StudentPerformance", b =>
                {
                    b.HasOne("DataCore.EntityModels.Student", "Student")
                        .WithMany("StudentPerformances")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataCore.EntityModels.Subject", "Subject")
                        .WithMany("StudentPerformances")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataCore.EntityModels.Subject", b =>
                {
                    b.HasOne("DataCore.EntityModels.Group", "Group")
                        .WithMany("Subjects")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataCore.EntityModels.SubjectInfo", "SubjectInfo")
                        .WithMany("Subjects")
                        .HasForeignKey("SubjectInfoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataCore.EntityModels.Teacher", null)
                        .WithMany("AssignedSubjects")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("DataCore.EntityModels.SubjectSetting", b =>
                {
                    b.HasOne("DataCore.EntityModels.Subject", "Subject")
                        .WithOne("SubjectSetting")
                        .HasForeignKey("DataCore.EntityModels.SubjectSetting", "SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataCore.EntityModels.Teacher", b =>
                {
                    b.HasOne("DataCore.EntityModels.Group", "Group")
                        .WithOne("Curator")
                        .HasForeignKey("DataCore.EntityModels.Teacher", "GroupId");
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
