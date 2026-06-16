using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Seminco.Infrastructure;
using Seminco.Infrastructure.Configuration;
using Seminco.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
var jwt = builder.Configuration.GetJwtOptions();
var configuredOrigins = builder.Configuration
    .GetSection(HostingOptions.SectionName)
    .Get<HostingOptions>()?
    .AllowedOrigins ?? [];
var corsOrigins = configuredOrigins
    .Concat([
        "http://localhost:4200",
        "http://127.0.0.1:4200",
        "https://localhost:4200",
        "https://127.0.0.1:4200"
    ])
    .Where(origin => !string.IsNullOrWhiteSpace(origin))
    .Select(origin => origin.Trim())
    .Distinct(StringComparer.OrdinalIgnoreCase)
    .ToArray();

builder.Services.AddSemincoInfrastructure(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState)
    { Title = "Validation failed", Status = StatusCodes.Status400BadRequest });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("SemincoCors", policy =>
    {
        policy
            .WithOrigins(corsOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret)),
        ValidateIssuer = true,
        ValidIssuer = jwt.Issuer,
        ValidateAudience = true,
        ValidAudience = jwt.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1)
    };
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return context.Response.WriteAsJsonAsync(new ProblemDetails { Title = "Unauthorized", Status = StatusCodes.Status401Unauthorized, Detail = "A valid bearer token is required." });
        }
    };
});
builder.Services.AddAuthorization();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks().AddDbContextCheck<SemincoDbContext>("postgresql", tags: ["ready"]);

const string DocsHtml = """
<!doctype html><html lang="en"><head><meta charset="utf-8"><title>Seminco API Docs</title></head>
<body><h1>Seminco API</h1><p>OpenAPI document: <a href="/docs/openapi/v1.json">/docs/openapi/v1.json</a></p></body></html>
""";

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseCors("SemincoCors");
app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi("/docs/openapi/{documentName}.json").AllowAnonymous();
app.MapGet("/docs", () => Results.Content(DocsHtml, "text/html")).AllowAnonymous();
app.MapGet("/api/diagnostico", () => Results.Ok(new { service = "Seminco.Api", runtime = ".NET 10", status = "running" })).AllowAnonymous();
app.MapControllers().RequireAuthorization();
app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false }).AllowAnonymous();
app.MapHealthChecks("/health/ready", new HealthCheckOptions { Predicate = check => check.Tags.Contains("ready") }).AllowAnonymous();
app.MapFallback("/api/{**path}", () => Results.Problem(title: "Endpoint not found", statusCode: StatusCodes.Status404NotFound)).AllowAnonymous();

app.Run();
