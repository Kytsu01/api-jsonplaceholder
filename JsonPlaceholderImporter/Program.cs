using Microsoft.EntityFrameworkCore;
using JsonPlaceholderImporter.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

builder.Services.AddHttpClient();

builder.Services.AddScoped<JsonPlaceholderImporter.Controllers.Imports.UsersImportController>();
builder.Services.AddScoped<JsonPlaceholderImporter.Controllers.Imports.PostsImporterController>();
builder.Services.AddScoped<JsonPlaceholderImporter.Controllers.Imports.CommentsImportController>();
builder.Services.AddScoped<JsonPlaceholderImporter.Controllers.Imports.AlbumsImportController>();
builder.Services.AddScoped<JsonPlaceholderImporter.Controllers.Imports.PhotosImportController>();
builder.Services.AddScoped<JsonPlaceholderImporter.Controllers.Imports.TodosImportController>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
