using System;
using System.Collections.Generic;
using Confluent.Kafka;
namespace Phi.Kafka.Fluent
{
    internal class ProducerFinalizerListFacet : IProducerFinalizerListFacet
    {
        private readonly IList<Action<Producer>> _finalizers = new List<Action<Producer>>();
        IProducerFinalizerListFacet IProducerFinalizerListFacet.AppendFinalizer(Action<Producer> finalizer)
        {
            _finalizers.Add(finalizer);
            return this;
        }
        public IEnumerable<Action<Producer>> ValidateAndBuild() => _finalizers;
    }
    internal class ProducerFinalizerListFacet<TKey, TValue> : IProducerFinalizerListFacet<TKey, TValue>
    {
        private readonly IList<Action<Producer<TKey, TValue>>> _finalizers = new List<Action<Producer<TKey, TValue>>>();
        IProducerFinalizerListFacet<TKey, TValue> IProducerFinalizerListFacet<TKey, TValue>.AppendFinalizer(Action<Producer<TKey, TValue>> finalizer)
        {
            _finalizers.Add(finalizer);
            return this;
        }
        public IEnumerable<Action<Producer<TKey, TValue>>> ValidateAndBuild() => _finalizers;
    }
}
