//using DAL;
//using Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BLL
//{
//    public class DepartamentosLogic
//    {
//        private readonly IRepository _repository;

//        public DepartamentosLogic(IRepository repository)
//        {
//            _repository = repository;
//        }

//        public List<Departamentos> GetAllDepartamentos()
//        {
//            return _repository.RetrieveAll<Departamentos>();
//        }

//        public Departamentos GetDepartamentoById(int id)
//        {
//            return _repository.Retrieve<Departamentos>(d => d.DepartamentoID == id);
//        }

//        public Departamentos CreateDepartamento(Departamentos departamento)
//        {
//            return _repository.Create(departamento);
//        }

//        public bool UpdateDepartamento(Departamentos departamento)
//        {
//            return _repository.Update(departamento);
//        }

//        public bool DeleteDepartamento(int id)
//        {
//            var departamento = GetDepartamentoById(id);
//            return departamento != null && _repository.Delete(departamento);
//        }
//    }
//}

using DAL;
using Entities;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class DepartamentosLogic
    {
        // Método para crear un departamento
        public Departamentos Create(Departamentos departamento)
        {
            Departamentos res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                // Comprobamos si el departamento ya existe por nombre
                Departamentos existingDepartamento = r.Retrieve<Departamentos>(d => d.Nombre == departamento.Nombre);
                if (existingDepartamento == null)
                {
                    res = r.Create(departamento);
                }
                else
                {
                    // Lógica si el departamento ya existe (si es necesario)
                    // Se podría devolver un mensaje o manejar el error aquí
                }
            }
            return res;
        }

        // Método para obtener un departamento por su ID
        public Departamentos RetrieveById(int id)
        {
            Departamentos res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.Retrieve<Departamentos>(d => d.DepartamentoID == id);
            }
            return res;
        }

        // Método para actualizar un departamento
        public bool Update(Departamentos departamentoToUpdate)
        {
            bool res = false;
            using (var r = RepositoryFactory.CreateRepository())
            {
                // Verificamos si ya existe un departamento con el mismo nombre pero con un ID diferente
                Departamentos existingDepartamento = r.Retrieve<Departamentos>(d =>
                    d.Nombre == departamentoToUpdate.Nombre && d.DepartamentoID != departamentoToUpdate.DepartamentoID);

                if (existingDepartamento == null)
                {
                    // Si no existe otro departamento con el mismo nombre, actualizamos el departamento
                    res = r.Update(departamentoToUpdate);
                }
                else
                {
                    // Lógica si el departamento con ese nombre ya existe (si es necesario)
                    // Aquí puedes manejar el caso si es necesario
                }
            }
            return res;
        }

        // Método para eliminar un departamento
        public bool Delete(int id)
        {
            bool res = false;
            var departamento = RetrieveById(id);
            if (departamento != null)
            {
                // Solo eliminamos si el departamento cumple con ciertas condiciones
                // Puedes agregar condiciones específicas, como verificar si está vacío o tiene alguna relación.
                using (var r = RepositoryFactory.CreateRepository())
                {
                    res = r.Delete(departamento);
                }
            }
            else
            {
                // Si el departamento no existe, puedes manejarlo de alguna forma
                Console.WriteLine($"Departamento con ID {id} no encontrado.");
            }
            return res;
        }

        // Método para obtener todos los departamentos
        public List<Departamentos> RetrieveAll()
        {
            List<Departamentos> res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.RetrieveAll<Departamentos>();
            }
            return res;
        }

        // Método para filtrar departamentos por nombre
        public List<Departamentos> Filter(string nombre)
        {
            List<Departamentos> res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.Filter<Departamentos>(d => d.Nombre == nombre);
            }
            return res;
        }
    }
}
