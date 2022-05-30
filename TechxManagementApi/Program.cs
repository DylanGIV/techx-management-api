using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TechxManagementApi.Authorization;
using TechxManagementApi.Helpers;
using TechxManagementApi.Services;
using System.Configuration;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
 
    services.AddDbContext<DataContext>();
    services.AddCors();
    services.AddControllers().AddJsonOptions(x => 
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddSwaggerGen(swagger =>
        {
            //This is to generate the Default UI of Swagger Documentation  
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ASP.NET 5 Web API",
                Description = "Authentication and Authorization in ASP.NET 5 with JWT and Swagger"
            });
            // To Enable authorization using Swagger (JWT)  
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                }
            });
        });

    // configure strongly typed settings object

    IConfiguration config;

#if (DEBUG)

    config = builder.Configuration.GetSection("AppSettings");

#else

    config = new ConfigurationBuilder().AddEnvironmentVariables().Build();

#endif

    services.Configure<AppSettings>(config);

    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<IEmailService, EmailService>();
    services.AddScoped<ICompanyService, CompanyService>();
    services.AddScoped<IProjectService, ProjectService>();
    services.AddScoped<ITeamService, TeamService>();
    services.AddScoped<IAccountTaskService, AccountTaskService>();


}

var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();    
    dataContext.Database.Migrate();
}

// configure HTTP request pipeline
{
    // generated swagger json and swagger ui middleware
    app.UseSwagger();
    app.UseSwaggerUI(x => 
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET Sign-up and Verification API");
        x.RoutePrefix = "";
    });
    
    
    // global cors policy
    app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto
});


var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
app.Run("http://0.0.0.0:" + port);
