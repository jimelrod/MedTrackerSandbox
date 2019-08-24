using AutoMapper;
using Eodg.MedicalTracker.Persistence;
using Eodg.MedicalTracker.Services;
using Eodg.MedicalTracker.Services.Interfaces;
using Eodg.MedicalTracker.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Eodg.MedicalTracker.Tests.Services
{
    public class ServiceFixture
    {
        public ServiceFixture()
        {
            var serviceCollection = new ServiceCollection();
            // serviceCollection
            //     .AddDbContext<MedicalTrackerDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()),
            //         ServiceLifetime.Transient);
            serviceCollection
                .AddDbContext<MedicalTrackerDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()))
                .AddScoped<IMemberService, MemberService>()
                .AddScoped<IProfileService, ProfileService>()
                .AddAutoMapper(typeof(DomainDtoMappingProfile));

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}