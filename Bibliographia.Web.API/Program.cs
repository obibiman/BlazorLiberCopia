using Bibliographia.Web.API.Configurations;
using Bibliographia.Web.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//add connection string
var connString = builder.Configuration.GetConnectionString("BiblioRepoDb");
builder.Services.AddDbContext<BiblioContext>(options => options.UseSqlServer(connString));


//add automapper
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add serilog
builder.Host.UseSerilog((contxt, loggingConfig) => loggingConfig.WriteTo.Console().ReadFrom.Configuration(contxt.Configuration));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", y => y
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
