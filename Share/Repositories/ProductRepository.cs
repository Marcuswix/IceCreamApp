using Microsoft.EntityFrameworkCore;
using Share.Contexts;
using Share.Entities;
using System.Diagnostics;

namespace Share.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity>
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        //Create
        public override Task<ProductEntity> Create(ProductEntity entity)
        {
            return base.Create(entity);
        }

        //Read
        public async Task<IEnumerable<ProductEntity>> GetAllProducts()
        {
            try
            {
                var result = await _context.Products
                    .Include(c => c.Category)
                    .Include(m => m.Manufacturer)
                    .Include(x => x.ProductImageUrl)
                    .ToListAsync();

                return result;

            }
            catch (Exception ex) { Debug.WriteLine("getAllProductsRes" + ex.Message);
                return null!; 
            }
        }

        //Update
        public ProductEntity UpdateProduct(ProductEntity product)
        {
            try
            {
                var entityToUpdate = _context.Products.Find(product.ArticleNumber);

                if (entityToUpdate != null)
                {
                    _context.Entry(entityToUpdate).CurrentValues.SetValues(product);
                    _context.SaveChanges();
                    return entityToUpdate;
                }

                return null!;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateProduct" + ex.Message);
                return null!;
            }
        }

        //Delete
        public bool DeleteProduct(ProductEntity product)
        {
            try
            {
                var entityToDelete = _context.Set<ProductEntity>().Find(product.ArticleNumber);

                if (entityToDelete != null)
                {
                    _context.Remove(entityToDelete);
                    _context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateProduct" + ex.Message);
                return false;
            }
        }
    }
}
