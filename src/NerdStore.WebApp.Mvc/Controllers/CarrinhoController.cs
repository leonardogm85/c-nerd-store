﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Application.Queries;
using System;
using System.Threading.Tasks;

namespace NerdStore.WebApp.Mvc.Controllers
{
    public class CarrinhoController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IPedidoQueries _pedidoQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public CarrinhoController(INotificationHandler<DomainNotification> notifications, IProdutoAppService produtoAppService, IPedidoQueries pedidoQueries, IMediatorHandler mediatorHandler)
            : base(notifications, mediatorHandler)
        {
            _produtoAppService = produtoAppService;
            _pedidoQueries = pedidoQueries;
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("meu-carrinho")]
        public async Task<IActionResult> Index()
        {
            return View(await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost("meu-carrinho")]
        public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
        {
            var produto = await _produtoAppService.ObterPorId(id);

            if (produto == null) return BadRequest();

            if (produto.QuantidadeEstoque < quantidade)
            {
                TempData["Erro"] = "Produto com estoque insuficiente.";
                return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
            }

            var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
            await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
            {
                return RedirectToAction("Index");
            }

            TempData["Erros"] = ObterMensagensErro();
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }

        //[HttpPost("remover-item")]
        //public async Task<IActionResult> RemoverItem(Guid id)
        //{
        //    var produto = await _produtoAppService.ObterPorId(id);

        //    if (produto == null) return BadRequest();

        //    var command = new RemoverItemPedidoCommand(ClienteId, id);
        //    await _mediatorHandler.EnviarComando(command);

        //    if (OperacaoValida())
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View("Index", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        //}

        //[HttpPost("atualizar-item")]
        //public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
        //{
        //    var produto = await _produtoAppService.ObterPorId(id);

        //    if (produto == null) return BadRequest();

        //    var command = new AtualizarItemPedidoCommand(ClienteId, id, quantidade);
        //    await _mediatorHandler.EnviarComando(command);

        //    if (OperacaoValida())
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View("Index", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        //}

        //[HttpPost("aplicar-voucher")]
        //public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
        //{
        //    var command = new AplicarVoucherPedidoCommand(ClienteId, voucherCodigo);
        //    await _mediatorHandler.EnviarComando(command);

        //    if (OperacaoValida())
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View("Index", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        //}
    }
}
