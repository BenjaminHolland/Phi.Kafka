using System;
using Confluent.Kafka;
namespace Phi.Kafka.Fluent
{
    public interface IProducerFactory<TKey, TValue>
    {
        Producer<TKey, TValue> Create(Action<IProducerFacet<TKey, TValue>> assembler);
    }
}
