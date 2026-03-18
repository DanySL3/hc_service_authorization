using Domain.Commons;
using InfrastructureCoreDatabase.EntityFramework.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureCoreDatabase
{
    public static class CoreDatabaseSetting
    {
        public static IServiceCollection AddInfrastructureCoreServices(this IServiceCollection services, IConfiguration configuration, bool lDevelopment)
        {

            services.AddDbContext<EntityFrameworkContext>(options =>
            {
                if (lDevelopment)
                {
                    options.UseNpgsql(configuration.GetConnectionString("DB_Authorization"));
                }
                else
                {
                    options.UseNpgsql(ConnectionCommon.CreateSqlConnection($"Host={Environment.GetEnvironmentVariable("DB_AUTHORIZATION_SERVER")};" +
                                                                            $"Port={Environment.GetEnvironmentVariable("DB_AUTHORIZATION_PORT")};" +
                                                                            $"Database={Environment.GetEnvironmentVariable("DB_AUTHORIZATION_DATABASE")};" +
                                                                            $"Username={Environment.GetEnvironmentVariable("DB_AUTHORIZATION_USER")};" +
                                                                            $"Password={Environment.GetEnvironmentVariable("DB_AUTHORIZATION_PASSWD")};"));
                }
            });
            return services;
        }
    }
}