using CadastroApi.Data; // Certifique-se que essa linha está presente
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar o serviço do DbContext com o SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=cadastro.db")); // Banco de dados SQLite

builder.Services.AddControllers(); // Adiciona o suporte para controladores

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Mapeia os controladores
});

// Criar automaticamente o banco de dados ao iniciar o app
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
