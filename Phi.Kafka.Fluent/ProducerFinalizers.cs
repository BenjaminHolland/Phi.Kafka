using System;
namespace Phi.Kafka.Fluent
{

    public class ProducerFinalizers : IProducerFinalizers
    {
        private static readonly Lazy<IProducerFinalizers> _instance = new Lazy<IProducerFinalizers>(() => new ProducerFinalizers());
        public static IProducerFinalizers Instance => _instance.Value;
        private ProducerFinalizers() { }
    }
    public class ProducerFinalizers<TKey,TValue> : IProducerFinalizers<TKey, TValue>
    {
        private static readonly Lazy<IProducerFinalizers<TKey, TValue>> _instance = new Lazy<IProducerFinalizers<TKey, TValue>>(() => new ProducerFinalizers<TKey, TValue>());
        public static IProducerFinalizers<TKey, TValue> Instance => _instance.Value;
        private ProducerFinalizers() { }
    }
}
