﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UpWork.Database;

#nullable disable

namespace UpWork.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230421155827_Update_UserModel")]
    partial class Update_UserModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.AbsenceModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AbsenceTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ApprovalState")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("TimeOffSupervisorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AbsenceTypeId");

                    b.HasIndex("TimeOffSupervisorId");

                    b.HasIndex("UserId");

                    b.ToTable("AbsenceModel");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.AbsenceTypeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NeedApproval")
                        .HasColumnType("bit");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("AbsenceTypeModel");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.OrganizationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UrlName")
                        .IsUnique();

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.PermissionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("GrantDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PermissionType")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Avatar")
                        .HasMaxLength(102400)
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid?>("CurrentTimeOffSupervisorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CurrentTimeOffSupervisorId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("OrganizationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.AbsenceModel", b =>
                {
                    b.HasOne("UpWork.Common.Models.DatabaseModels.AbsenceTypeModel", "AbsenceType")
                        .WithMany("Absences")
                        .HasForeignKey("AbsenceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UpWork.Common.Models.DatabaseModels.UserModel", "TimeOffSupervisor")
                        .WithMany("AbsencesSupervised")
                        .HasForeignKey("TimeOffSupervisorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("UpWork.Common.Models.DatabaseModels.UserModel", "User")
                        .WithMany("Absences")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AbsenceType");

                    b.Navigation("TimeOffSupervisor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.AbsenceTypeModel", b =>
                {
                    b.HasOne("UpWork.Common.Models.DatabaseModels.OrganizationModel", "Organization")
                        .WithMany("AbsenceTypes")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.PermissionModel", b =>
                {
                    b.HasOne("UpWork.Common.Models.DatabaseModels.UserModel", "User")
                        .WithMany("Permissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.UserModel", b =>
                {
                    b.HasOne("UpWork.Common.Models.DatabaseModels.UserModel", "CurrentTimeOffSupervisor")
                        .WithMany("SupervisedEmployees")
                        .HasForeignKey("CurrentTimeOffSupervisorId");

                    b.HasOne("UpWork.Common.Models.DatabaseModels.OrganizationModel", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationId");

                    b.Navigation("CurrentTimeOffSupervisor");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.AbsenceTypeModel", b =>
                {
                    b.Navigation("Absences");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.OrganizationModel", b =>
                {
                    b.Navigation("AbsenceTypes");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.UserModel", b =>
                {
                    b.Navigation("Absences");

                    b.Navigation("AbsencesSupervised");

                    b.Navigation("Permissions");

                    b.Navigation("SupervisedEmployees");
                });
#pragma warning restore 612, 618
        }
    }
}
