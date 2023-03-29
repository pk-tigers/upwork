﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UpWork.Database;

#nullable disable

namespace UpWork.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.AbsenceModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AbsenceTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("AbsenceTypeId1")
                        .HasColumnType("TEXT");

                    b.Property<int>("ApprovalState")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("From")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("To")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserModelId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AbsenceTypeId1");

                    b.HasIndex("UserModelId");

                    b.ToTable("AbsenceModel");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.AbsenceTypeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<bool>("NeedApproval")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("AbsenceTypeModel");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.OrganizationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Avatar")
                        .HasMaxLength(102400)
                        .HasColumnType("BLOB");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TimeOffSupervisorId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("OrganizationId");

                    b.HasIndex("TimeOffSupervisorId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.AbsenceModel", b =>
                {
                    b.HasOne("UpWork.Common.Models.DatabaseModels.AbsenceTypeModel", "AbsenceType")
                        .WithMany("Absences")
                        .HasForeignKey("AbsenceTypeId1");

                    b.HasOne("UpWork.Common.Models.DatabaseModels.UserModel", "UserModel")
                        .WithMany()
                        .HasForeignKey("UserModelId");

                    b.Navigation("AbsenceType");

                    b.Navigation("UserModel");
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

            modelBuilder.Entity("UpWork.Common.Models.DatabaseModels.UserModel", b =>
                {
                    b.HasOne("UpWork.Common.Models.DatabaseModels.OrganizationModel", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UpWork.Common.Models.DatabaseModels.UserModel", "TimeOffSupervisor")
                        .WithMany("SupervisedEmployees")
                        .HasForeignKey("TimeOffSupervisorId");

                    b.Navigation("Organization");

                    b.Navigation("TimeOffSupervisor");
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
                    b.Navigation("SupervisedEmployees");
                });
#pragma warning restore 612, 618
        }
    }
}
