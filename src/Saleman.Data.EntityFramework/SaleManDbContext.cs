namespace Saleman.Data.EntityFramework
{
    using WebFramework.Infrastructure.Ultility;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Model.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SalemanDbContext : IdentityDbContext
    {
        protected IDateTimeAdapter DateTimeProvider { get; set; }

        public virtual DbSet<UserEntity> UserEntities { get; set; }

        public SalemanDbContext()
        {

        }

        public SalemanDbContext(IDateTimeAdapter dateTimeAdapter)
        {
            this.DateTimeProvider = dateTimeAdapter;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=./Saleman.db");
            //optionsBuilder
            //    .UseMySql(@"Server=localhost;database=saleman;uid=saleman;pwd=Saleman#2017;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // User
            builder.Entity<UserEntity>()
                .ToTable("UserExtends")
                .HasOne(u => u.User)
                .WithOne()
                .HasPrincipalKey<IdentityUser>(u => u.Id);

            // Unit
            builder.Entity<UnitEntity>()
                .ToTable("Units");

            // Store
            builder.Entity<StoreEntity>()
                .ToTable("Stores")
                .HasOne(s => s.Detail)
                .WithOne(sd => sd.Store)
                .HasForeignKey<StoreDetailEntity>(sd => sd.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<StoreDetailEntity>()
                .ToTable("StoreDetails")
                .HasMany(sa => sa.StoreAddress)
                .WithOne(s => s.StoreDetail)
                .HasForeignKey(ad => ad.StoreDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<StoreDetailEntity>()
                   .HasMany(sd => sd.AdditionalInformation)
                   .WithOne()
                   .HasForeignKey(ad => ad.StoreDetailId);

            builder.Entity<StoreDetailEntity>()
                   .HasOne(sd => sd.Owner)
                   .WithMany(u => u.Stores)
                   .HasForeignKey(ad => ad.OwnerId);

            // Location
            builder.Entity<LocationEntity>()
                .ToTable("Locations")
                .HasDiscriminator<int>(nameof(LocationType))
                .HasValue<DistrictEntity>((int)LocationType.District)
                .HasValue<ProvinceEntity>((int)LocationType.Province);

            builder.Entity<DistrictEntity>()
                .HasOne(m => m.ParentLocation)
                .WithMany(m => m.Districts)
                .HasForeignKey(md => md.ParentLocationId);

            // Store Addresses
            builder.Entity<StoreAddressEntity>()
                .ToTable("StoreAddresses")
                .HasKey(s => new { s.AddressId, s.StoreDetailId });

            // Address
            builder.Entity<AddressEntity>()
                .ToTable("Addresses")
                .HasOne(m => m.District)
                .WithMany()
                .HasForeignKey(md => md.DistrictId);

            builder.Entity<AddressEntity>()
                .HasMany(ad => ad.StoreAddress)
                .WithOne(sa => sa.Address)
                .HasForeignKey(sa => sa.AddressId);

            // Media
            builder.Entity<MediaAssetEntity>()
                .ToTable("MediaAssets")
                .HasDiscriminator<string>("MediaType")
                .HasValue<ImageMediaEntity>(MediaAssetEntity.MediaType.Image)
                .HasValue<VideoMediaEntity>(MediaAssetEntity.MediaType.Video)
                .HasValue<FileAssetEntity>(MediaAssetEntity.MediaType.Unknow)
                .HasValue<PdfMediaEntity>(MediaAssetEntity.MediaType.Pdf);

            builder.Entity<MediaAssetEntity>()
                .HasOne(m => m.CreatedBy)
                .WithMany()
                .HasForeignKey(md => md.CreatedById);

            // Category
            builder.Entity<CategoryEntity>()
                .ToTable("Categories")
                .HasOne(c => c.Avatar)
                .WithMany()
                .HasForeignKey(c => c.AvatarId);

            builder.Entity<CategoryEntity>()
                .HasOne(c => c.Store)
                .WithMany()
                .HasForeignKey(c => c.StoreId);

            builder.Entity<CategoryEntity>()
                .HasOne(c => c.ParentCategory)
                .WithMany(m => m.SubCategories)
                .HasForeignKey(s => s.ParentCategoryId);

            // Product
            builder.Entity<ProductEntity>()
                .ToTable("Products")
                .HasOne(p => p.Detail)
                .WithOne(pd => pd.Product)
                .HasForeignKey<ProductDetailEntity>(pd => pd.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductEntity>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            builder.Entity<ProductEntity>()
                .HasOne(p => p.Avatar)
                .WithMany()
                .HasForeignKey(pa => pa.AvatarId);

            // Product Detail
            builder.Entity<ProductDetailEntity>()
                .ToTable("ProductDetails")
                .HasOne(pd => pd.Unit)
                .WithMany()
                .HasForeignKey(pd => pd.UnitId);

            // Media product
            builder.Entity<MediaProductEntity>()
                .ToTable("MediaProducts")
                .HasKey(t => new { t.ProductDetailId, t.MediaId });

            builder.Entity<MediaProductEntity>()
                .HasOne(mp => mp.ProductDetail)
                .WithMany(p => p.MediaAssets)
                .HasForeignKey(p => p.ProductDetailId);

            builder.Entity<MediaProductEntity>()
                .HasOne(mp => mp.Media)
                .WithMany()
                .HasForeignKey(m => m.MediaId);

            // Sale
            builder.Entity<SaleEntity>()
                .ToTable("Sales")
                .HasOne(s => s.Product)
                .WithOne(p => p.Sale)
                .HasForeignKey<SaleEntity>(sa => sa.ProductId);

            builder.Entity<SaleEntity>()
                .HasOne(s => s.Store)
                .WithMany()
                .HasForeignKey(sa => sa.StoreId);

            // Sale Audit
            builder.Entity<SaleAuditEntity>()
                .ToTable("SaleAudits")
                .HasDiscriminator<string>("Status")
                .HasValue<PendingSaleAuditEntity>("pending")
                .HasValue<ApprovedSaleAuditEntity>("approved");

            builder.Entity<SaleAuditEntity>()
                .HasOne(sa => sa.Sale)
                .WithMany(s => s.Audits)
                .HasForeignKey(ad => ad.SaleId);

            // Additional information
            builder.Entity<AdditionalInformationEntity>()
                .ToTable("AdditionalInformations")
                .HasDiscriminator<string>("InformationType")
                .HasValue<EmailInformationEntity>("email")
                .HasValue<PhoneInformationEntity>("phone")
                .HasValue<SocialInformationEntity>("social")
                .HasValue<GenericInformationEntity>("general");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == EntityState.Added || x.State == EntityState.Modified));
            UpdateAuditableEntities(modifiedEntries);

            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == EntityState.Added || x.State == EntityState.Modified)).ToList();

            UpdateAuditableEntities(modifiedEntries);

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditableEntities(IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> modifiedEntries)
        {
            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    var now = this.DateTimeProvider.Now;

                    if (entry.State == EntityState.Added)
                    {
                        entity.Created = now;
                    }
                    else
                    {
                        Entry(entity).Property(x => x.Created).IsModified = false;
                    }

                    entity.Updated = now;
                }
            }
        }
    }
}