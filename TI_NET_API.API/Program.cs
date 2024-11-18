using TI_NET_API.BLL.Interfaces;
using TI_NET_API.BLL.Services;
using TI_NET_API.DAL.Context;
using TI_NET_API.DAL.Interfaces;
using TI_NET_API.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add management of XML responses
//builder.Services.AddControllers(
//    config =>
//    {
//        config.RespectBrowserAcceptHeader = true;
//    }).AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Dependency Injection

builder.Services.AddSingleton<FakeDB>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieServices, MovieServices>();

var app = builder.Build();

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
