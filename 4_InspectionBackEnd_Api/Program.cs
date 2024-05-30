using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using _3_InspectionBackEnd_Infrastructure;
using _2_InspectionBackEnd_Application;
using Microsoft.Extensions.Configuration;
using _3_InspectionBackEnd_Infrastructure.Settings;
using _3_InspectionBackEnd_Infrastructure.Seeder;
using _4_InspectionBackEnd_Api.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
    options.SuppressOutputFormatterBuffering = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token (\"bearer {token}\")",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    o.OperationFilter<SecurityRequirementsOperationFilter>();
    //o.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    //{
    //    { new OpenApiSecurityScheme
    //    {
    //        Reference = new OpenApiReference
    //        {
    //            Type=ReferenceType.SecurityScheme,
    //            Id="Bearer"
    //        }
    //    }, new string[]{}
    //    }
    //});
});

builder.Services.AddDatabaseInfrastructure(builder.Configuration);
builder.Services.AddIdentityApplication();
var settings = builder.Configuration.Get<Infrastructure_Setting>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Jwt.Key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,

        ValidIssuer = settings.Jwt.Issuer,
        ValidAudience = settings.Jwt.Audience
    };
});

//builder.Services.AddCors();
//builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
//{
//    build.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader();
//}));
//builder.Services.AddDbContext<BookingRMAppDataContext>(
//    o => o.UseNpgsql(builder.Configuration.GetConnectionString("BookingMRAppDb"))
//    );

var app = builder.Build();
var origins = builder.Configuration["AllowedOrigins"].Split(";");
app.UseCors(x => x
    .WithOrigins(origins)
    .AllowAnyMethod()
    .AllowAnyHeader());


app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

// Configure
//app.UseCors("AnyOrigin");
//
//app.UseCors("corspolicy");

app.UseRouting();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Seed();
app.HondaMotorcycleSeed();
app.KawasakiMotorcycleSeed();
app.PiaggioMotorcycleSeed();
app.SuzukiMotorcycleSeed();
app.YamahaMotorcycleSeed();
app.Run();
