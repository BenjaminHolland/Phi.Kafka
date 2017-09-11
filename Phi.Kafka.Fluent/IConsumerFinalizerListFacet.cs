using System;
using Confluent.Kafka;
namespace Phi.Kafka.Fluent
{
    public interface IConsumerFinalizerListFacet
    {
        IConsumerFinalizerListFacet AppendFinalizer(Action<Consumer> finalizer);
    }
    public interface IConsumerFinalizerListFacet<TKey, TValue>
    {
        /// <summary>
        /// Add a Consumer Finalizer
        /// </summary>
        /// <param name="finalizer">The finalizer to run</param>
        /// <returns></returns>
        /// <remarks>
        /// Finalizers are run in the order they are added
        /// </remarks>
        IConsumerFinalizerListFacet<TKey, TValue> AppendFinalizer(Action<Consumer<TKey, TValue>> finalizer);
    }
}
