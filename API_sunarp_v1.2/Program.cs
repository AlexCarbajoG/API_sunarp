using API_sunarp.Data;
using API_sunarp.Data.Dependencias;
using API_sunarp.Data.Repositories;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyApp",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:3000") // Cambiar según sea necesario
                         .AllowAnyHeader()
                         .AllowAnyMethod();
        });
});


var mySQLConfiguration = new API_sunarp.Data.MySqlConfiguration(builder.Configuration.GetConnectionString("MySqlConnection"));
builder.Services.AddSingleton(mySQLConfiguration);

builder.Services.AddScoped<API_sunarp_repository, Sunarp_repository>();
builder.Services.AddScoped<API_empleadoRepository, EmpleadoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aplicar CORS antes de la autorización
app.UseCors("AllowMyApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
