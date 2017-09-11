using System;
using System.Collections.Generic;
using Confluent.Kafka;
using System.Linq;
namespace Phi.Kafka.Fluent
{
    internal class ProducerFacet : IProducerFacet
    {
        Action<IIdentityFacet> _identityAssembler = NoOpFactory.NoOp<IIdentityFacet>();

        Action<IBrokerListFacet> _brokerListAssembler = NoOpFactory.NoOp<IBrokerListFacet>();
      
        Action<ICustomFacet> _customAssembler = NoOpFactory.NoOp<ICustomFacet>();
        Action<IDebugFacet> _debugAssembler = NoOpFactory.NoOp<IDebugFacet>();
        Action<IProducerFinalizerListFacet> _finalizerListAssembler = NoOpFactory.NoOp<IProducerFinalizerListFacet>();

        IProducerFacet IProducerFacet.SetIdentity(Action<IIdentityFacet> assembler)
        {
            _identityAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }



        IProducerFacet IProducerFacet.SetBrokers(Action<IBrokerListFacet> assembler)
        {
            _brokerListAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }

        
     

        IProducerFacet IProducerFacet.SetCustom(Action<ICustomFacet> assembler)
        {
            _customAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }

        IProducerFacet IProducerFacet.SetFinalizers(Action<IProducerFinalizerListFacet> assembler)
        {
            _finalizerListAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }
        IProducerFacet IProducerFacet.SetDebug(Action<IDebugFacet> assembler)
        {
            _debugAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }
        public Producer ValidateAndBuild()
        {
            var settings = Enumerable.Empty<KeyValuePair<string, object>>();

            var identityFacet = new IdentityFacet();
            _identityAssembler(identityFacet);
            settings = settings.Concat(identityFacet.ValidateAndBuild().ToArray());

            var brokerFacet = new BrokerListFacet();
            _brokerListAssembler(brokerFacet);
            settings = settings.Concat(brokerFacet.ValidateAndBuild().ToArray());



            var debugFacet = new DebugFacet();
            _debugAssembler(debugFacet);
            settings = settings.Concat(debugFacet.ValidateAndBuild().ToArray());

            var customFacet = new CustomFacet();
            _customAssembler(customFacet);
            settings = settings.Concat(customFacet.ValidateAndBuild().ToArray());

           

            IDictionary<string, object> finalSettings = new Dictionary<string, object>();
            foreach (var setting in settings)
            {
                finalSettings[setting.Key] = setting.Value;
            }

            var finalizationFacet = new ProducerFinalizerListFacet();
            _finalizerListAssembler(finalizationFacet);
            var finalizerSequence = finalizationFacet.ValidateAndBuild();

            var producer = new Producer(finalSettings);
            foreach (var finalizer in finalizerSequence)
            {
                finalizer(producer);
            }
            return producer;
        }
    }
    internal class ProducerFacet<TKey, TValue> : IProducerFacet<TKey, TValue>
    {
        Action<IIdentityFacet> _identityAssembler = NoOpFactory.NoOp<IIdentityFacet>();
        
        Action<IBrokerListFacet> _brokerListAssembler = NoOpFactory.NoOp<IBrokerListFacet>();
        Action<ISerializationFacet<TKey, TValue>> _serializationAssembler = NoOpFactory.NoOp<ISerializationFacet<TKey, TValue>>();
        Action<ICustomFacet> _customAssembler = NoOpFactory.NoOp<ICustomFacet>();
        Action<IDebugFacet> _debugAssembler = NoOpFactory.NoOp<IDebugFacet>();
        Action<IProducerFinalizerListFacet<TKey, TValue>> _finalizerListAssembler = NoOpFactory.NoOp<IProducerFinalizerListFacet<TKey, TValue>>();

        IProducerFacet<TKey, TValue> IProducerFacet<TKey, TValue>.SetIdentity(Action<IIdentityFacet> assembler)
        {
            _identityAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }

      

        IProducerFacet<TKey, TValue> IProducerFacet<TKey, TValue>.SetBrokers(Action<IBrokerListFacet> assembler)
        {
            _brokerListAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }

        IProducerFacet<TKey, TValue> IProducerFacet<TKey, TValue>.SetSerializers(Action<ISerializationFacet<TKey, TValue>> assembler)
        {
            _serializationAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }

        IProducerFacet<TKey, TValue> IProducerFacet<TKey, TValue>.SetCustom(Action<ICustomFacet> assembler)
        {
            _customAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }

        IProducerFacet<TKey, TValue> IProducerFacet<TKey, TValue>.SetFinalizers(Action<IProducerFinalizerListFacet<TKey, TValue>> assembler)
        {
            _finalizerListAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }
        IProducerFacet<TKey, TValue> IProducerFacet<TKey, TValue>.SetDebug(Action<IDebugFacet> assembler)
        {
            _debugAssembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            return this;
        }
        public Producer<TKey, TValue> ValidateAndBuild()
        {
            var settings = Enumerable.Empty<KeyValuePair<string, object>>();

            var identityFacet = new IdentityFacet();
            _identityAssembler(identityFacet);
            settings=settings.Concat(identityFacet.ValidateAndBuild().ToArray());

            var brokerFacet = new BrokerListFacet();
            _brokerListAssembler(brokerFacet);
            settings=settings.Concat(brokerFacet.ValidateAndBuild().ToArray());

          

            var debugFacet = new DebugFacet();
            _debugAssembler(debugFacet);
            settings=settings.Concat(debugFacet.ValidateAndBuild().ToArray());

            var customFacet = new CustomFacet();
            _customAssembler(customFacet);
            settings=settings.Concat(customFacet.ValidateAndBuild().ToArray());

            var serializerFacet = new SerializationFacet<TKey, TValue>();
            _serializationAssembler(serializerFacet);
            var serializers = serializerFacet.ValidateAndBuild();

            IDictionary<string, object> finalSettings = new Dictionary<string, object>();
            foreach (var setting in settings)
            {
                finalSettings[setting.Key] = setting.Value;
            }

            var finalizationFacet = new ProducerFinalizerListFacet<TKey, TValue>();
            _finalizerListAssembler(finalizationFacet);
            var finalizerSequence = finalizationFacet.ValidateAndBuild();

            var producer = new Producer<TKey, TValue>(finalSettings, serializers.ForKey, serializers.ForValue);
            foreach (var finalizer in finalizerSequence)
            {
                finalizer(producer);
            }
            return producer;
        }


    }
}
