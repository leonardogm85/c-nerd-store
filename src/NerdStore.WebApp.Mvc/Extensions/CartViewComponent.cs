using Microsoft.AspNetCore.Mvc;
using NerdStore.Vendas.Application.Queries;
using System;
using System.Threading.Tasks;

namespace NerdStore.WebApp.Mvc.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IPedidoQueries _pedidoQueries;

        protected Guid ClienteId = Guid.Parse("036437B9-1CCA-4F83-974A-8EC27A8C06C5");

        public CartViewComponent(IPedidoQueries pedidoQueries)
        {
            _pedidoQueries = pedidoQueries;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
            var itens = carrinho?.Itens.Count ?? 0;

            return View(itens);
        }
    }
}
