using Microsoft.EntityFrameworkCore;
using Share.Contexts;
using Share.Entities;
using System.Diagnostics;
using System.Linq.Expressions;


namespace Share.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity>
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<ProductEntity> GetAll()
        {
            try
            {
                var result = _context.Products
                    .Include(c => c.Category)
                    .Include(m => m.Manufacturer)
                    .ToList();
                return result; 
            }
            catch (Exception ex) { Debug.WriteLine("getAllProductsRes" + ex.Message);
                return null!; 
            }
        }

        public ProductEntity UpdateProduct(ProductEntity product)
        {
            try
            {
                var entityToUpdate = _context.Set<ProductEntity>().Find(product.ArticleNumber);

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
