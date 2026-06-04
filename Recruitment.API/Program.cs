using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Recruitment.API.Middleware;
using Recruitment.Services.Abstraction;
using Recruitment.Services.Implementation;
using Recruitment.Store.Abstraction;
using Recruitment.Store.Implementation;
using Serilog;
using System.Text;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30)
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseSerilog();

// Controllers
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Dependency Injection


builder.Services.AddScoped<IUserStore, UserStore>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAuthStore, AuthStore>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IJobStore, JobStore>();
builder.Services.AddScoped<IJobService, JobService>();

builder.Services.AddScoped<IApplicationStore, ApplicationStore>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

builder.Services.AddScoped<IErrorLogStore, ErrorLogStore>();

builder.Services.AddScoped<IInterviewStore, InterviewStore>();

builder.Services.AddScoped<IInterviewService, InterviewService>();


// JWT Authentication


var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey!))
            };
    });

builder.Services.AddAuthorization();


// Swagger


builder.Services.AddSwaggerGen(c =>
{

    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Enter JWT Token"
        });

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type =
                                ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });

    var xmlFile =
        $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath =
        Path.Combine(
            AppContext.BaseDirectory,
            xmlFile);

    c.IncludeXmlComments(xmlPath);
});

// API Behavior


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


// Build App


var app = builder.Build();
//app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();