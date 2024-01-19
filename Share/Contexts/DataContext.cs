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
        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<ManufacturerEntity> Manufacturer { get; set; }
        public virtual DbSet<CustomerEntity> Customer { get; set; }
        public virtual DbSet<AddressEntity> Address { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<CategoryEntity>()
        //        .HasIndex(x => x.CategoryName)
        //        .IsUnique();

        //    modelBuilder.Entity<ManufacturerEntity>()
        //        .HasIndex(x => x.ManufacturerName)
        //        .IsUnique();

        //    modelBuilder.Entity<CustomerEntity>()
        //        .HasIndex(x => x.Email)
        //        .IsUnique();

        //    modelBuilder.Entity<ProductEntity>()
        //        .HasIndex(x => x.ArticleNumber)
        //        .IsUnique();
        //}
    }
}
