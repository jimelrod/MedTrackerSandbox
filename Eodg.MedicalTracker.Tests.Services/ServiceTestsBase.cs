using Eodg.MedicalTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Eodg.MedicalTracker.Tests.Services
{
    public abstract class ServiceTestsBase
    {
        public ServiceTestsBase(ServiceFixture serviceFixture)
        {
            ServiceFixture = serviceFixture;
            DbContext = GetService<MedicalTrackerDbContext>();
        }

        protected ServiceFixture ServiceFixture { get; private set; }
        protected MedicalTrackerDbContext DbContext { get; private set; }

        protected T GetService<T>()
        {
            return ServiceFixture.ServiceProvider.GetService<T>();
        }

        protected void Detach<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            var entity = DbContext.Set<T>().Single(predicate);
            DbContext.Entry(entity).State = EntityState.Detached;
        }
    }
}