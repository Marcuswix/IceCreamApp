using Share.Dtos;
using Share.Entities;
using Share.Repositories;
using System.Diagnostics;
using Share.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Share.Services
{
    public class ProductsService
    {
        private readonly ProductRepository _productRepository;
        private readonly DataContext _dataContext;

        public ProductsService(ProductRepository productRepository, DataContext dataContext)
        {
            _productRepository = productRepository;
            _dataContext = dataContext;
        }

        //Create
        public bool CreateProduct(Product product)
        {
            try
            {
                if (product.ArticleNumber == string.Empty)
                {
                    return false;
                }

                    if (!_productRepository.Exists(x => x.ArticleNumber == product.ArticleNumber))
                    {
                    var existingCategory = _dataContext.Categories
                        .Include(c => c.SubCategory)
                        .FirstOrDefault(x => x.CategoryName == product.CategoryName);

                    if (existingCategory == null)
                    {
                        existingCategory = new CategoryEntity
                        {
                            CategoryName = product.CategoryName,
                            SubCategory = new SubCategoryEntity
                            {
                                SubcategoryName = product.SubcategoryName!
                            }
                        };

                        _dataContext.Categories.Add(existingCategory);
                    }
                    else if (existingCategory.SubCategory == null)
                    {
                        _dataContext.Entry(existingCategory).Reference(c => c.SubCategory).Load();
                    }

                    var existingManufacturer = _dataContext.Manufacturer
                        .FirstOrDefault(x => x.ManufacturerName == product.ManufacturerName);

                    if (existingManufacturer == null)
                    {
                        existingManufacturer = new ManufacturerEntity
                        {
                            ManufacturerName = product.ManufacturerName,
                        };

                        _dataContext.Manufacturer.Add(existingManufacturer);
                    }

                    var existingImages = _dataContext.Images
                        .FirstOrDefault(x => x.ImageUrl == product.ImageUrl);

                    if (existingImages == null)
                    {
                        existingImages = new ImagesEntity
                        {
                            ImageUrl = product.ImageUrl!
                        };

                        _dataContext?.Images.Add(existingImages);
                    }

                    var productEntity = new ProductEntity
                    {
                        ArticleNumber = product.ArticleNumber,
                        Title = product.Title,
                        Description = product.Description,
                        SpecificationAsJson = product.SpecificationAsJson,
                        Price = product.Price,
                        Category = existingCategory,
                        Manufacturer = existingManufacturer,
                        ProductImageId = existingImages.Id
                    };

                           var result = _productRepository.Create(productEntity);

                            if (result != null)
                            {
                                return true;
                            }
                            else { return false; }
                    }
            }
            catch (Exception ex) { Debug.WriteLine("CreateProductProdServ" + ex.Message); }

            return false;
        }

        //Read
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = new List<Product>();

            try 
            {
                var result = await _productRepository.GetAllProducts();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        products.Add(new Product
                        {
                            ArticleNumber = item.ArticleNumber,
                            Title = item.Title,
                            Description = item.Description,
                            SpecificationAsJson = item.SpecificationAsJson,
                            Price = item.Price,
                            CategoryName = item.Category?.CategoryName ?? string.Empty,
                            SubcategoryName = item.Category?.SubCategory?.SubcategoryName ?? string.Empty,
                            ManufacturerName = item.Manufacturer.ManufacturerName,
                            ImageUrl = item.ProductImageUrl?.ToString() ?? string.Empty,
                        });
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine("GetAllProductsProdServ" + ex.Message); }
            return products;
        }

        public async Task<ProductEntity> GetOneProduct(string title)
        {
            try
            {
                var result = await _productRepository.GetOne(x => x.Title == title);

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

        //Update
        public async Task<bool> UpdateProduct(string articleNumber, Action<ProductEntity> updateAction)
        {
            try
            {
                var existingProduct = await _productRepository.GetOne(x => x.ArticleNumber == articleNumber);

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

        //Delete
        public async Task<bool> DeleteProduct(string articleNumber) 
        {
            try 
            {
                var productToDelete = await _productRepository.GetOne(x => x.ArticleNumber == articleNumber);

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
