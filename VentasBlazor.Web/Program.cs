using VentasBlazor.Web.Components;
using VentasBlazor.Web.Model.Commands;
using VentasBlazor.Web.Model.Database;
using VentasBlazor.Web.Model.Services;

namespace VentasBlazor.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("The connection string 'DefaultConnection' was not found.");
            }

            // Registro de servicios de base de datos y lógica
            builder.Services.AddScoped<SQLServer>(sql => new SQLServer(connectionString));

            // Servicios y comandos para Productos
            builder.Services.AddScoped<ProductoCommand>();
            builder.Services.AddScoped<ProductoService>();

            // Servicios y comandos para Clientes
            builder.Services.AddScoped<ClienteCommand>();
            builder.Services.AddScoped<ClienteService>();

            builder.Services.AddScoped<ClienteCorreoCommand>();
            builder.Services.AddScoped<ClienteCorreoService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
