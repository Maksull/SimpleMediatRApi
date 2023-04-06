using Microsoft.Extensions.Caching.Memory;
using SimpleMediatr.DI;
using SimpleMediatR.Endpoints;
using SimpleMediatR.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDI(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapProductsEndpoints();
app.MapCategoriesEndpoints();

app.MapControllers();

app.MigrateDatabase();

app.Run();
