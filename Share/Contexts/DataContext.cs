using Microsoft.EntityFrameworkCore;
using Share.Entities;

namespace Share.Contexts
{
    public partial class DataContext : DbContext
    {
        public DataContext() 
        { 
        }
        
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }
        //Detta blir databas tabeller: Entitetens namn och sedan namnet på tabellen.
        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<ManufacturerEntity> Manufacturer { get; set; }
        public virtual DbSet<CustomerEntity> Customer { get; set; }
        public virtual DbSet<AddressEntity> Address { get; set; }
        public virtual DbSet<SubCategoryEntity> Subcategories { get; set; }
        public virtual DbSet<ProductImagesEntity> ProductImages { get; set; }
        public virtual DbSet<ImagesEntity> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>()
                .HasIndex(x => x.CategoryName)
                .IsUnique();

            modelBuilder.Entity<ManufacturerEntity>()
                .HasIndex(x => x.ManufacturerName)
                .IsUnique();

            modelBuilder.Entity<CustomerEntity>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<ProductEntity>()
                .HasIndex(x => x.ArticleNumber)
                .IsUnique();

            modelBuilder.Entity<SubCategoryEntity>()
                .HasIndex(x => x.SubcategoryName)
                .IsUnique();

            modelBuilder.Entity<ProductEntity>()
                .HasKey(p => p.ArticleNumber);

            modelBuilder.Entity<ProductImagesEntity>()
                .HasKey(pi => new { pi.ArticleNumber, pi.ImageId });

            modelBuilder.Entity<ProductImagesEntity>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImageUrl)
                .HasForeignKey(pi => pi.ArticleNumber);

            modelBuilder.Entity<ProductImagesEntity>()
                .HasOne(pi => pi.Images)
                .WithMany(i => i.ProductImages)
                .HasForeignKey(pi => pi.ImageId);
        }
    }
}
