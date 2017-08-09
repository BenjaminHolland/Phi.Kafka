using Confluent.Kafka.Serialization;
namespace Phi.Kafka.Fluent
{
    public interface IDeserializers<TKey, TValue>
    {
        IDeserializer<TKey> ForKey { get; }
        IDeserializer<TValue> ForValue { get; }
    }
}
