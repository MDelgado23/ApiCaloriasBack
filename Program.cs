using CaloriasApi.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; 

var builder = WebApplication.CreateBuilder(args);


var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
builder.Services.AddCors();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string ConvertConnectionString(string rawUrl)
{
    var uri = new Uri(rawUrl);
    var userInfo = uri.UserInfo.Split(':');
    return $"server={uri.Host};port={uri.Port};database={uri.AbsolutePath.TrimStart('/')};user={userInfo[0]};password={userInfo[1]}";


}

var rawUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
var connectionString = ConvertConnectionString(rawUrl);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors(policy => 
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.UseAuthorization();
app.MapControllers();



// Crear la base de datos si no existe
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     db.Database.EnsureCreated();
// }

app.Run();
