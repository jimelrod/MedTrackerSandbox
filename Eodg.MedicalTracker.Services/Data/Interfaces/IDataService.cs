using Eodg.MedicalTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services.Data.Interfaces
{

    public interface IDataService<T> where T : class, IEntity
    {
        void Add(T entity);
        Task AddAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        ICollection<T> Get();
        ICollection<T> Get(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> GetAsync();
        Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate);
        T GetSingle(int id);
        T GetSingle(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(int id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        Task UpdateAsync(T entity);
    }
}
