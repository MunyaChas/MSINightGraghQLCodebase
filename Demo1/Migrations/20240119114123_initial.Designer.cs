﻿// <auto-generated />
using System;
using Demo1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Demo1.Migrations
{
    [DbContext(typeof(Demo1DbContext))]
    [Migration("20240119114123_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Demo1.Data.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EmployeeId");

                    b.HasKey("Id");

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("Demo1.Data.MatchToOpenRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("MatchToOpenRequestId");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OpenRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OpenRequestId");

                    b.ToTable("MatchToOpenRequests", (string)null);
                });

            modelBuilder.Entity("Demo1.Data.OpenRequestBU", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("OpenRequestId");

                    b.HasKey("Id");

                    b.ToTable("OpenRequestBUs", (string)null);
                });

            modelBuilder.Entity("Demo1.Data.Employee", b =>
                {
                    b.OwnsOne("Demo1.Data.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("EmployeeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Email");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employees");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.OwnsOne("Demo1.Data.EmployeeCode", "EmployeeCode", b1 =>
                        {
                            b1.Property<Guid>("EmployeeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("EmployeeCode");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employees");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.OwnsOne("Demo1.Data.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("EmployeeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("First")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("Last")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("LastName");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employees");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.OwnsOne("Demo1.Data.Phone", "Phone", b1 =>
                        {
                            b1.Property<Guid>("EmployeeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Phone");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employees");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.OwnsMany("Demo1.Data.Qualification", "Qualifications", b1 =>
                        {
                            b1.Property<Guid>("QualificationId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("QualificationId");

                            b1.Property<Guid>("EmployeeId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("QualificationId");

                            b1.HasIndex("EmployeeId");

                            b1.ToTable("Qualifications", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");

                            b1.OwnsOne("Demo1.Data.Institute", "Institute", b2 =>
                                {
                                    b2.Property<Guid>("QualificationId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("nvarchar(50)")
                                        .HasColumnName("Institute");

                                    b2.HasKey("QualificationId");

                                    b2.ToTable("Qualifications");

                                    b2.WithOwner()
                                        .HasForeignKey("QualificationId");
                                });

                            b1.OwnsOne("Demo1.Data.NameOfQualification", "NameOfQualification", b2 =>
                                {
                                    b2.Property<Guid>("QualificationId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("nvarchar(50)")
                                        .HasColumnName("NameOfQualification");

                                    b2.HasKey("QualificationId");

                                    b2.ToTable("Qualifications");

                                    b2.WithOwner()
                                        .HasForeignKey("QualificationId");
                                });

                            b1.OwnsOne("Demo1.Data.Year", "YearCompleted", b2 =>
                                {
                                    b2.Property<Guid>("QualificationId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int?>("Value")
                                        .IsRequired()
                                        .HasColumnType("int")
                                        .HasColumnName("YearCompleted");

                                    b2.HasKey("QualificationId");

                                    b2.ToTable("Qualifications");

                                    b2.WithOwner()
                                        .HasForeignKey("QualificationId");
                                });

                            b1.Navigation("Institute")
                                .IsRequired();

                            b1.Navigation("NameOfQualification")
                                .IsRequired();

                            b1.Navigation("YearCompleted")
                                .IsRequired();
                        });

                    b.OwnsMany("Demo1.Data.SkillsMatrix", "SkillsMatrices", b1 =>
                        {
                            b1.Property<Guid>("SkillsMatrixId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("SkillsMatrixId");

                            b1.Property<Guid>("EmployeeId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("SkillsMatrixId");

                            b1.HasIndex("EmployeeId");

                            b1.ToTable("SkillsMatrices", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");

                            b1.OwnsOne("Demo1.Data.Skill", "Skill", b2 =>
                                {
                                    b2.Property<Guid>("SkillsMatrixId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("nvarchar(50)")
                                        .HasColumnName("Skill");

                                    b2.HasKey("SkillsMatrixId");

                                    b2.ToTable("SkillsMatrices");

                                    b2.WithOwner()
                                        .HasForeignKey("SkillsMatrixId");
                                });

                            b1.OwnsOne("Demo1.Data.SkillLevel", "SkillLevel", b2 =>
                                {
                                    b2.Property<Guid>("SkillsMatrixId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("SkillLevel");

                                    b2.HasKey("SkillsMatrixId");

                                    b2.ToTable("SkillsMatrices");

                                    b2.WithOwner()
                                        .HasForeignKey("SkillsMatrixId");
                                });

                            b1.OwnsOne("Demo1.Data.YearOfExperience", "YearsOfExperience", b2 =>
                                {
                                    b2.Property<Guid>("SkillsMatrixId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int?>("Value")
                                        .IsRequired()
                                        .HasColumnType("int")
                                        .HasColumnName("YearsOfExperience");

                                    b2.HasKey("SkillsMatrixId");

                                    b2.ToTable("SkillsMatrices");

                                    b2.WithOwner()
                                        .HasForeignKey("SkillsMatrixId");
                                });

                            b1.Navigation("Skill")
                                .IsRequired();

                            b1.Navigation("SkillLevel");

                            b1.Navigation("YearsOfExperience")
                                .IsRequired();
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("EmployeeCode")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Phone")
                        .IsRequired();

                    b.Navigation("Qualifications");

                    b.Navigation("SkillsMatrices");
                });

            modelBuilder.Entity("Demo1.Data.MatchToOpenRequest", b =>
                {
                    b.HasOne("Demo1.Data.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Demo1.Data.OpenRequestBU", "OpenRequest")
                        .WithMany()
                        .HasForeignKey("OpenRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Demo1.Data.ApplyForPosition", "ApplyForPosition", b1 =>
                        {
                            b1.Property<Guid>("MatchToOpenRequestId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("Value")
                                .HasColumnType("bit")
                                .HasColumnName("ApplyForPosition");

                            b1.HasKey("MatchToOpenRequestId");

                            b1.ToTable("MatchToOpenRequests");

                            b1.WithOwner()
                                .HasForeignKey("MatchToOpenRequestId");
                        });

                    b.OwnsOne("Demo1.Data.InterviewDate", "InterviewDate", b1 =>
                        {
                            b1.Property<Guid>("MatchToOpenRequestId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("Value")
                                .HasColumnType("datetime2")
                                .HasColumnName("InterviewDate");

                            b1.HasKey("MatchToOpenRequestId");

                            b1.ToTable("MatchToOpenRequests");

                            b1.WithOwner()
                                .HasForeignKey("MatchToOpenRequestId");
                        });

                    b.OwnsOne("Demo1.Data.IsClosed", "IsClosed", b1 =>
                        {
                            b1.Property<Guid>("MatchToOpenRequestId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("Value")
                                .HasColumnType("bit")
                                .HasColumnName("IsClosed");

                            b1.HasKey("MatchToOpenRequestId");

                            b1.ToTable("MatchToOpenRequests");

                            b1.WithOwner()
                                .HasForeignKey("MatchToOpenRequestId");
                        });

                    b.OwnsOne("Demo1.Data.IsHired", "IsHired", b1 =>
                        {
                            b1.Property<Guid>("MatchToOpenRequestId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("Value")
                                .HasColumnType("bit")
                                .HasColumnName("IsHired");

                            b1.HasKey("MatchToOpenRequestId");

                            b1.ToTable("MatchToOpenRequests");

                            b1.WithOwner()
                                .HasForeignKey("MatchToOpenRequestId");
                        });

                    b.OwnsOne("Demo1.Data.IsMatch", "IsMatch", b1 =>
                        {
                            b1.Property<Guid>("MatchToOpenRequestId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("Value")
                                .HasColumnType("bit")
                                .HasColumnName("IsMatch");

                            b1.HasKey("MatchToOpenRequestId");

                            b1.ToTable("MatchToOpenRequests");

                            b1.WithOwner()
                                .HasForeignKey("MatchToOpenRequestId");
                        });

                    b.OwnsOne("Demo1.Data.IsOpen", "IsOpen", b1 =>
                        {
                            b1.Property<Guid>("MatchToOpenRequestId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("Value")
                                .HasColumnType("bit")
                                .HasColumnName("IsOpen");

                            b1.HasKey("MatchToOpenRequestId");

                            b1.ToTable("MatchToOpenRequests");

                            b1.WithOwner()
                                .HasForeignKey("MatchToOpenRequestId");
                        });

                    b.OwnsOne("Demo1.Data.IsWithdrawn", "IsWithdrawn", b1 =>
                        {
                            b1.Property<Guid>("MatchToOpenRequestId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("Value")
                                .HasColumnType("bit")
                                .HasColumnName("IsWithdrawn");

                            b1.HasKey("MatchToOpenRequestId");

                            b1.ToTable("MatchToOpenRequests");

                            b1.WithOwner()
                                .HasForeignKey("MatchToOpenRequestId");
                        });

                    b.OwnsOne("Demo1.Data.MatchScore", "MatchScore", b1 =>
                        {
                            b1.Property<Guid>("MatchToOpenRequestId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("MatchScore");

                            b1.HasKey("MatchToOpenRequestId");

                            b1.ToTable("MatchToOpenRequests");

                            b1.WithOwner()
                                .HasForeignKey("MatchToOpenRequestId");
                        });

                    b.Navigation("ApplyForPosition")
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("InterviewDate");

                    b.Navigation("IsClosed")
                        .IsRequired();

                    b.Navigation("IsHired")
                        .IsRequired();

                    b.Navigation("IsMatch")
                        .IsRequired();

                    b.Navigation("IsOpen")
                        .IsRequired();

                    b.Navigation("IsWithdrawn")
                        .IsRequired();

                    b.Navigation("MatchScore")
                        .IsRequired();

                    b.Navigation("OpenRequest");
                });

            modelBuilder.Entity("Demo1.Data.OpenRequestBU", b =>
                {
                    b.OwnsOne("Demo1.Data.AccountManager", "AccountManager", b1 =>
                        {
                            b1.Property<Guid>("OpenRequestBUId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("AccountManager");

                            b1.HasKey("OpenRequestBUId");

                            b1.ToTable("OpenRequestBUs");

                            b1.WithOwner()
                                .HasForeignKey("OpenRequestBUId");
                        });

                    b.OwnsMany("Demo1.Data.Competence", "Competences", b1 =>
                        {
                            b1.Property<Guid>("OpenRequestBUId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Competence");

                            b1.Property<int>("YearsOfExperience")
                                .HasColumnType("int")
                                .HasColumnName("YearsOfExperience");

                            b1.HasKey("OpenRequestBUId", "Id");

                            b1.ToTable("Competence");

                            b1.WithOwner()
                                .HasForeignKey("OpenRequestBUId");
                        });

                    b.OwnsOne("Demo1.Data.DeadLine", "DeadLine", b1 =>
                        {
                            b1.Property<Guid>("OpenRequestBUId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("datetime2")
                                .HasColumnName("DeadLine");

                            b1.HasKey("OpenRequestBUId");

                            b1.ToTable("OpenRequestBUs");

                            b1.WithOwner()
                                .HasForeignKey("OpenRequestBUId");
                        });

                    b.OwnsOne("Demo1.Data.Department", "Cluster", b1 =>
                        {
                            b1.Property<Guid>("OpenRequestBUId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Cluster");

                            b1.HasKey("OpenRequestBUId");

                            b1.ToTable("OpenRequestBUs");

                            b1.WithOwner()
                                .HasForeignKey("OpenRequestBUId");
                        });

                    b.OwnsOne("Demo1.Data.Location", "Location", b1 =>
                        {
                            b1.Property<Guid>("OpenRequestBUId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Location");

                            b1.HasKey("OpenRequestBUId");

                            b1.ToTable("OpenRequestBUs");

                            b1.WithOwner()
                                .HasForeignKey("OpenRequestBUId");
                        });

                    b.OwnsOne("Demo1.Data.NumberOfFTERequired", "NumberOfFTERequired", b1 =>
                        {
                            b1.Property<Guid>("OpenRequestBUId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("NumberOfFTERequired");

                            b1.HasKey("OpenRequestBUId");

                            b1.ToTable("OpenRequestBUs");

                            b1.WithOwner()
                                .HasForeignKey("OpenRequestBUId");
                        });

                    b.OwnsOne("Demo1.Data.SkillLevel", "SkillLevel", b1 =>
                        {
                            b1.Property<Guid>("OpenRequestBUId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("SkillLevel");

                            b1.HasKey("OpenRequestBUId");

                            b1.ToTable("OpenRequestBUs");

                            b1.WithOwner()
                                .HasForeignKey("OpenRequestBUId");
                        });

                    b.OwnsOne("Demo1.Data.PositionDescription", "PositionDescription", b1 =>
                        {
                            b1.Property<Guid>("OpenRequestBUId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("PositionDescription");

                            b1.HasKey("OpenRequestBUId");

                            b1.ToTable("OpenRequestBUs");

                            b1.WithOwner()
                                .HasForeignKey("OpenRequestBUId");
                        });

                    b.OwnsOne("Demo1.Data.PositionName", "PositionName", b1 =>
                        {
                            b1.Property<Guid>("OpenRequestBUId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("PositionName");

                            b1.HasKey("OpenRequestBUId");

                            b1.ToTable("OpenRequestBUs");

                            b1.WithOwner()
                                .HasForeignKey("OpenRequestBUId");
                        });

                    b.OwnsOne("Demo1.Data.RoleStartDate", "RoleStartDate", b1 =>
                        {
                            b1.Property<Guid>("OpenRequestBUId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("datetime2")
                                .HasColumnName("RoleStartDate");

                            b1.HasKey("OpenRequestBUId");

                            b1.ToTable("OpenRequestBUs");

                            b1.WithOwner()
                                .HasForeignKey("OpenRequestBUId");
                        });

                    b.OwnsOne("Demo1.Data.TeamRequestId", "TeamRequestId", b1 =>
                        {
                            b1.Property<Guid>("OpenRequestBUId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("TeamRequestId");

                            b1.HasKey("OpenRequestBUId");

                            b1.ToTable("OpenRequestBUs");

                            b1.WithOwner()
                                .HasForeignKey("OpenRequestBUId");
                        });

                    b.OwnsOne("Demo1.Data.TeamRequestName", "TeamRequestName", b1 =>
                        {
                            b1.Property<Guid>("OpenRequestBUId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("TeamRequestName");

                            b1.HasKey("OpenRequestBUId");

                            b1.ToTable("OpenRequestBUs");

                            b1.WithOwner()
                                .HasForeignKey("OpenRequestBUId");
                        });

                    b.Navigation("AccountManager")
                        .IsRequired();

                    b.Navigation("Cluster")
                        .IsRequired();

                    b.Navigation("Competences");

                    b.Navigation("DeadLine")
                        .IsRequired();

                    b.Navigation("Location")
                        .IsRequired();

                    b.Navigation("NumberOfFTERequired")
                        .IsRequired();

                    b.Navigation("PositionDescription")
                        .IsRequired();

                    b.Navigation("PositionName")
                        .IsRequired();

                    b.Navigation("RoleStartDate")
                        .IsRequired();

                    b.Navigation("SkillLevel")
                        .IsRequired();

                    b.Navigation("TeamRequestId")
                        .IsRequired();

                    b.Navigation("TeamRequestName")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
