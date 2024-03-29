using AutoMapper;
using Eodg.MedicalTracker.Persistence;
using Eodg.MedicalTracker.Common;
using Eodg.MedicalTracker.Services.Data;
using Eodg.MedicalTracker.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Eodg.MedicalTracker.Tests.Services
{
    public class ServiceFixture
    {
        public ServiceFixture()
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection
                .AddDbContext<MedicalTrackerDbContext>(options =>
                {
                    options
                        .UseSqlite("DataSource=:memory:");
                })
                // TODO: Remove following when done checking against live database...
                // .AddDbContext<MedicalTrackerDbContext>(options => options.UseSqlServer("Server=localhost;Database=MedicalTracker;Trusted_Connection=True;"))
                
                .AutoRegisterDependencies(typeof(DataService<>))
                .AddAutoMapper(typeof(DomainDtoMappingProfile));

            ServiceProvider = serviceCollection.BuildServiceProvider();

            CreateDatabase();
            SeedData();
        }

        public ServiceProvider ServiceProvider { get; private set; }

        private void CreateDatabase()
        {
            var dbContext = ServiceProvider.GetService<MedicalTrackerDbContext>();

            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();
        }

        private void SeedData()
        {
            var dataUtilityService = ServiceProvider.GetService<IDataUtilityService>();

            dataUtilityService.SeedData();
        }
    }
}