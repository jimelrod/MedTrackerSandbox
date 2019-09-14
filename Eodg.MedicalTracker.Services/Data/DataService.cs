using Eodg.MedicalTracker.Domain.Interfaces;
using Eodg.MedicalTracker.Persistence;
using Eodg.MedicalTracker.Services.Data.Interfaces;
using Eodg.MedicalTracker.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services.Data
{
    public class DataService<T> : IDataService<T> where
        T : class, IEntity
    {

        public DataService(MedicalTrackerDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected MedicalTrackerDbContext DbContext { get; private set; }

        #region Get

        public virtual T Get(int id)
        {
            T entity;

            try
            {
                entity = DbContext.Set<T>().Single(e => e.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. Id: {id}. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entity;
        }

        public virtual IEnumerable<T> Get()
        {
            return
                DbContext
                    .Set<T>()
                    .ToList();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return
                DbContext
                    .Set<T>()
                    .Where(predicate)
                    .ToList();
        }

        public virtual async Task<T> GetAsync(int id)
        {
            T entity;

            try
            {
                entity = await DbContext.Set<T>().SingleAsync(e => e.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. Id: {id}. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetAsync()
        {
            return
                await
                    DbContext
                        .Set<T>()
                        .ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return
                await
                    DbContext
                        .Set<T>()
                        .Where(predicate)
                        .ToListAsync();
        }

        #endregion

        public virtual void Add(T entity)
        {
            try
            {
                DbContext.Set<T>().Add(entity);

                DbContext.SaveChanges();
            }
            catch (Exception ex) when (
                ex is InvalidOperationException ||
                ex is DbUpdateException)
            {
                DbContext.ResetContext();

                throw new ResourceNotAddedException(typeof(T), ex);
            }
        }

        public virtual async Task AddAsync(T entity)
        {
            try
            {
                DbContext.Set<T>().Add(entity);

                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex) when (
                ex is InvalidOperationException ||
                ex is DbUpdateException)
            {
                DbContext.ResetContext();

                throw new ResourceNotAddedException(typeof(T), ex);
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                DbContext.Update(entity);

                DbContext.SaveChanges();
            }
            catch (Exception ex) when (
                ex is InvalidOperationException ||
                ex is DbUpdateException)
            {
                DbContext.ResetContext();

                throw new ResourceNotUpdatedException(entity.Id, typeof(T), ex);
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            try
            {
                DbContext.Update(entity);

                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex) when (
                ex is InvalidOperationException ||
                ex is DbUpdateException)
            {
                DbContext.ResetContext();

                throw new ResourceNotUpdatedException(entity.Id, typeof(T), ex);
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                DbContext.Remove(entity);

                DbContext.SaveChanges();
            }
            catch (Exception ex) when (
                ex is InvalidOperationException ||
                ex is DbUpdateException)
            {
                DbContext.ResetContext();

                throw new ResourceNotDeletedException(entity.Id, typeof(T), ex);
            }
        }

        public virtual async Task DeleteAsync(T entity)
        {
            try
            {
                DbContext.Remove(entity);

                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex) when (
                ex is InvalidOperationException ||
                ex is DbUpdateException)
            {
                DbContext.ResetContext();

                throw new ResourceNotDeletedException(entity.Id, typeof(T), ex);
            }
        }
    }
}