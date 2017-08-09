using System;
namespace Phi.Kafka.Fluent
{
    public class ConsumerFinalizers<TKey, TValue> : IConsumerFinalizers<TKey, TValue>
    {
        private static readonly Lazy<IConsumerFinalizers<TKey, TValue>> _instance = new Lazy<IConsumerFinalizers<TKey, TValue>>(() => new ConsumerFinalizers<TKey, TValue>());
        public static IConsumerFinalizers<TKey, TValue> Instance => _instance.Value;
        private ConsumerFinalizers() { }
    
    }
}
