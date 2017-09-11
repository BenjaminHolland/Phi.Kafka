using System;
using System.Collections.Generic;
using Confluent.Kafka;
using System.Linq;
namespace Phi.Kafka.Fluent
{
    internal class ConsumerFacet<TKey, TValue> : IConsumerFacet<TKey, TValue>
    {
        Action<IIdentityFacet> _identityAssembler = NoOpFactory.NoOp<IIdentityFacet>();
        Action<IAutoCommitFacet> _autoCommitAssembler = NoOpFactory.NoOp<IAutoCommitFacet>();
        Action<IBrokerListFacet> _brokerListAssembler = NoOpFactory.NoOp<IBrokerListFacet>();
        Action<IDeserializationFacet<TKey, TValue>> _deserializationAssembler = NoOpFactory.NoOp<IDeserializationFacet<TKey, TValue>>();
        Action<ICustomFacet> _customAssembler = NoOpFactory.NoOp<ICustomFacet>();
        Action<IDebugFacet> _debugAssembler = NoOpFactory.NoOp<IDebugFacet>();
        Action<IConsumerFinalizerListFacet<TKey, TValue>> _finalizerListAssembler = NoOpFactory.NoOp<IConsumerFinalizerListFacet<TKey, TValue>>();

        IConsumerFacet<TKey, TValue> IConsumerFacet<TKey, TValue>.SetIdentity(Action<IIdentityFacet> assembler)
        {
            _identityAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }

        IConsumerFacet<TKey, TValue> IConsumerFacet<TKey, TValue>.SetAutoCommit(Action<IAutoCommitFacet> assembler)
        {
            _autoCommitAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }

        IConsumerFacet<TKey, TValue> IConsumerFacet<TKey, TValue>.SetBrokers(Action<IBrokerListFacet> assembler)
        {
            _brokerListAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }

        IConsumerFacet<TKey, TValue> IConsumerFacet<TKey, TValue>.SetDeserializers(Action<IDeserializationFacet<TKey, TValue>> assembler)
        {
            _deserializationAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }

        IConsumerFacet<TKey, TValue> IConsumerFacet<TKey, TValue>.SetCustom(Action<ICustomFacet> assembler)
        {
            _customAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }

        IConsumerFacet<TKey, TValue> IConsumerFacet<TKey, TValue>.SetFinalizers(Action<IConsumerFinalizerListFacet<TKey, TValue>> assembler)
        {
            _finalizerListAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }
        IConsumerFacet<TKey, TValue> IConsumerFacet<TKey, TValue>.SetDebug(Action<IDebugFacet> assembler)
        {
            _debugAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }
        public Consumer<TKey,TValue> ValidateAndBuild()
        {
            var settings = Enumerable.Empty<KeyValuePair<string, object>>();

            var identityFacet = new IdentityFacet();
            _identityAssembler(identityFacet);
            settings=settings.Concat(identityFacet.ValidateAndBuild().ToArray());

            var brokerFacet = new BrokerListFacet();
            _brokerListAssembler(brokerFacet);
            settings=settings.Concat(brokerFacet.ValidateAndBuild().ToArray());

            var autoCommitFacet = new AutoCommitFacet();
            _autoCommitAssembler(autoCommitFacet);
            settings=settings.Concat(autoCommitFacet.ValidateAndBuild().ToArray());

            var debugFacet = new DebugFacet();
            _debugAssembler(debugFacet);
            settings=settings.Concat(debugFacet.ValidateAndBuild().ToArray());

            var customFacet = new CustomFacet();
            _customAssembler(customFacet);
            settings=settings.Concat(customFacet.ValidateAndBuild().ToArray());

            var deserializerFacet = new DeserializationFacet<TKey, TValue>();
            _deserializationAssembler(deserializerFacet);
            var deserializers = deserializerFacet.ValidateAndBuild();

            IDictionary<string, object> finalSettings = new Dictionary<string, object>();
            foreach(var setting in settings)
            {
                finalSettings[setting.Key] = setting.Value;
            }

            var finalizationFacet = new ConsumerFinalizerListFacet<TKey, TValue>();
            _finalizerListAssembler(finalizationFacet);
            var finalizerSequence = finalizationFacet.ValidateAndBuild();

            var consumer = new Consumer<TKey, TValue>(finalSettings, deserializers.ForKey, deserializers.ForValue);
            foreach(var finalizer in finalizerSequence)
            {
                finalizer(consumer);
            }
            return consumer;
        }

   
    }
}
