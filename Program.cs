using CadastroApi.Data; // Certifique-se que essa linha está presente
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder; // Adicionado para configuração do middleware
using Microsoft.Extensions.DependencyInjection; // Adicionado para configuração de serviços
using Microsoft.Extensions.Hosting; // Adicionado para configurar o ambiente de hospedagem

var builder = WebApplication.CreateBuilder(args);

// Adicionar o serviço do DbContext com o SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=cadastro.db")); // Banco de dados SQLite

builder.Services.AddControllers(); // Adiciona o suporte para controladores

// Adiciona o middleware de tratamento de erros global
builder.Services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();

// Middleware de tratamento de erros - isso irá capturar exceções não tratadas globalmente
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers(); // Mapeia os controladores
});

// Criar automaticamente o banco de dados ao iniciar o app
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
