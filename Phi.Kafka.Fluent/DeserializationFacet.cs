using Confluent.Kafka.Serialization;
using System;
using Confluent.Kafka;
using Phi.Kafka.Fluent.Resources;
namespace Phi.Kafka.Fluent
{
    internal class DeserializationFacet<TKey, TValue> : IDeserializers<TKey, TValue>, IDeserializationFacet<TKey, TValue>
    {
        private static readonly bool IS_KEY_NULL = typeof(TKey).Equals(typeof(Null));
        private IDeserializer<TKey> _forKey;
        private IDeserializer<TValue> _forValue;
        IDeserializer<TKey> IDeserializers<TKey, TValue>.ForKey => _forKey;

        IDeserializer<TValue> IDeserializers<TKey, TValue>.ForValue => _forValue;

        IDeserializationFacet<TKey, TValue> IDeserializationFacet<TKey, TValue>.SetKeyDeserializer(IDeserializer<TKey> deserializer)
        {
            if (deserializer == null && !IS_KEY_NULL) throw new ArgumentNullException(nameof(deserializer));
            _forKey = deserializer;
            return this;
        }

        IDeserializationFacet<TKey, TValue> IDeserializationFacet<TKey, TValue>.SetValueDeserializer(IDeserializer<TValue> deserializer)
        {
            _forValue = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
            return this;
        }
        public IDeserializers<TKey, TValue> ValidateAndBuild()
        {
            if (_forKey == null && !IS_KEY_NULL) throw new InvalidOperationException(ErrorMessages.KeyDeserializerRequiredMessage);
            if (_forValue == null) throw new InvalidOperationException(ErrorMessages.ValueDeserializerRequiredMessage);
            return this;
        }
    }
}
