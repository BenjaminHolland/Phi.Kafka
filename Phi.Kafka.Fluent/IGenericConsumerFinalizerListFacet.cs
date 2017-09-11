using System;
using Confluent.Kafka;
namespace Phi.Kafka.Fluent
{
    public interface IConsumerFinalizerListFacet<TKey, TValue>
    {
        IConsumerFinalizerListFacet<TKey, TValue> AppendFinalizer(Action<Consumer<TKey, TValue>> finalizer);
    }
}
