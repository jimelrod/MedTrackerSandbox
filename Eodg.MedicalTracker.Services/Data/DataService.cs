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
        private readonly MedicalTrackerDbContext _dbContext;

        public DataService(MedicalTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Get

        public T GetSingle(int id)
        {
            T entity;

            try
            {
                entity = _dbContext.Set<T>().Single(e => e.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. Id: {id}. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entity;
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            T entity;

            try
            {
                entity = _dbContext.Set<T>().Single(predicate);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entity;
        }

        public ICollection<T> Get()
        {
            ICollection<T> entities;

            try
            {
                entities = _dbContext.Set<T>().ToList();
            }
            // TODO: Figure out if there will actually be an exception thrown...
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entities;
        }

        public ICollection<T> Get(Expression<Func<T, bool>> predicate)
        {
            ICollection<T> entities;

            try
            {
                entities = _dbContext.Set<T>().Where(predicate).ToList();
            }
            // TODO: Figure out if there will actually be an exception thrown...
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entities;
        }

        public async Task<T> GetSingleAsync(int id)
        {
            T entity;

            try
            {
                entity = await _dbContext.Set<T>().SingleAsync(e => e.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. Id: {id}. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entity;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            T entity;

            try
            {
                entity = await _dbContext.Set<T>().SingleAsync(predicate);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entity;
        }

        public async Task<ICollection<T>> GetAsync()
        {
            ICollection<T> entities;

            try
            {
                entities = await _dbContext.Set<T>().ToListAsync();
            }
            // TODO: Figure out if there will actually be an exception thrown...
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entities;
        }

        public async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            ICollection<T> entities;

            try
            {
                entities = await _dbContext.Set<T>().Where(predicate).ToListAsync();
            }
            // TODO: Figure out if there will actually be an exception thrown...
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entities;
        }

        #endregion

        public void Add(T entity)
        {
            try
            {
                _dbContext.Set<T>().Add(entity);

                _dbContext.SaveChanges();
            }
            catch (Exception ex) when (
                ex is InvalidOperationException ||
                ex is DbUpdateException)
            {
                _dbContext.ResetContext();

                throw new ResourceNotAddedException(typeof(T), ex);
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                _dbContext.Set<T>().Add(entity);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) when (
                ex is InvalidOperationException ||
                ex is DbUpdateException)
            {
                _dbContext.ResetContext();

                throw new ResourceNotAddedException(typeof(T), ex);
            }
        }

        public void Update(T entity)
        {
            try
            {
                _dbContext.Update(entity);

                _dbContext.SaveChanges();
            }
            catch (Exception ex) when (
                ex is InvalidOperationException ||
                ex is DbUpdateException)
            {
                _dbContext.ResetContext();

                throw new ResourceNotUpdatedException(entity.Id, typeof(T), ex);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Update(entity);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) when (
                ex is InvalidOperationException ||
                ex is DbUpdateException)
            {
                _dbContext.ResetContext();

                throw new ResourceNotUpdatedException(entity.Id, typeof(T), ex);
            }
        }

        public void Delete(T entity)
        {
            try
            {
                _dbContext.Remove(entity);

                _dbContext.SaveChanges();
            }
            catch (Exception ex) when (
                ex is InvalidOperationException ||
                ex is DbUpdateException)
            {
                _dbContext.ResetContext();

                throw new ResourceNotDeletedException(entity.Id, typeof(T), ex);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                _dbContext.Remove(entity);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) when (
                ex is InvalidOperationException ||
                ex is DbUpdateException)
            {
                _dbContext.ResetContext();

                throw new ResourceNotDeletedException(entity.Id, typeof(T), ex);
            }
        }
    }
}