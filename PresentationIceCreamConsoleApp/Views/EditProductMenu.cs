﻿using Share.Dtos;
using Share.Entities;
using Share.Services;

namespace PresentationIceCreamConsoleApp.Views
{
    public class EditProductMenu
    {
        private readonly ProductsService _productsService;

        public EditProductMenu(ProductsService productsService)
        {
            _productsService = productsService;
        }

        public void ShowEditProductMenu()
        {

            Console.Clear();
            Console.WriteLine("Edit Product");
            Console.WriteLine("#############");
            Console.WriteLine("Write the article Number of the product you wish to edit:");
            string productToEdit = Console.ReadLine()!;

            var result = _productsService.UpdateProduct(productToEdit, editedProduct =>
            {
                Console.WriteLine("New Title:");
                editedProduct.Title = Console.ReadLine()!;
                Console.WriteLine("Description:");
                editedProduct.Description = Console.ReadLine()!;
                Console.WriteLine("Price:");
                editedProduct.Price = decimal.Parse(Console.ReadLine()!);

                if (editedProduct.Category == null)
                {
                    editedProduct.Category = new CategoryEntity();
                }
                Console.WriteLine("New Category:");
                editedProduct.Category.CategoryName = Console.ReadLine()!;


                if (editedProduct.Manufacturer == null)
                {
                    editedProduct.Manufacturer = new ManufacturerEntity();
                }

                Console.WriteLine("New Manufacturer:");
                editedProduct.Manufacturer.ManufacturerName = Console.ReadLine()!;
            });

            if (result == true)
            {
                Console.Clear();
                Console.WriteLine("The Product was successfully updated. Press any key to continue.");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("There is no product with that article number... Press any key to continue.");
                Console.ReadKey();
            }
        }
    }
}
