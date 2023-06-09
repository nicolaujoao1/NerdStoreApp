using MediatR;

namespace NerdStore.Catalogo.Domain.Events
{
    public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
    {
        private IProdutoRepository _produtoRepository;

        public ProdutoEventHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Handle(ProdutoAbaixoEstoqueEvent message, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorId(message.AggregateId);

            //Exemplo: Enviar um email para aquisição de mais produtos 
        }
    }
}
