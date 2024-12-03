using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoriesLogic
    {
        public Categories Create(Categories categories)
        {
            Categories res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                Categories result = r.Retrieve<Categories>(p => p.CategoryName == categories.CategoryName);
                if (result == null)
                {
                    res = r.Create(categories);
                }
                else
                {
                    //Comentarios
                }
                return res;
            }

        }
        public Categories RetrieveById(int id)
        {
            Categories res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.Retrieve<Categories>(p => p.CategoryID == id);
            }
            return res;
        }

        //public bool Update(Categories productToUpdate) { 
        //    bool res = false;
        //    using (var r = RepositoryFactory.CreateRepository())
        //    {
        //        //Validar el nombre de productos
        //        Categories temp = r.Retrieve<Categories>
        //            (p => p.ProductName == productToUpdate.ProductName && p.ProductID != productToUpdate.ProductID);
        //        if (temp != null)
        //        {
        //            //No existe
        //            res = r.Update(productToUpdate);
        //        }
        //        else
        //        {
        //            //Logica para mostrar que no se pudo modificar
        //        }
        //    }
        //    return res;
        //}
        public bool Update(Categories productToUpdate)
        {
            bool res = false;
            using (var r = RepositoryFactory.CreateRepository())
            {
                // Verificar si ya existe un producto con el mismo nombre pero con un ProductID diferente
                Categories existingProduct = r.Retrieve<Categories>(
                    p => p.CategoryName == productToUpdate.CategoryName && p.CategoryID != productToUpdate.CategoryID);

                if (existingProduct == null)
                {
                    // No existe otro producto con el mismo nombre, proceder con la actualización
                    res = r.Update(productToUpdate);
                }
                else
                {
                    // Producto con el mismo nombre ya existe, no se puede actualizar
                    // Aquí podrías añadir lógica para manejar el error (ej. mensaje de error o flag)
                }
            }
            return res;
        }

        public bool Delete(int id)
        {
            bool res = false;
            var product = RetrieveById(id); // Recuperamos el producto por su ID
            if (product != null)
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    res = r.Delete(product); // Eliminamos el producto directamente
                }
            }
            return res; // Retornamos si la eliminación fue exitosa o no
        }


        //buscar por nombre
        public List<Categories> Filter(string name)
        {
            List<Categories> res = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                res = repository.Filter<Categories>(p => p.CategoryName == name);
            }
            return res;
        }

        //Mostrar todo
        public List<Categories> RetrieveAll()
        {
            List<Categories> res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.RetrieveAll<Categories>();
            }
            return res;
        }



    }
}