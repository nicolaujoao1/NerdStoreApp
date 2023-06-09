using NerdStore.Core.Messages;
using System.Runtime.CompilerServices;

namespace NerdStore.Core.DomainObjects
{
    public class DomainEvent:Event
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
