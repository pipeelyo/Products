using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using productos.Context;
using productos.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<AppDBContext>(
    op => op.UseSqlServer(connectionString)
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sales API", Version = "v1" });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = @"JWT Authorization header using the Bearer scheme. <br /> <br />
//                      Enter 'Bearer' [space] and then your token in the text input below.<br /> <br />
//                      Example: 'Bearer 12345abcdef'<br /> <br />",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
//      {
//        {
//          new OpenApiSecurityScheme
//          {
//            Reference = new OpenApiReference
//              {
//                Type = ReferenceType.SecurityScheme,
//                Id = "Bearer"
//              },
//              Scheme = "oauth2",
//              Name = "Bearer",
//              In = ParameterLocation.Header,
//            },
//            new List<string>()
//          }
//        });
//});


builder.Services.AddScoped<IAutorizacionService, AutorizacionService>();

var key = builder.Configuration.GetValue<string>("JwtSettings:key");
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(conf =>
{
    conf.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    conf.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(conf =>
{
    conf.RequireHttpsMetadata = false;
    conf.SaveToken = true;
    conf.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
