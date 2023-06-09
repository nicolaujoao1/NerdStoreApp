using NerdStore.Core.Messages;

namespace NerdStore.Core.MediatR
{
    public interface IMediatRHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}
