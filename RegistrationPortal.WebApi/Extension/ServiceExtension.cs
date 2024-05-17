using Microsoft.EntityFrameworkCore;
using RegistrationPortal.Application.MapInitializer;
using RegistrationPortal.Application.Services.Abstractions;
using RegistrationPortal.Application.Services.Implementations;
using RegistrationPortal.Infrastructure.DataContext;
using RegistrationPortal.Infrastructure.GenericRepository.IRepoBase;
using RegistrationPortal.Infrastructure.GenericRepository.RepositoryBase;
using System.Text.Json.Serialization;

namespace RegistrationPortal.WebApi.Extension
{
    public static class ServiceExtension
    {
        public static void ResolveDependencyInjectionContainer(this IServiceCollection services)
        {
            services.AddScoped<IProgramService, ProgramService>();
            services.AddScoped<ICandidateApplicationServices, CandidateApplicationServices>();
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        }
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var cosmosConfig = configuration.GetSection("CosmosConfig");
            services.AddDbContext<ProgramAppDbContext>(options =>
                options.UseCosmos(
                    configuration["CosmosConfig:accountEndpoint"], // Cosmos DB account endpoint
                    configuration["CosmosConfig:accountKey"],      // Cosmos DB account key
                    configuration["CosmosConfig:databaseName"]));  // Cosmos DB database name
        }
        public static void ConfigureController(this IServiceCollection services)
        {
            services.AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
                        .AddJsonOptions(options =>
                        {
                            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        }).AddXmlDataContractSerializerFormatters();
        }
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProfileMapping).Assembly);
        }
    }
}
