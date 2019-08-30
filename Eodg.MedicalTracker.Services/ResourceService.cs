using AutoMapper;
using Eodg.MedicalTracker.Domain.Interfaces;
using Eodg.MedicalTracker.Persistence;
using Eodg.MedicalTracker.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services
{
    public abstract class ResourceService<T> where
        T : class, IEntity
    {
        public ResourceService(MedicalTrackerDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        protected MedicalTrackerDbContext DbContext { get; private set; }
        protected IMapper Mapper { get; private set; }

        protected T GetEntity(int id)
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

        protected T GetEntity(Expression<Func<T, bool>> predicate)
        {
            T entity;

            try
            {
                entity = DbContext.Set<T>().Single(predicate);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entity;
        }

        protected ICollection<T> GetEntities()
        {
            ICollection<T> entities;

            try
            {
                entities = DbContext.Set<T>().ToList();
            }
            // TODO: Figure out if there will actually be an exception thrown...
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entities;
        }

        protected ICollection<T> GetEntities(Expression<Func<T, bool>> predicate)
        {
            ICollection<T> entities;

            try
            {
                entities = DbContext.Set<T>().Where(predicate).ToList();
            }
            // TODO: Figure out if there will actually be an exception thrown...
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entities;
        }

        protected async Task<T> GetEntityAsync(int id)
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

        protected async Task<T> GetEntityAsync(Expression<Func<T, bool>> predicate)
        {
            T entity;

            try
            {
                entity = await DbContext.Set<T>().SingleAsync(predicate);
            }
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entity;
        }

        protected async Task<ICollection<T>> GetEntitiesAsync()
        {
            ICollection<T> entities;

            try
            {
                entities = await DbContext.Set<T>().ToListAsync();
            }
            // TODO: Figure out if there will actually be an exception thrown...
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entities;
        }

        protected async Task<ICollection<T>> GetEntitiesAsync(Expression<Func<T, bool>> predicate)
        {
            ICollection<T> entities;

            try
            {
                entities = await DbContext.Set<T>().Where(predicate).ToListAsync();
            }
            // TODO: Figure out if there will actually be an exception thrown...
            catch (InvalidOperationException ex)
            {
                var message = $"{typeof(T)} not found. See InnerException for details...";

                throw new ResourceNotFoundException(message, ex);
            }

            return entities;
        }

        protected T AddEntity(T entity)
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

            return entity;
        }

        protected async Task<T> AddEntityAsync(T entity)
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

            return entity;
        }

        protected T UpdateEntity(T entity)
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

            return entity;
        }

        protected async Task<T> UpdateEntityAsync(T entity)
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

            return entity;
        }

        protected void DeleteEntity(T entity)
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

        protected async Task DeleteEntityAsync(T entity)
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