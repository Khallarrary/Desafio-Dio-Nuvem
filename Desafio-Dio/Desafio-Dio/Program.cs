using Desafio_Dio.Data;
using Microsoft.EntityFrameworkCore;
using Azure.Data.Tables;
using Desafio_Dio.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConexaoLocal")));

builder.Services.AddSingleton<TableClient>(_ =>
    new TableClient(
        builder.Configuration.GetConnectionString("EmuladorLocal"),
        "Historico"));

builder.Services.AddScoped<FuncionarioService>();

var app = builder.Build();

var funcionarioTabela = app.Services.GetRequiredService<TableClient>();

await funcionarioTabela.CreateIfNotExistsAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/openapi/v1.json", "API de Funcionarios v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
