using System;
using Confluent.Kafka;
namespace Phi.Kafka.Fluent
{
    public interface IConsumerFactory<TKey, TValue>
    {
        Consumer<TKey, TValue> Create(Action<IConsumerFacet<TKey, TValue>> assembler);
    }
}
