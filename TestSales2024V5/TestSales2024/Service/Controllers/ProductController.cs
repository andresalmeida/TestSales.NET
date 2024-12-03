using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        private readonly ProductLogic _productLogic;

        public ProductController()
        {
            _productLogic = new ProductLogic();
        }

        // POST: Create a new product
        [HttpPost]
        [Route("CreateProduct")]
        public IHttpActionResult CreateProduct([FromBody] Products product)
        {
            if (product == null)
                return BadRequest("Product data cannot be null.");

            if (string.IsNullOrEmpty(product.ProductName))
                return BadRequest("Product name is required.");

            if (product.UnitPrice < 0)
                return BadRequest("Unit price cannot be negative.");

            if (product.UnitsInStock < 0)
                return BadRequest("Units in stock cannot be negative.");

            try
            {
                var createdProduct = _productLogic.Create(product);
                return Created($"api/Product/RetrieveProductById/{createdProduct.ProductID}", createdProduct);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: Retrieve a product by ID
        [HttpGet]
        [Route("RetrieveProductById/{id:int}")]
        public IHttpActionResult RetrieveProductById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid product ID.");

            var product = _productLogic.RetrieveById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // PUT: Update an existing product
        [HttpPut]
        [Route("UpdateProduct")]
        public IHttpActionResult UpdateProduct([FromBody] Products product)
        {
            if (product == null || product.ProductID <= 0)
                return BadRequest("Invalid product data.");

            if (string.IsNullOrEmpty(product.ProductName))
                return BadRequest("Product name is required.");

            if (product.UnitPrice < 0)
                return BadRequest("Unit price cannot be negative.");

            if (product.UnitsInStock < 0)
                return BadRequest("Units in stock cannot be negative.");

            try
            {
                var result = _productLogic.Update(product);
                if (result)
                    return Ok("Product updated successfully.");
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: Delete a product by ID
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid product ID.");

            try
            {
                var product = _productLogic.RetrieveById(id);
                if (product == null)
                    return NotFound(); // Producto no encontrado

                if (product.UnitsInStock > 0)
                    return BadRequest("Cannot delete product. UnitsInStock must be 0 to delete.");

                var result = _productLogic.Delete(id);
                if (result)
                    return Ok("Product deleted successfully.");

                return InternalServerError(); // Falla inesperada
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }




        // GET: Filter products by name
        [HttpGet]
        [Route("FilterProducts")]
        public IHttpActionResult FilterProducts(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Name filter cannot be empty.");

            var products = _productLogic.Filter(name);
            return Ok(products);
        }

        // GET: Retrieve all products
        [HttpGet]
        [Route("GetAllProducts")]
        public IHttpActionResult GetAllProducts()
        {
            var products = _productLogic.RetrieveAll();
            return Ok(products);
        }
    }
}