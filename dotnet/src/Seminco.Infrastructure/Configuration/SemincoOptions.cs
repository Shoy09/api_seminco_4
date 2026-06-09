using System.ComponentModel.DataAnnotations;

namespace Seminco.Infrastructure.Configuration;

public sealed class DatabaseOptions
{
    public const string SectionName = "Database";
    [Required] public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 5432;
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string User { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;

    public string ToConnectionString() =>
        $"Host={Host};Port={Port};Database={Name};Username={User};Password={Password};";
}

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";
    [Required, MinLength(32)] public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = "Seminco.Api";
    public string Audience { get; set; } = "Seminco.Clients";
    public int ExpirationHours { get; set; } = 3;
}

public sealed class CloudinaryOptions
{
    public const string SectionName = "Cloudinary";
    [Required] public string CloudName { get; set; } = string.Empty;
    [Required] public string ApiKey { get; set; } = string.Empty;
    [Required] public string ApiSecret { get; set; } = string.Empty;
}

public sealed class HostingOptions
{
    public const string SectionName = "Hosting";
    public string[] AllowedOrigins { get; set; } = [];
}
