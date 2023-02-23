using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CommandContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSqlConnection"));
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