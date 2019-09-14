using Eodg.MedicalTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services.Data.Interfaces
{
    public interface IProfileDataService : IDataService<Profile>
    {
        new Profile Get(int id);
        new Task<Profile> GetAsync(int id);
        new IEnumerable<Profile> Get(Expression<Func<Profile, bool>> predicate);
        new Task<IEnumerable<Profile>> GetAsync(Expression<Func<Profile, bool>> predicate);
        IEnumerable<Profile> Get(string firebaseId);
        Task<IEnumerable<Profile>> GetAsync(string firebaseId);
    }
}