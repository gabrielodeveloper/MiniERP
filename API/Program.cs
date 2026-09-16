using AcessoBancoDados;
using Negocios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddScoped<AcessoDadosSqlServer>(sp =>
{
    string? connectionString =
        builder.Configuration.GetConnectionString("PedidoVenda");

    if(string.IsNullOrEmpty(connectionString))
    {
        throw new Exception("Connection string 'PedidoVenda' não foi configurada.");
    }

    return new AcessoDadosSqlServer(connectionString);
});

builder.Services.AddScoped<ClienteNegocio>();
builder.Services.AddScoped<ProdutoNegocio>();
builder.Services.AddScoped<PedidoNegocio>();
builder.Services.AddScoped<PedidoItemNegocio>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "PedidoVenda API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
