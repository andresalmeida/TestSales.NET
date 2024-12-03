using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
            categories.CategoryName = "Test";
            categories.Description = "Descripcion Test";

            var product = new Products();
            product.ProductName = "Aji";
            product.UnitPrice = 5;
            product.UnitsInStock = 500;

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