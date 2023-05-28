﻿// <auto-generated />
using System;
using HelloJobBackEnd.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HelloJobBackEnd.Migrations
{
    [DbContext(typeof(HelloJobDbContext))]
    [Migration("20230528102235_AddSubscribeTable")]
    partial class AddSubscribeTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HelloJobBackEnd.Entities.BusinessArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BusinessTitleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessTitleId");

                    b.ToTable("BusinessArea");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.BusinessTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BusinessTitle");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Cv", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BornDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("BusinessAreaId")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CvPDF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DrivingLicense")
                        .HasColumnType("bit");

                    b.Property<int>("EducationId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExperienceId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("OperatingModeId")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessAreaId");

                    b.HasIndex("CityId");

                    b.HasIndex("EducationId");

                    b.HasIndex("ExperienceId");

                    b.HasIndex("OperatingModeId");

                    b.HasIndex("UserId");

                    b.ToTable("Cvs");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Experience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.InfoEmployeer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VacansId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VacansId");

                    b.ToTable("InfoEmployeers");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.InfoWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VacansId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VacansId");

                    b.ToTable("InfoWorks");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.OperatingMode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OperatingModes");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CvId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("VacansId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CvId");

                    b.HasIndex("UserId");

                    b.HasIndex("VacansId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.RequestItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CvId")
                        .HasColumnType("int");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("VacansId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CvId");

                    b.HasIndex("RequestId");

                    b.HasIndex("VacansId");

                    b.ToTable("RequestItems");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Rules", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("CV")
                        .HasColumnType("bit");

                    b.Property<string>("Rule")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Vacans")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Subscribe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subscribe");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Vacans", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BusinessAreaId")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DrivingLicense")
                        .HasColumnType("bit");

                    b.Property<int>("EducationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExperienceId")
                        .HasColumnType("int");

                    b.Property<int>("OperatingModeId")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Salary")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessAreaId");

                    b.HasIndex("CityId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("EducationId");

                    b.HasIndex("ExperienceId");

                    b.HasIndex("OperatingModeId");

                    b.ToTable("Vacans");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.WishList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("WishLists");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.WishListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CvId")
                        .HasColumnType("int");

                    b.Property<bool>("IsLiked")
                        .HasColumnType("bit");

                    b.Property<int?>("VacansId")
                        .HasColumnType("int");

                    b.Property<int>("WishListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CvId");

                    b.HasIndex("VacansId");

                    b.HasIndex("WishListId");

                    b.ToTable("WishListItems");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.BusinessArea", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.BusinessTitle", "BusinessTitle")
                        .WithMany("BusinessAreas")
                        .HasForeignKey("BusinessTitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessTitle");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Company", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.User", "User")
                        .WithMany("Companies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Cv", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.BusinessArea", "BusinessArea")
                        .WithMany("Cvs")
                        .HasForeignKey("BusinessAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelloJobBackEnd.Entities.City", "City")
                        .WithMany("Cvs")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelloJobBackEnd.Entities.Education", "Education")
                        .WithMany("Cvs")
                        .HasForeignKey("EducationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelloJobBackEnd.Entities.Experience", "Experience")
                        .WithMany("Cvs")
                        .HasForeignKey("ExperienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelloJobBackEnd.Entities.OperatingMode", "OperatingMode")
                        .WithMany("Cvs")
                        .HasForeignKey("OperatingModeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelloJobBackEnd.Entities.User", "User")
                        .WithMany("Cvs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessArea");

                    b.Navigation("City");

                    b.Navigation("Education");

                    b.Navigation("Experience");

                    b.Navigation("OperatingMode");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.InfoEmployeer", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.Vacans", "Vacans")
                        .WithMany("infoEmployeers")
                        .HasForeignKey("VacansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vacans");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.InfoWork", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.Vacans", "Vacans")
                        .WithMany("InfoWorks")
                        .HasForeignKey("VacansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vacans");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Message", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Request", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.Cv", null)
                        .WithMany("Requests")
                        .HasForeignKey("CvId");

                    b.HasOne("HelloJobBackEnd.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HelloJobBackEnd.Entities.Vacans", null)
                        .WithMany("Requests")
                        .HasForeignKey("VacansId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.RequestItem", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.Cv", "Cv")
                        .WithMany()
                        .HasForeignKey("CvId");

                    b.HasOne("HelloJobBackEnd.Entities.Request", "Request")
                        .WithMany("RequestItems")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelloJobBackEnd.Entities.Vacans", "Vacans")
                        .WithMany()
                        .HasForeignKey("VacansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cv");

                    b.Navigation("Request");

                    b.Navigation("Vacans");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Vacans", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.BusinessArea", "BusinessArea")
                        .WithMany("Vacans")
                        .HasForeignKey("BusinessAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelloJobBackEnd.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelloJobBackEnd.Entities.Company", "Company")
                        .WithMany("Vacans")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelloJobBackEnd.Entities.Education", "Education")
                        .WithMany()
                        .HasForeignKey("EducationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelloJobBackEnd.Entities.Experience", "Experience")
                        .WithMany()
                        .HasForeignKey("ExperienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelloJobBackEnd.Entities.OperatingMode", "OperatingMode")
                        .WithMany()
                        .HasForeignKey("OperatingModeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessArea");

                    b.Navigation("City");

                    b.Navigation("Company");

                    b.Navigation("Education");

                    b.Navigation("Experience");

                    b.Navigation("OperatingMode");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.WishList", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.WishListItem", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.Cv", "Cv")
                        .WithMany("WishListItems")
                        .HasForeignKey("CvId");

                    b.HasOne("HelloJobBackEnd.Entities.Vacans", "Vacans")
                        .WithMany("WishListItems")
                        .HasForeignKey("VacansId");

                    b.HasOne("HelloJobBackEnd.Entities.WishList", "WishList")
                        .WithMany("WishListItems")
                        .HasForeignKey("WishListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cv");

                    b.Navigation("Vacans");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelloJobBackEnd.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HelloJobBackEnd.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.BusinessArea", b =>
                {
                    b.Navigation("Cvs");

                    b.Navigation("Vacans");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.BusinessTitle", b =>
                {
                    b.Navigation("BusinessAreas");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.City", b =>
                {
                    b.Navigation("Cvs");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Company", b =>
                {
                    b.Navigation("Vacans");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Cv", b =>
                {
                    b.Navigation("Requests");

                    b.Navigation("WishListItems");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Education", b =>
                {
                    b.Navigation("Cvs");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Experience", b =>
                {
                    b.Navigation("Cvs");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.OperatingMode", b =>
                {
                    b.Navigation("Cvs");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Request", b =>
                {
                    b.Navigation("RequestItems");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.User", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("Cvs");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.Vacans", b =>
                {
                    b.Navigation("InfoWorks");

                    b.Navigation("Requests");

                    b.Navigation("WishListItems");

                    b.Navigation("infoEmployeers");
                });

            modelBuilder.Entity("HelloJobBackEnd.Entities.WishList", b =>
                {
                    b.Navigation("WishListItems");
                });
#pragma warning restore 612, 618
        }
    }
}
