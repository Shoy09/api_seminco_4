using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Seminco.Infrastructure.Configuration;
using Seminco.Infrastructure.Persistence;
using Seminco.Infrastructure.Catalogs.Services;
using Seminco.Application.Auth;
using Seminco.Application.Catalogs;
using Seminco.Application.Operaciones;
using Seminco.Application.Planes;
using Seminco.Application.Users;
using Seminco.Infrastructure.Auth;
using Seminco.Infrastructure.Operaciones;
using Seminco.Infrastructure.Planes;
using Seminco.Infrastructure.Users;

namespace Seminco.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSemincoInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DatabaseOptions>()
            .Configure(options => Copy(configuration.GetDatabaseOptions(), options))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<JwtOptions>()
            .Configure(options => Copy(configuration.GetJwtOptions(), options))
            .ValidateDataAnnotations()
            .Validate(options => options.ExpirationHours > 0, "JWT expiration must be positive.")
            .ValidateOnStart();

        services.AddOptions<CloudinaryOptions>()
            .Configure(options => Copy(configuration.GetCloudinaryOptions(), options))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<SemincoDbContext>((provider, options) =>
        {
            var database = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.UseNpgsql(database.ToConnectionString());
        });

        services.AddScoped<LoginService>();
        services.AddScoped<RegisterService>();
        services.AddScoped<UserProfileService>();
        services.AddScoped<IUserAuthRepository, UserRepository>();
        services.AddScoped<IUserProfileRepository, UserRepository>();
        services.AddSingleton<IPasswordVerifier, BCryptPasswordVerifier>();
        services.AddSingleton<IJwtTokenIssuer, JwtTokenIssuer>();
        services.AddScoped<IOperacionService, OperacionService>();

        RegisterCatalogServices(services);
        RegisterPlanServices(services);

        return services;
    }

    private static void RegisterPlanServices(IServiceCollection services)
    {
        services.AddScoped<IPlanService<PlanMensualDto>, PlanMensualService>();
        services.AddScoped<IPlanService<PlanMetrajeDto>, PlanMetrajeService>();
        services.AddScoped<IPlanService<PlanProduccionDto>, PlanProduccionService>();
        services.AddScoped<IFechaPlanMensualService, FechaPlanMensualService>();
    }

    private static void RegisterCatalogServices(IServiceCollection services)
    {
        services.AddScoped<ICatalogService<EquipoDto>, EquipoService>();
        services.AddScoped<ICatalogService<EstadoDto>, EstadoService>();
        services.AddScoped<ICatalogService<TipoPerforacionDto>, TipoPerforacionService>();
        services.AddScoped<ICatalogService<TipoEquipoDto>, TipoEquipoService>();
        services.AddScoped<ICatalogService<CheckListItemDto>, CheckListItemService>();
        services.AddScoped<ICatalogService<ChecklistTelemandoDto>, ChecklistTelemandoService>();
        services.AddScoped<ICatalogService<SeccionDto>, SeccionService>();
        services.AddScoped<ICatalogService<LongitudBarraDto>, LongitudBarraService>();
        services.AddScoped<ICatalogService<PernoDto>, PernoService>();
        services.AddScoped<ICatalogService<MallaDto>, MallaService>();
        services.AddScoped<ICatalogService<OrigenDestinoDto>, OrigenDestinoService>();
        services.AddScoped<ICatalogService<AccesorioDto>, AccesorioService>();
        services.AddScoped<ICatalogService<ExplosivoDto>, ExplosivoService>();
        services.AddScoped<ICatalogService<ExplosivoUniDto>, ExplosivoUniService>();
        services.AddScoped<ICatalogService<NumeroRetardoDto>, NumeroRetardoService>();
    }

    private static void Copy(DatabaseOptions source, DatabaseOptions target) =>
        (target.Host, target.Port, target.Name, target.User, target.Password) =
        (source.Host, source.Port, source.Name, source.User, source.Password);

    private static void Copy(JwtOptions source, JwtOptions target) =>
        (target.Secret, target.Issuer, target.Audience, target.ExpirationHours) =
        (source.Secret, source.Issuer, source.Audience, source.ExpirationHours);

    private static void Copy(CloudinaryOptions source, CloudinaryOptions target) =>
        (target.CloudName, target.ApiKey, target.ApiSecret) =
        (source.CloudName, source.ApiKey, source.ApiSecret);
}
