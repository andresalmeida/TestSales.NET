using BLL;
using Entities;
using System.Collections.Generic;
using System.Web.Http;

namespace DepartmentService.Controllers
{
    [RoutePrefix("api/departamentos")]
    public class DepartamentosController : ApiController
    {
        private readonly DepartamentosLogic departamentosLogic;

        // Constructor que inicializa la lógica de departamentos
        public DepartamentosController()
        {
            departamentosLogic = new DepartamentosLogic();
        }

        // Obtener todos los departamentos
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllDepartamentos()
        {
            List<Departamentos> departamentos = departamentosLogic.RetrieveAll();
            return Ok(departamentos);
        }

        // Obtener un departamento por su ID
        [HttpGet]
        [Route("{id:int}", Name = "GetDepartamentoById")]
        public IHttpActionResult GetDepartamentoById(int id)
        {
            Departamentos departamento = departamentosLogic.RetrieveById(id);
            if (departamento == null)
                return NotFound();  // Si no existe el departamento
            return Ok(departamento);
        }

        // Crear un nuevo departamento
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateDepartamento(Departamentos departamento)
        {
            if (departamento == null)
                return BadRequest("Invalid data.");  // Validación de entrada
            var createdDepartamento = departamentosLogic.Create(departamento);
            if (createdDepartamento != null)
            {
                return CreatedAtRoute("GetDepartamentoById", new { id = createdDepartamento.DepartamentoID }, createdDepartamento);
            }
            return Conflict();  // Si ya existe un departamento con el mismo nombre u otra condición
        }

        // Actualizar un departamento
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateDepartamento(int id, Departamentos departamento)
        {
            if (id != departamento.DepartamentoID)
                return BadRequest("ID mismatch.");
            bool success = departamentosLogic.Update(departamento);
            if (!success)
                return NotFound();  // Si no se encuentra el departamento para actualizar
            return Ok(departamento);
        }

        // Eliminar un departamento
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteDepartamento(int id)
        {
            bool success = departamentosLogic.Delete(id);
            if (!success)
                return NotFound();  // Si no se encuentra el departamento para eliminar
            return Ok();
        }
    }
}
