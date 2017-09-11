using System;
using Confluent.Kafka;
namespace Phi.Kafka.Fluent
{
    public class DefaultProducerFactory<TKey, TValue> : IProducerFactory<TKey, TValue>
    {
        private static readonly Lazy<IProducerFactory<TKey, TValue>> _instance = new Lazy<IProducerFactory<TKey, TValue>>(() => new DefaultProducerFactory<TKey, TValue>());
        public static IProducerFactory<TKey, TValue> Instance => _instance.Value;
        private DefaultProducerFactory() { }

        public Producer<TKey, TValue> Create(Action<IProducerFacet<TKey, TValue>> assembler)
        {
            if (assembler == null) throw new ArgumentNullException(nameof(assembler));
            var producerFacet = new ProducerFacet<TKey, TValue>();
            assembler(producerFacet);
            return producerFacet.ValidateAndBuild();
        }
    }
}
