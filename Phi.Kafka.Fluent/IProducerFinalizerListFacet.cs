using Confluent.Kafka;
using System;

namespace Phi.Kafka.Fluent
{
    public interface IProducerFinalizerListFacet
    {
        IProducerFinalizerListFacet AppendFinalizer(Action<Producer> finalizer);
    }
    public interface IProducerFinalizerListFacet<TKey, TValue>
    {
        IProducerFinalizerListFacet<TKey, TValue> AppendFinalizer(Action<Producer<TKey, TValue>> finalizer);
    }
}