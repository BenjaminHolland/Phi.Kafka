using Confluent.Kafka.Serialization;
namespace Phi.Kafka.Fluent
{
    public interface ISerializationFacet<TKey, TValue>
    {
        ISerializationFacet<TKey, TValue> SetKeySerializer(ISerializer<TKey> deserializer);
        ISerializationFacet<TKey, TValue> SetValueSerializer(ISerializer<TValue> deserializer);
    }
}
