using Confluent.Kafka.Serialization;
namespace Phi.Kafka.Fluent
{
    public interface IDeserializationFacet<TKey, TValue>
    {
        IDeserializationFacet<TKey, TValue> SetKeyDeserializer(IDeserializer<TKey> deserializer);
        IDeserializationFacet<TKey, TValue> SetValueDeserializer(IDeserializer<TValue> deserializer);
    }
}
