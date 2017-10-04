using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Saleman.Data.EntityFramework;

namespace Saleman.Data.EntityFramework.Migrations
{
    [DbContext(typeof(SalemanDbContext))]
    [Migration("20170426022423_CreateModel")]
    partial class CreateModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Saleman.Model.Entities.AdditionalInformationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("InformationType")
                        .IsRequired();

                    b.Property<string>("Name")
                        .HasMaxLength(300);

                    b.Property<Guid>("StoreDetailId");

                    b.HasKey("Id");

                    b.HasIndex("StoreDetailId");

                    b.ToTable("AdditionalInformations");

                    b.HasDiscriminator<string>("InformationType").HasValue("AdditionalInformationEntity");
                });

            modelBuilder.Entity("Saleman.Model.Entities.AddressEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<Guid>("DistrictId");

                    b.Property<string>("Lane")
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Saleman.Model.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AvatarId");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<Guid?>("ParentCategoryId");

                    b.Property<bool>("ShowOnMenu");

                    b.Property<Guid>("StoreId");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.HasIndex("ParentCategoryId");

                    b.HasIndex("StoreId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Saleman.Model.Entities.LocationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LocationType");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.ToTable("Locations");

                    b.HasDiscriminator<int>("LocationType");
                });

            modelBuilder.Entity("Saleman.Model.Entities.MediaAssetEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedById");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("FileName")
                        .HasMaxLength(1000);

                    b.Property<string>("Link")
                        .HasMaxLength(1000);

                    b.Property<string>("MediaType")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("MediaAssets");

                    b.HasDiscriminator<string>("MediaType").HasValue("MediaAssetEntity");
                });

            modelBuilder.Entity("Saleman.Model.Entities.MediaProductEntity", b =>
                {
                    b.Property<Guid>("ProductDetailId");

                    b.Property<Guid>("MediaId");

                    b.HasKey("ProductDetailId", "MediaId");

                    b.HasIndex("MediaId");

                    b.ToTable("MediaProducts");
                });

            modelBuilder.Entity("Saleman.Model.Entities.ProductDetailEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("CostPerUnit");

                    b.Property<Guid>("ProductId");

                    b.Property<Guid?>("UnitId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.HasIndex("UnitId");

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("Saleman.Model.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AvatarId");

                    b.Property<Guid>("CategoryId");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<Guid?>("StoreDetailEntityId");

                    b.Property<Guid>("StoreId");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("StoreDetailEntityId");

                    b.HasIndex("StoreId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Saleman.Model.Entities.SaleAuditEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<long>("SaleId");

                    b.Property<string>("SalemanId")
                        .IsRequired();

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<decimal>("Total");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("SaleId");

                    b.HasIndex("SalemanId");

                    b.ToTable("SaleAudits");

                    b.HasDiscriminator<string>("Status").HasValue("SaleAuditEntity");
                });

            modelBuilder.Entity("Saleman.Model.Entities.SaleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Discount");

                    b.Property<Guid>("ProductId");

                    b.Property<Guid>("StoreId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.HasIndex("StoreId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Saleman.Model.Entities.StoreAddressEntity", b =>
                {
                    b.Property<Guid>("AddressId");

                    b.Property<Guid>("StoreDetailId");

                    b.HasKey("AddressId", "StoreDetailId");

                    b.HasIndex("StoreDetailId");

                    b.ToTable("StoreAddresses");
                });

            modelBuilder.Entity("Saleman.Model.Entities.StoreDetailEntity", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("OwnerId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("StoreDetails");
                });

            modelBuilder.Entity("Saleman.Model.Entities.StoreEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Saleman.Model.Entities.UnitEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Symbol")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("Saleman.Model.Entities.UserEntity", b =>
                {
                    b.Property<string>("Id");

                    b.HasKey("Id");

                    b.ToTable("UserExtends");
                });

            modelBuilder.Entity("Saleman.Model.Entities.EmailInformationEntity", b =>
                {
                    b.HasBaseType("Saleman.Model.Entities.AdditionalInformationEntity");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.ToTable("EmailInformationEntity");

                    b.HasDiscriminator().HasValue("email");
                });

            modelBuilder.Entity("Saleman.Model.Entities.GenericInformationEntity", b =>
                {
                    b.HasBaseType("Saleman.Model.Entities.AdditionalInformationEntity");

                    b.Property<string>("Information")
                        .HasMaxLength(1000);

                    b.ToTable("GenericInformationEntity");

                    b.HasDiscriminator().HasValue("general");
                });

            modelBuilder.Entity("Saleman.Model.Entities.PhoneInformationEntity", b =>
                {
                    b.HasBaseType("Saleman.Model.Entities.AdditionalInformationEntity");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.ToTable("PhoneInformationEntity");

                    b.HasDiscriminator().HasValue("phone");
                });

            modelBuilder.Entity("Saleman.Model.Entities.SocialInformationEntity", b =>
                {
                    b.HasBaseType("Saleman.Model.Entities.AdditionalInformationEntity");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.ToTable("SocialInformationEntity");

                    b.HasDiscriminator().HasValue("social");
                });

            modelBuilder.Entity("Saleman.Model.Entities.DistrictEntity", b =>
                {
                    b.HasBaseType("Saleman.Model.Entities.LocationEntity");

                    b.Property<Guid?>("ParentLocationId");

                    b.HasIndex("ParentLocationId");

                    b.ToTable("DistrictEntity");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Saleman.Model.Entities.ProvinceEntity", b =>
                {
                    b.HasBaseType("Saleman.Model.Entities.LocationEntity");


                    b.ToTable("ProvinceEntity");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Saleman.Model.Entities.FileAssetEntity", b =>
                {
                    b.HasBaseType("Saleman.Model.Entities.MediaAssetEntity");


                    b.ToTable("FileAssetEntity");

                    b.HasDiscriminator().HasValue("unknow");
                });

            modelBuilder.Entity("Saleman.Model.Entities.ImageMediaEntity", b =>
                {
                    b.HasBaseType("Saleman.Model.Entities.MediaAssetEntity");


                    b.ToTable("ImageMediaEntity");

                    b.HasDiscriminator().HasValue("image");
                });

            modelBuilder.Entity("Saleman.Model.Entities.PdfMediaEntity", b =>
                {
                    b.HasBaseType("Saleman.Model.Entities.MediaAssetEntity");


                    b.ToTable("PdfMediaEntity");

                    b.HasDiscriminator().HasValue("pdf");
                });

            modelBuilder.Entity("Saleman.Model.Entities.VideoMediaEntity", b =>
                {
                    b.HasBaseType("Saleman.Model.Entities.MediaAssetEntity");


                    b.ToTable("VideoMediaEntity");

                    b.HasDiscriminator().HasValue("video");
                });

            modelBuilder.Entity("Saleman.Model.Entities.ApprovedSaleAuditEntity", b =>
                {
                    b.HasBaseType("Saleman.Model.Entities.SaleAuditEntity");


                    b.ToTable("ApprovedSaleAuditEntity");

                    b.HasDiscriminator().HasValue("approved");
                });

            modelBuilder.Entity("Saleman.Model.Entities.PendingSaleAuditEntity", b =>
                {
                    b.HasBaseType("Saleman.Model.Entities.SaleAuditEntity");


                    b.ToTable("PendingSaleAuditEntity");

                    b.HasDiscriminator().HasValue("pending");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Saleman.Model.Entities.AdditionalInformationEntity", b =>
                {
                    b.HasOne("Saleman.Model.Entities.StoreDetailEntity")
                        .WithMany("AdditionalInformation")
                        .HasForeignKey("StoreDetailId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Saleman.Model.Entities.AddressEntity", b =>
                {
                    b.HasOne("Saleman.Model.Entities.DistrictEntity", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Saleman.Model.Entities.CategoryEntity", b =>
                {
                    b.HasOne("Saleman.Model.Entities.ImageMediaEntity", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId");

                    b.HasOne("Saleman.Model.Entities.CategoryEntity", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId");

                    b.HasOne("Saleman.Model.Entities.StoreEntity", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Saleman.Model.Entities.MediaAssetEntity", b =>
                {
                    b.HasOne("Saleman.Model.Entities.UserEntity", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");
                });

            modelBuilder.Entity("Saleman.Model.Entities.MediaProductEntity", b =>
                {
                    b.HasOne("Saleman.Model.Entities.MediaAssetEntity", "Media")
                        .WithMany()
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Saleman.Model.Entities.ProductDetailEntity", "ProductDetail")
                        .WithMany("MediaAssets")
                        .HasForeignKey("ProductDetailId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Saleman.Model.Entities.ProductDetailEntity", b =>
                {
                    b.HasOne("Saleman.Model.Entities.ProductEntity", "Product")
                        .WithOne("Detail")
                        .HasForeignKey("Saleman.Model.Entities.ProductDetailEntity", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Saleman.Model.Entities.UnitEntity", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");
                });

            modelBuilder.Entity("Saleman.Model.Entities.ProductEntity", b =>
                {
                    b.HasOne("Saleman.Model.Entities.ImageMediaEntity", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId");

                    b.HasOne("Saleman.Model.Entities.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Saleman.Model.Entities.StoreDetailEntity")
                        .WithMany("Products")
                        .HasForeignKey("StoreDetailEntityId");

                    b.HasOne("Saleman.Model.Entities.StoreEntity", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Saleman.Model.Entities.SaleAuditEntity", b =>
                {
                    b.HasOne("Saleman.Model.Entities.SaleEntity", "Sale")
                        .WithMany("Audits")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Saleman.Model.Entities.UserEntity", "Saleman")
                        .WithMany()
                        .HasForeignKey("SalemanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Saleman.Model.Entities.SaleEntity", b =>
                {
                    b.HasOne("Saleman.Model.Entities.ProductEntity", "Product")
                        .WithOne("Sale")
                        .HasForeignKey("Saleman.Model.Entities.SaleEntity", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Saleman.Model.Entities.StoreEntity", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Saleman.Model.Entities.StoreAddressEntity", b =>
                {
                    b.HasOne("Saleman.Model.Entities.AddressEntity", "Address")
                        .WithMany("StoreAddress")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Saleman.Model.Entities.StoreDetailEntity", "StoreDetail")
                        .WithMany("StoreAddress")
                        .HasForeignKey("StoreDetailId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Saleman.Model.Entities.StoreDetailEntity", b =>
                {
                    b.HasOne("Saleman.Model.Entities.StoreEntity", "Store")
                        .WithOne("Detail")
                        .HasForeignKey("Saleman.Model.Entities.StoreDetailEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Saleman.Model.Entities.UserEntity", "Owner")
                        .WithMany("Stores")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Saleman.Model.Entities.UserEntity", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser", "User")
                        .WithOne()
                        .HasForeignKey("Saleman.Model.Entities.UserEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Saleman.Model.Entities.DistrictEntity", b =>
                {
                    b.HasOne("Saleman.Model.Entities.ProvinceEntity", "ParentLocation")
                        .WithMany("Districts")
                        .HasForeignKey("ParentLocationId");
                });
        }
    }
}
