using System;
using Confluent.Kafka;
namespace Phi.Kafka.Fluent
{
    public class DefaultConsumerFactory : IConsumerFactory
    {
        private static readonly Lazy<IConsumerFactory> _instrance = new Lazy<IConsumerFactory>(() => new DefaultConsumerFactory());
        public static IConsumerFactory Instance = _instrance.Value;
        private DefaultConsumerFactory() { }
        public Consumer Create(Action<IConsumerFacet> assembler)
        {
            if (assembler == null) throw new ArgumentNullException(nameof(assembler));
            var consumerFacet = new ConsumerFacet();
            assembler(consumerFacet);
            return consumerFacet.ValidateAndBuild();
        }
    }
    public class DefaultConsumerFactory<TKey, TValue> : IConsumerFactory<TKey, TValue>
    {
        private static readonly Lazy<IConsumerFactory<TKey, TValue>> _instance = new Lazy<IConsumerFactory<TKey, TValue>>(() => new DefaultConsumerFactory<TKey, TValue>());
        public static IConsumerFactory<TKey, TValue> Instance => _instance.Value;
        private DefaultConsumerFactory() { }

        public Consumer<TKey, TValue> Create(Action<IConsumerFacet<TKey, TValue>> assembler)
        {
            if (assembler == null) throw new ArgumentNullException(nameof(assembler));
            var consumerFacet = new ConsumerFacet<TKey, TValue>();
            assembler(consumerFacet);
            return consumerFacet.ValidateAndBuild();
        }
    }
}
