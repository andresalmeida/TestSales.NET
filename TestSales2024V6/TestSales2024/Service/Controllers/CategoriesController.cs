using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Controllers
{
    [RoutePrefix("api/Categories")]
    public class CategoriesController : ApiController
    {
        private readonly CategoriesLogic _categoriesLogic;

        public CategoriesController()
        {
            _categoriesLogic = new CategoriesLogic();
        }

        // POST: Create a new category
        [HttpPost]
        [Route("CreateCategory")]
        public IHttpActionResult CreateCategory([FromBody] Categories category)
        {
            if (category == null)
                return BadRequest("Category data cannot be null.");

            if (string.IsNullOrEmpty(category.CategoryName))
                return BadRequest("Category name is required.");

            try
            {
                var createdCategory = _categoriesLogic.Create(category);
                return Created($"api/Categories/RetrieveCategoryById/{createdCategory.CategoryID}", createdCategory);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: Retrieve a category by ID
        [HttpGet]
        [Route("RetrieveCategoryById/{id:int}")]
        public IHttpActionResult RetrieveCategoryById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid category ID.");

            var category = _categoriesLogic.RetrieveById(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // PUT: Update an existing category
        [HttpPut]
        [Route("UpdateCategory")]
        public IHttpActionResult UpdateCategory([FromBody] Categories category)
        {
            if (category == null || category.CategoryID <= 0)
                return BadRequest("Invalid category data.");

            if (string.IsNullOrEmpty(category.CategoryName))
                return BadRequest("Category name is required.");

            try
            {
                var result = _categoriesLogic.Update(category);
                if (result)
                    return Ok("Category updated successfully.");
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: Delete a category by ID
        [HttpDelete]
        [Route("DeleteCategory/{id:int}")]
        public IHttpActionResult DeleteCategory(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid category ID.");

            try
            {
                var result = _categoriesLogic.Delete(id);
                if (result)
                    return Ok("Category deleted successfully.");
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: Filter categories by name
        [HttpGet]
        [Route("FilterCategories")]
        public IHttpActionResult FilterCategories(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Name filter cannot be empty.");

            var categories = _categoriesLogic.Filter(name);
            return Ok(categories);
        }

        // GET: Retrieve all categories
        [HttpGet]
        [Route("GetAllCategories")]
        public IHttpActionResult GetAllCategories()
        {
            var categories = _categoriesLogic.RetrieveAll();
            return Ok(categories);
        }
    }
}