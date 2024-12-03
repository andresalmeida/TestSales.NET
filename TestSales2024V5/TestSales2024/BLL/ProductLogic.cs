using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductLogic
    {
        public Products Create(Products products)
        {
            Products res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                Products result = r.Retrieve<Products>(p => p.ProductName == products.ProductName);
                if (result == null)
                {
                    res = r.Create(products);
                }
                else
                {
                    //Comentarios
                }
                return res;
            }

        }
        public Products RetrieveById(int id)
        {
            Products res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.Retrieve<Products>(p => p.ProductID == id);
            }
            return res;
        }

        //public bool Update(Products productToUpdate) { 
        //    bool res = false;
        //    using (var r = RepositoryFactory.CreateRepository())
        //    {
        //        //Validar el nombre de productos
        //        Products temp = r.Retrieve<Products>
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
        public bool Update(Products productToUpdate)
        {
            bool res = false;
            using (var r = RepositoryFactory.CreateRepository())
            {
                // Verificar si ya existe un producto con el mismo nombre pero con un ProductID diferente
                Products existingProduct = r.Retrieve<Products>(
                    p => p.ProductName == productToUpdate.ProductName && p.ProductID != productToUpdate.ProductID);

                if (existingProduct == null)
                {
                    // No existe otro producto con el mismo nombre, proceder con la actualización
                    res = r.Update(productToUpdate);
                }
                else
                {

                }
            }
            return res;
        }

        public bool Delete(int id)
        {
            bool res = false;
            var product = RetrieveById(id);
            if (product != null)
            {
                if (product.UnitsInStock == 0)
                {
                    using (var r = RepositoryFactory.CreateRepository())
                    {
                        res = r.Delete(product);
                    }
                }
                else
                {
                    Console.WriteLine("Cannot delete product. UnitsInStock is greater than 0.");
                }
            }
            else
            {
                Console.WriteLine($"Product with ID {id} not found.");
            }
            return res;
        }


        //buscar por nombre
        public List<Products> Filter(string name)
        {
            List<Products> res = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                res = repository.Filter<Products>(p => p.ProductName == name);
            }
            return res;
        }

        //Mostrar todo
        public List<Products> RetrieveAll()
        {
            List<Products> res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.RetrieveAll<Products>();
            }
            return res;
        }



    }
}