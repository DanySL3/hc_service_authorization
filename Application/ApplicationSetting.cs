using Application.Interfaces.Externals;
using Application.Usecases;
using Domain.Commons;
using Domain.Interfaces;
using Domain.Interfaces.Getting;
using Domain.Interfaces.Method;
using InfrastructureCoreDatabase;
using InfrastructureCoreDatabase.DataAccess.Gettings;
using InfrastructureCoreDatabase.DataAccess.Methods;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationSetting
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {

            services.AddInfrastructureCoreServices(configuration, isDevelopment);

            //helpers

            services.AddScoped<IHelperCommon, HelperCommon>();


            //Domian Interface and Infrastucture DataAccess

            services.AddTransient<IMenuGettingInfrastructure, MenuGetting>();
            services.AddTransient<ISistemaGettingInfrastructure, SistemaGetting>();
            services.AddTransient<IPerfilGettingInfrastructure, PerfilGetting>();
            services.AddTransient<IPerfilInfrastructure, PerfilMethod>();
            services.AddTransient<IAgenciaGettingInfrastructure, AgenciaGetting>();
            services.AddTransient<IUsuarioInfrastructure, UsuarioMethod>();
            services.AddTransient<IUsuarioGettingInfrastructure, UsuarioGetting>();

            //Interface Application
            services.AddTransient<IMenuApplication, MenuCase>();
            services.AddTransient<ISistemaApplication, SistemaCase>();
            services.AddTransient<IUsuarioApplication, UsuarioCase>();
            services.AddTransient<IPerfilApplication, PerfilCase>();
            services.AddTransient<IAgenciaApplication, AgenciaCase>();

            return services;
        }
    }
}
