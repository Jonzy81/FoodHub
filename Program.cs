using DotNetEnv;
using FoodHub.Data;
using FoodHub.Data.Repository;
using FoodHub.Data.Repository.IRepository;
using FoodHub.Services;
using FoodHub.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Loading variables from .env file
Env.Load();
// Add services to the container.
builder.Services.AddDbContext<RestaurantContext>(options =>
{
    options.UseSqlServer(Environment.GetEnvironmentVariable("DEFAULTCONNECTION"));  //Connectionstring from env file 
});

builder.Services.AddControllers();


//Cors setup, this method only allows you to only allow this server 
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("LocalReact", policy =>
//    {
//        policy.WithOrigins("http://localhost:5175/")  //Change http adress for the correct one for your react app 
//        .AllowAnyHeader()
//        .AllowAnyMethod()
//        .AllowCredentials();      //Used for text based information
//    });
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,    //Checks if token is valid 
            ValidateIssuerSigningKey = true,
            ValidIssuer =Environment.GetEnvironmentVariable("JWT_ISSUER"),
            ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")))   //Get data from config file
        };
    });   
builder.Services.AddAuthorization();    //Autherization
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(     //Adds jwt tokens to swagger manages authentication easier
//    c =>
//    {
//        c.SwaggerDoc("v1", new OpenApiInfo { Title = "SQLicious API", Version = "v1" });
//        // Define Bearer Authentication scheme for Swagger
//        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//        {
//            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
//            Name = "Authorization",
//            In = ParameterLocation.Header,
//            Type = SecuritySchemeType.Http,
//            Scheme = "bearer"
//        });
//        // Apply Bearer authentication globally in Swagger UI
//        c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                },
//                Scheme = "oauth2",
//                Name = "Bearer",
//                In = ParameterLocation.Header,
//            },
//            new List<string>()
//        }
//    });
//    }
       );
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();

var app = builder.Build();

//app.UseCors("LocalReact");    //Uses for Cors

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
