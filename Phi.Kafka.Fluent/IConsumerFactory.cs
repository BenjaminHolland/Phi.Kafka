using System;
using Confluent.Kafka;
using Phi.Kafka.Fluent;
namespace Phi.Kafka.Fluent
{
    public interface IConsumerFactory<TKey, TValue>
    {
        Consumer<TKey, TValue> Create(Action<IConsumerFacet<TKey, TValue>> assembler);
    }
}
