using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Share.Dtos;
using Share.Entities;
using Share.Repositories;
using System.Diagnostics;
using Share.Contexts;

namespace Share.Services
{
    public class ProductsService
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly ProductRepository _productRepository;
        private readonly DataContext _dataContext;

        public ProductsService(ProductRepository productRepository, CategoryRepository categoryRepository, DataContext dataContext)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _dataContext = dataContext;
        }

        public bool CreateProduct(Product product)
        {
            try
            {

                    if (!_productRepository.Exists(x => x.ArticleNumber == product.ArticleNumber))
                    {
                    var existingCategory = _dataContext.Categories.FirstOrDefault(x => x.CategoryName == product.CategoryName);
                    
                    if (existingCategory == null)
                    {
                        existingCategory = new CategoryEntity
                        {
                            CategoryName = product.CategoryName,
                        };

                        _dataContext.Categories.Add(existingCategory);
                    }

                    var existingManufacturer = _dataContext.Manufacturer.FirstOrDefault(x => x.ManufacturerName == product.ManufacturerName);

                    if (existingManufacturer == null)
                    {
                        existingManufacturer = new ManufacturerEntity
                        {
                            ManufacturerName = product.ManufacturerName,
                        };

                        _dataContext.Manufacturer.Add(existingManufacturer);

                    }

                    var productEntity = new ProductEntity
                    {
                        ArticleNumber = product.ArticleNumber,
                        Title = product.Title,
                        Description = product.Description,
                        SpecificationAsJson = product.SpecificationAsJson,
                        Price = product.Price,
                        Category = existingCategory,
                        Manufacturer = existingManufacturer
                    };

                                var result = _productRepository.Create(productEntity);

                                if (result != null)
                                {
                                    return true;
                                }
                }
            }
            catch (Exception ex) { Debug.WriteLine("CreateProductProdServ" + ex.Message); }

            return false;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = new List<Product>();

            try 
            {
                var result = _productRepository.GetAll();

                foreach (var item in result)
                {
                    products.Add(new Product
                    {
                        ArticleNumber = item.ArticleNumber,
                        Title = item.Title,
                        Description = item.Description,
                        SpecificationAsJson = item.SpecificationAsJson,
                        Price = item.Price,
                        CategoryName = item.Category.CategoryName,
                        ManufacturerName = item.Manufacturer.ManufacturerName
                    });
                 }
            }
            catch (Exception ex) { Debug.WriteLine("GetAllProductsProdServ" + ex.Message); }
            return products;
        }

        public ProductEntity GetOneProduct(string articleNumber)
        {
            try
            {
                var result = _productRepository.GetOne(x => x.ArticleNumber == articleNumber);

                if (result != null) 
                {
                    return result;
                }
                else
                { 
                    return null!; 
                }
            }
            catch (Exception ex) { Debug.WriteLine("GetOneProductService" + ex.Message);
                return null!;
            }
        }

        public bool UpdateProduct(string articleNumber, Action<ProductEntity> updateAction)
        {
            try
            {
                var existingProduct = _productRepository.GetOne(x => x.ArticleNumber == articleNumber);

                if (existingProduct != null)
                {
                    updateAction(existingProduct);
                    var result = _productRepository.UpdateProduct(existingProduct);
                    if (result != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateCustomerProdServ" + ex.Message);
            }

            return false;
        }

        public bool DeleteProduct(string articleNumber) 
        {
            try 
            {
                var productToDelete = _productRepository.GetOne(x => x.ArticleNumber == articleNumber);

                if(productToDelete != null)
                {
                    _productRepository.DeleteProduct(productToDelete);
                    return true;
                }
                return false;
            }
            catch(Exception ex) { Debug.WriteLine("DeleteProductProductServices" + ex.Message);
                return false; 
            }
        }















    }


}
