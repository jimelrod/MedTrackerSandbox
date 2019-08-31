using AutoMapper;
using Eodg.MedicalTracker.Api.Authentication;
using Eodg.MedicalTracker.Api.Authorization;
using Eodg.MedicalTracker.Persistence;
using Eodg.MedicalTracker.Services;
using Eodg.MedicalTracker.Services.Interfaces;
using Eodg.MedicalTracker.Services.Mapping;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Eodg.MedicalTracker.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // TODO: Maybe document how this method works in regards to ENV variables...
            // Initialize Firebase App...
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.GetApplicationDefault(),
            });
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(DomainDtoMappingProfile));

            var schemeName = Configuration["Authentication:SchemeName"];
            var displayName = Configuration["Authentication:DisplayName"];
            services.AddFirebaseAuthentication(schemeName, displayName);

            services.AddAuthorization();

            services.AddDbContext<MedicalTrackerDbContext>(options =>
            {
                // TODO: Put conn string in env variable or something...
                //          Will be fine for local development...
                string connectionString = "Server=localhost;Database=MedicalTracker;Trusted_Connection=True;";
                options.UseSqlServer(connectionString);
            });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IAuthorizationHandler, MemberSpecificAuthorizationHandler>();

            // TODO: Figure out a smart way to map services in the DI container...
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IProfileService, ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
