using ClientWebsiteAPI.DataAccess;
using ClientWebsiteAPI.Extensions;
using ClientWebsiteAPI.HelperClass;
using ClientWebsiteAPI.Interface;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddJWTTokenServices(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var info = new OpenApiInfo()
{
    Title = "DT API Documentation",
    Version = "v1",
    Description = "",   

};
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IAccount, AccountAccess>();
builder.Services.AddSingleton<IGeneral, GeneralAccess>();
builder.Services.AddSingleton<IBooking, BookingAccess>();
//builder.Services.AddSingleton<ICommunication, CommunicationAccess>();
builder.Services.AddSingleton<ICommunication, CommunicationAccess>();
builder.Services.AddSwaggerGen(c =>
{    // To provide the API summary
    c.SwaggerDoc("v1", info);

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}
app.UseSwagger(u =>
{
    u.RouteTemplate = "swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "swagger";
    options.SwaggerEndpoint(url: "./v1/swagger.json", name: "ClientWebSiteAPI");  //"Your API Title or Version"
    options.DefaultModelsExpandDepth(-1);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
