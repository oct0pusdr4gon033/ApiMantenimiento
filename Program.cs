using ApiMantenimiento.Data.Context;
using ApiMantenimiento.Repositories;
// Agrega los usings de tus interfaces y clases
using ApiMantenimiento.Repositories.Interfaces;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using ApiMantenimiento.Repositories.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using ApiMantenimiento.Services.MFlota; // O la carpeta donde esté tu AreaOperacionService
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", app =>
    {
        app.AllowAnyOrigin()   // Permite que Angular (o cualquier otro) se conecte
           .AllowAnyHeader()   // Permite enviar JSON
           .AllowAnyMethod();  // Permite GET, POST, PUT, DELETE
    });
});
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddOpenApi();

// ==========================================
// CONFIGURAR JWT AUTHENTICATION
// ==========================================
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("Key");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
        ValidateAudience = true,
        ValidAudience = jwtSettings.GetValue<string>("Audience"),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// ==========================================
// 1. REGISTRAR AUTOMAPPER
// ==========================================
// Esto escanea tu proyecto y carga automáticamente tu MFlotaMappingProfile
builder.Services.AddAutoMapper(config => { }, typeof(Program));

// 2. INYECCIÓN DE DEPENDENCIAS AUTOMÁTICA (CON SCRUTOR)
// ==========================================
builder.Services.Scan(selector => selector    // Le decimos que busque en todo nuestro proyecto actual
    .FromAssembliesOf(typeof(Program))

    // 1. Escanea y registra TODOS los Repositorios automáticamente
    .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Repository")))
    .AsImplementedInterfaces()
    .WithScopedLifetime()

    // 2. Escanea y registra TODOS los Servicios automáticamente
    .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Service")))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

builder.Services.AddDbContext<MantenimientoDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConexionSQL")
    )
);

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                QueueLimit = 0,
                Window = TimeSpan.FromMinutes(1)
            }));
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        await context.HttpContext.Response.WriteAsync("Demasiadas peticiones. Por favor, intentelo mas tarde.", token);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRateLimiter();

var webRoot = app.Environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
if (!Directory.Exists(webRoot)) Directory.CreateDirectory(webRoot);
app.UseStaticFiles();

app.UseCors("PermitirFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
