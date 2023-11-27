using Microsoft.EntityFrameworkCore;
using SimonP_amital.Context;
using SimonP_amital.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register DbContext
builder.Services.AddDbContext<DataBaseContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("TemplateDB")); });


// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("*") // Replace with the allowed origin
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
RegisterServiceDependencies(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Use CORS middleware
app.UseCors("AllowSpecificOrigin");

app.Run();

static void RegisterServiceDependencies(WebApplicationBuilder builder)
{
    AddServices(builder);
}

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<LogicService>();
}