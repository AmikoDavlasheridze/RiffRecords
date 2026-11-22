using Microsoft.OpenApi;
using RiffRecords.Application;
using RiffRecords.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RiffRecords.Api.Security;
using RiffRecords.Application.Options;
using RiffRecords.Application.Services;
using RiffRecords.Infrastructure.Persistence;



var builder = WebApplication.CreateBuilder(args);

var signingKeyBase64 = builder.Configuration["Jwt:SigningKey"]
    ?? throw new InvalidOperationException("Signing key is not configured.");

var signingKey = new SymmetricSecurityKey(
    Convert.FromBase64String(signingKeyBase64));

var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("Jwt:Issuer is missing.");

var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException("Jwt:Audience is missing.");

builder.Services.AddSingleton(new JwtTokenService(
    new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
    jwtIssuer,
    jwtAudience));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            IssuerSigningKey = signingKey,
            ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateLifetime = true,
            RoleClaimType = "role",
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddOpenApi(options =>
{
    options.AddOperationTransformer((operation, _, _) =>
    {
        foreach (var parameter in operation.Parameters ?? [])
        {
            if (parameter.Schema is OpenApiSchema { Format: "int32" } schema &&
                schema.Type == (JsonSchemaType.Integer | JsonSchemaType.String))
            {
                schema.Type = JsonSchemaType.Integer;
                schema.Pattern = null;
            }
        }

        return Task.CompletedTask;
    });
});

var app = builder.Build();

if (app.Configuration.GetValue<bool>("Database:MigrateOnStartup"))
    await app.Services.MigrateDatabaseAsync();

var seed = app.Configuration.GetSection(AdminSeedOptions.SectionName).Get<AdminSeedOptions>();
if(string.IsNullOrWhiteSpace(seed?.Email) || string.IsNullOrWhiteSpace(seed?.Password))
{
    app.Logger.LogWarning("AdminSeed not configured; skipping admin seeding");
}
else
{
    using var scope = app.Services.CreateScope();
    await scope.ServiceProvider.GetRequiredService<AdminSeeder>().SeedAsync(seed);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/openapi/v1.json", "RiffRecords API"));
}


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();