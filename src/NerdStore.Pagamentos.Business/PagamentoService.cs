using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace NerdStore.Pagamentos.Business
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PagamentoService(
            IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
            IPagamentoRepository pagamentoRepository,
            IMediatorHandler mediatorHandler)
        {
            _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
            _pagamentoRepository = pagamentoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido)
        {
            var pedido = new Pedido
            {
                Id = pagamentoPedido.PedidoId,
                Valor = pagamentoPedido.Total
            };

            var pagamento = new Pagamento
            {
                PedidoId = pagamentoPedido.PedidoId,
                Valor = pagamentoPedido.Total,
                NomeCartao = pagamentoPedido.NomeCartao,
                NumeroCartao = pagamentoPedido.NumeroCartao,
                ExpiracaoCartao = pagamentoPedido.ExpiracaoCartao,
                CvvCartao = pagamentoPedido.CvvCartao
            };

            var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

            if (transacao.StatusTransacao == StatusTransacao.Pago)
            {
                transacao.AdicionarEvento(new PagamentoRealizadoEvent(
                    pagamentoPedido.PedidoId,
                    pagamentoPedido.ClienteId,
                    transacao.PagamentoId,
                    transacao.Id,
                    pagamentoPedido.Total));

                _pagamentoRepository.Adicionar(pagamento);
                _pagamentoRepository.AdicionarTransacao(transacao);

                await _pagamentoRepository.UnitOfWork.Commit();

                return transacao;
            }

            await _mediatorHandler.PublicarNotificacao(new DomainNotification("Pagamento", "A operadora recusou o pagamento."));
            await _mediatorHandler.PublicarEvento(new PagamentoRecusadoEvent(
                pagamentoPedido.PedidoId,
                pagamentoPedido.ClienteId,
                transacao.PagamentoId,
                transacao.Id,
                pagamentoPedido.Total));

            return transacao;
        }
    }
}
