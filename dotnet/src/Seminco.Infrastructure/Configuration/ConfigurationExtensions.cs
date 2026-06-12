using Microsoft.Extensions.Configuration;

namespace Seminco.Infrastructure.Configuration;

public static class ConfigurationExtensions
{
    public static DatabaseOptions GetDatabaseOptions(this IConfiguration configuration) => new()
    {
        Host = Get(configuration, "Database:Host", "DB_HOST"),
        Port = int.TryParse(Get(configuration, "Database:Port", "DB_PORT"), out var port) ? port : 5432,
        Name = Get(configuration, "Database:Name", "DB_NAME"),
        User = Get(configuration, "Database:User", "DB_USER"),
        Password = Get(configuration, "Database:Password", "DB_PASSWORD"),
        Schema = Get(configuration, "Database:Schema", "DB_SCHEMA")
    };

    public static JwtOptions GetJwtOptions(this IConfiguration configuration) => new()
    {
        Secret = Get(configuration, "Jwt:Secret", "JWT_SECRET"),
        Issuer = Get(configuration, "Jwt:Issuer", null, "Seminco.Api"),
        Audience = Get(configuration, "Jwt:Audience", null, "Seminco.Clients"),
        ExpirationHours = int.TryParse(Get(configuration, "Jwt:ExpirationHours", null, "3"), out var hours) ? hours : 3
    };

    public static CloudinaryOptions GetCloudinaryOptions(this IConfiguration configuration) => new()
    {
        CloudName = Get(configuration, "Cloudinary:CloudName", "CLOUDINARY_CLOUD_NAME"),
        ApiKey = Get(configuration, "Cloudinary:ApiKey", "CLOUDINARY_API_KEY"),
        ApiSecret = Get(configuration, "Cloudinary:ApiSecret", "CLOUDINARY_API_SECRET")
    };

    private static string Get(IConfiguration configuration, string sectionKey, string? envKey, string fallback = "") =>
        configuration[sectionKey]
        ?? (envKey is null ? null : configuration[envKey])
        ?? fallback;
}
