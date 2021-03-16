using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Data;
using NerdStore.Catalogo.Data.Repositories;
using NerdStore.Catalogo.Domain;
using NerdStore.Core.Bus;

namespace NerdStore.WebApp.Mvc.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<CatalogoContext>();
        }
    }
}
