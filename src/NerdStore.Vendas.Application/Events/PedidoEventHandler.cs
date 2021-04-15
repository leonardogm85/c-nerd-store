﻿using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Vendas.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Events
{
    public class PedidoEventHandler :
        INotificationHandler<PedidoRascunhoIniciadoEvent>,
        INotificationHandler<PedidoAtualizadoEvent>,
        INotificationHandler<PedidoItemAdicionadoEvent>,
        INotificationHandler<PedidoEstoqueRejeitadoEvent>,
        INotificationHandler<PagamentoRealizadoEvent>,
        INotificationHandler<PagamentoRecusadoEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PedidoEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(PedidoRascunhoIniciadoEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoAtualizadoEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoItemAdicionadoEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(PedidoEstoqueRejeitadoEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoCommand(message.PedidoId, message.ClienteId));
        }

        public async Task Handle(PagamentoRealizadoEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new FinalizarPedidoCommand(message.PedidoId, message.ClienteId));
        }

        public async Task Handle(PagamentoRecusadoEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoEstornarEstoqueCommand(message.PedidoId, message.ClienteId));
        }
    }
}
