using Confluent.Kafka.Serialization;
using System;
using Confluent.Kafka;
using Phi.Kafka.Fluent.Resources;
namespace Phi.Kafka.Fluent
{
    internal class SerializationFacet<TKey, TValue> : ISerializers<TKey, TValue>, ISerializationFacet<TKey, TValue>
    {
        private static readonly bool IS_KEY_NULL = typeof(TKey).Equals(typeof(Null));
        private ISerializer<TKey> _forKey;
        private ISerializer<TValue> _forValue;
        ISerializer<TKey> ISerializers<TKey, TValue>.ForKey => _forKey;

        ISerializer<TValue> ISerializers<TKey, TValue>.ForValue => _forValue;

        ISerializationFacet<TKey, TValue> ISerializationFacet<TKey, TValue>.SetKeySerializer(ISerializer<TKey> serializer)
        {
            if (serializer == null && !IS_KEY_NULL) throw new ArgumentNullException(nameof(serializer));
            _forKey = serializer;
            return this;
        }

        ISerializationFacet<TKey, TValue> ISerializationFacet<TKey, TValue>.SetValueSerializer(ISerializer<TValue> serializer)
        {
            _forValue = serializer ?? throw new ArgumentNullException(nameof(serializer));
            return this;
        }
        public ISerializers<TKey, TValue> ValidateAndBuild()
        {
            if (_forKey == null && !IS_KEY_NULL) throw new InvalidOperationException(ErrorMessages.KeySerializerRequiredMessage);
            if (_forValue == null) throw new InvalidOperationException(ErrorMessages.ValueSerializerRequiredMessage);
            return this;
        }
    }
}
