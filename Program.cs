using CaloriasApi.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using DotNetEnv; // ⬅️ Asegurate de importar esto si usás .env

var builder = WebApplication.CreateBuilder(args);

// ⬇️ 1. Cargar variables desde .env
Env.Load(); // Esto permite que lea DATABASE_URL desde el archivo .env

// ⬇️ 2. Puerto local (opcional si solo usás 5000 por default)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000"; // Cambié a 5000 (más estándar)
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");;

// ⬇️ 3. Servicios esenciales
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ⬇️ 4. Convertir DATABASE_URL al formato de conexión MySQL
string ConvertConnectionString(string rawUrl)
{
    if (string.IsNullOrEmpty(rawUrl))
        throw new InvalidOperationException("DATABASE_URL no está definido.");

    var uri = new Uri(rawUrl);
    var userInfo = uri.UserInfo.Split(':');
    return $"server={uri.Host};port={uri.Port};database={uri.AbsolutePath.TrimStart('/')};user={userInfo[0]};password={userInfo[1]}";
}

var rawUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
var connectionString = ConvertConnectionString(rawUrl);

// ⬇️ 5. Registrar DbContext con la cadena de Railway
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// ⬇️ 6. Swagger solo en desarrollo (bien así)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ⬇️ 7. Middlewares
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "API Calorías funcionando 🚀");

app.Run();
