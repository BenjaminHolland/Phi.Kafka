using System;
namespace Phi.Kafka.Fluent
{
    public class ConsumerFinalizers : IConsumerFinalizers
    {
        private static readonly Lazy<IConsumerFinalizers> _instance = new Lazy<IConsumerFinalizers>(() => new ConsumerFinalizers());
        public static IConsumerFinalizers Instance => _instance.Value;
        private ConsumerFinalizers() { }
    }
    public class ConsumerFinalizers<TKey, TValue> : IConsumerFinalizers<TKey, TValue>
    {
        private static readonly Lazy<IConsumerFinalizers<TKey, TValue>> _instance = new Lazy<IConsumerFinalizers<TKey, TValue>>(() => new ConsumerFinalizers<TKey, TValue>());
        public static IConsumerFinalizers<TKey, TValue> Instance => _instance.Value;
        private ConsumerFinalizers() { }
    
    }
}
