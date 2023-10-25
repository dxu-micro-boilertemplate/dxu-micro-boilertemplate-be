using System.Reflection;
using System.Text.Json.Serialization;
using API.Data.Repositories;
using API.Infrastructure.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using API.Infrastructure;
using H.Core.Data;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*;https://*");
// Add services to the container.
builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", corsPolicyBuilder => corsPolicyBuilder
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});
builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    o.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
    o.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    o.SerializerSettings.ContractResolver = new DefaultContractResolver()
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    };

    o.SerializerSettings.Converters.Add(new StringEnumConverter()
    {
        AllowIntegerValues = true
    });
});
builder.Services.AddMvc()
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.RegisterSwaggerModule();
builder.Services.RegisterServiceModule();
builder.Services.RegisterDataRepositories();
builder.Services.RegisterCoreData();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(cor => cor
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials())
    .UseDeveloperExceptionPage()
    .UseApplicationSwagger()
    .UseHttpsRedirection();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();