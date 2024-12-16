using System;
using System.Collections.Generic;
using System.Data.Entity; // por si acaso se añadio esto en dbcontext
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EFRepository : IRepository
    {
        DbContext _dbContext;

        public EFRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Configuration.LazyLoadingEnabled = false;
        }

        public TEntity Create<TEntity>(TEntity toCreate) where TEntity : class
        {
            TEntity result = default;
            try
            {
                _dbContext.Set<TEntity>().Add(toCreate);
                _dbContext.SaveChanges();
                result = toCreate;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                // Captura detalles de los errores de validación
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(validationErrors => validationErrors.ValidationErrors)
                    .Select(validationError => $"Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = $"Validation failed for entity of type {typeof(TEntity).Name}: {fullErrorMessage}";

                // Lanza una nueva excepción con detalles adicionales
                throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception ex)
            {
                // Manejo genérico de otras excepciones si es necesario
                throw new Exception("An unexpected error occurred while creating the entity.", ex);
            }
            return result;
        }


        public bool Delete<TEntity>(TEntity toDelete) where TEntity : class
        {
            bool result = false;
            try
            {
                _dbContext.Entry<TEntity>(toDelete).State = EntityState.Deleted;
                result = _dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }

        public List<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            List<TEntity> result = null;
            try
            {
                result = _dbContext.Set<TEntity>().Where(criteria).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public TEntity Retrieve<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            TEntity result = null;
            try
            {
                result = _dbContext.Set<TEntity>().FirstOrDefault(criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Update<TEntity>(TEntity toUpdate) where TEntity : class
        {
            bool result = false;
            try
            {
                _dbContext.Entry<TEntity>(toUpdate).State = EntityState.Modified;
                result = _dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        // Nuevo método para recuperar todos los registros
        public List<TEntity> RetrieveAll<TEntity>() where TEntity : class
        {
            List<TEntity> result = null;
            try
            {
                result = _dbContext.Set<TEntity>().ToList(); // Obtiene todos los registros de la tabla
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

    }
}