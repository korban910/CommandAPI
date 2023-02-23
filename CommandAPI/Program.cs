using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectBuilder = new NpgsqlConnectionStringBuilder
{
    ConnectionString = builder.Configuration.GetConnectionString("PostgresSqlConnection"),
    Username = builder.Configuration["UserID"],
    Password = builder.Configuration["Password"]
};
builder.Services.AddDbContext<CommandContext>(opt =>
{
    opt.UseNpgsql(connectBuilder.ConnectionString);
    // opt.UseSql(builder.Configuration.GetConnectionString("MySqlConnection"));
});
// Add service for NewtonsoftJson
builder.Services.AddControllers().AddNewtonsoftJson(s =>
{
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllers();
});

app.Run();