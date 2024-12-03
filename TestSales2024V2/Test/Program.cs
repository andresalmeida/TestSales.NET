using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            addCategoryAndProduct();
        }

        static void addCategoryAndProduct()
        {
            var categories = new Categories();
            categories.CategoryName = "Test 2";
            categories.Description = "Description Test 2";

            var product = new Products();
            product.ProductName = "Product Test 2";
            product.UnitPrice = 100;
            product.UnitsInStock = 10;

            categories.Products.Add(product);
            using (var repository = RepositoryFactory.CreateRepository())
            {
                repository.Create(categories);
            }
            Console.WriteLine($"Categoria: {categories.CategoryID}, Producto: {product.ProductID}");
            Console.ReadLine();
        }
    }
}
