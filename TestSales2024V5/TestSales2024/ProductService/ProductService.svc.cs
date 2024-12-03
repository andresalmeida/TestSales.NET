using BLL; // Agregar el espacio de nombres para ProductLogic
using Entities;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ProductService
{
    // Implementación del servicio SOAP
    public class ProductService : IProductService
    {
        private ProductLogic _productLogic;

        public ProductService()
        {
            _productLogic = new ProductLogic(); // Inicializar ProductLogic
        }

        public Products CreateProduct(Products product)
        {
            return _productLogic.Create(product);
        }

        public Products RetrieveProductById(int id)
        {
            return _productLogic.RetrieveById(id);
        }

        public bool UpdateProduct(Products product)
        {
            return _productLogic.Update(product);
        }

        public bool DeleteProduct(int id)
        {
            return _productLogic.Delete(id);
        }

        public List<Products> RetrieveAllProducts()
        {
            return _productLogic.RetrieveAll();
        }

        public List<Products> FilterProductsByName(string name)
        {
            return _productLogic.Filter(name);
        }
    }
}
