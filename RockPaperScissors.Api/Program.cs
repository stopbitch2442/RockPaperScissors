using Microsoft.OpenApi.Models;
using System.Reflection;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "RockPaperScissors API",
        Description = "Развлекательна игра камень-ножницы-бумага",
        Contact = new OpenApiContact
        {
            Name = "Связь со мной(разработчиком)",
            Url = new Uri("https://t.me/neofall")
        },
    });
    var projectDirectory = AppContext.BaseDirectory;

    var projectName = Assembly.GetExecutingAssembly().GetName().Name;
    var xmlFileName = $"{projectName}.xml";
    var xmlPath = Path.Combine(projectDirectory, xmlFileName);
    options.IncludeXmlComments(xmlPath);
    options.AddEnumsWithValuesFixFilters();
});

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

app.Run();
