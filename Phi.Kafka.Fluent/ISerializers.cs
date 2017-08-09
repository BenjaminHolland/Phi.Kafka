using Confluent.Kafka.Serialization;
namespace Phi.Kafka.Fluent
{
    public interface ISerializers<TKey, TValue>
    {
        ISerializer<TKey> ForKey { get; }
        ISerializer<TValue> ForValue { get; }
    }
}
