using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectBuilder = new NpgsqlConnectionStringBuilder();
connectBuilder.ConnectionString = builder.Configuration.GetConnectionString("PostgresSqlConnection");
connectBuilder.Username = builder.Configuration["UserID"];
connectBuilder.Password = builder.Configuration["Password"];
builder.Services.AddDbContext<CommandContext>(opt =>
{
    opt.UseNpgsql(connectBuilder.ConnectionString);
    // opt.UseSql(builder.Configuration.GetConnectionString("MySqlConnection"));
});
builder.Services.AddControllers();
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