using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;
using TI_NET_API.BLL.Interfaces;
using TI_NET_API.BLL.Services;
using TI_NET_API.DAL.Context;
using TI_NET_API.DAL.Interfaces;
using TI_NET_API.DAL.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using TI_NET_API.API.Services;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

    //Permet de recuperer des enums sur base d'une string
builder.Services.AddControllers().AddJsonOptions(option => 
    {
        option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Add management of XML responses
//builder.Services.AddControllers(
//    config =>
//    {
//        config.RespectBrowserAcceptHeader = true;
//    }).AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });

    // Permet d'ajouter le "cadenas" sur les routes
    // - Implémentation simple (Cadenas sur toutes les routes)
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    // - Plus d'infos : 
    // https://github.com/domaindrivendev/Swashbuckle.AspNetCore?tab=readme-ov-file#add-security-definitions-and-requirements
});


// Dependency Injection
// - DB

builder.Services.AddTransient<SqlConnection>(c => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// - DAL
builder.Services.AddSingleton<FakeDB>();
builder.Services.AddScoped<IMovieRepository, MovieADORepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// - BLL
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IUserService, UserService>();
// - API
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<MailHelperService>();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],

        ValidateLifetime = true

    }; 
});

builder.Services.AddCors(service =>
{
    service.AddPolicy("FFA", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });

    service.AddPolicy("Dev", policy =>
    {
        policy.WithOrigins("http://localhost:4200");
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Dev");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
