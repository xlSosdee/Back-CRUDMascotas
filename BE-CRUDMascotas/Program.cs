using BE_CRUDMascotas.Models;
using BE_CRUDMascotas.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registro del contexto de la base de datos en el contenedor de servicios
//Utilizamos <AplicationDbContext> para indicar el contexto de la base de datos creado previamente

builder.Services.AddDbContext<AplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
});

//Automapper

builder.Services.AddAutoMapper(typeof(Program));

//Add Services

builder.Services.AddScoped<IMascotaRepository,MascotaRepository>();

var app = builder.Build();



app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
