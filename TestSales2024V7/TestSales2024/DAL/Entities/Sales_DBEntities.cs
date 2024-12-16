//using System.Data.Entity;

//namespace Entities
//{
//    internal class Sales_DBEntities : DbContext
//    {
//    }
//}

using System.Data.Entity;

namespace Entities
{
    public class Sales_DBEntities : DbContext
    {
        public DbSet<Departamentos> Departamentos { get; set; } // Asegúrate de que esta propiedad esté definida

        // Si tienes otras entidades, agrégalas también
        // public DbSet<OtroModelo> OtrosModelos { get; set; }

        // Si deseas usar configuraciones personalizadas con Fluent API, puedes hacerlo en OnModelCreating
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Definir la clave primaria de la entidad Departamentos
            modelBuilder.Entity<Departamentos>()
                .HasKey(d => d.DepartamentoID);  // DepartamentoID es la clave primaria
        }
    }
}
