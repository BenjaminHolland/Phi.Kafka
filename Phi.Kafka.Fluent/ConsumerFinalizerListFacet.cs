using System;
using System.Collections.Generic;
using Confluent.Kafka;
namespace Phi.Kafka.Fluent
{
    internal class ConsumerFinalizerListFacet : IConsumerFinalizerListFacet
    {
        private readonly IList<Action<Consumer>> _finalizers = new List<Action<Consumer>>();
        IConsumerFinalizerListFacet IConsumerFinalizerListFacet.AppendFinalizer(Action<Consumer> finalizer)
        {
            _finalizers.Add(finalizer);
            return this;
        }
        public IEnumerable<Action<Consumer>> ValidateAndBuild() => _finalizers;
    }
    internal class ConsumerFinalizerListFacet<TKey, TValue> : IConsumerFinalizerListFacet<TKey, TValue>
    {
        private readonly IList<Action<Consumer<TKey, TValue>>> _finalizers = new List<Action<Consumer<TKey, TValue>>>();
        IConsumerFinalizerListFacet<TKey, TValue> IConsumerFinalizerListFacet<TKey, TValue>.AppendFinalizer(Action<Consumer<TKey, TValue>> finalizer)
        {
            _finalizers.Add(finalizer);
            return this;
        }
        public IEnumerable<Action<Consumer<TKey, TValue>>> ValidateAndBuild() => _finalizers;
    }
}
