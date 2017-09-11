using System;
namespace Phi.Kafka.Fluent
{
    public interface IProducerFacet
    {
        IProducerFacet SetIdentity(Action<IIdentityFacet> assembler);
        IProducerFacet SetDebug(Action<IDebugFacet> assembler);
        IProducerFacet SetBrokers(Action<IBrokerListFacet> assembler);
        IProducerFacet SetCustomer(Action<ICustomFacet> assembler);
        IProducerFacet SetFinalizers(Action<IProducerFinalizerListFacet> assembler);
    }
    public interface IProducerFacet<TKey, TValue>
    {
        IProducerFacet<TKey, TValue> SetIdentity(Action<IIdentityFacet> assembler);
        IProducerFacet<TKey, TValue> SetDebug(Action<IDebugFacet> assembler);
        IProducerFacet<TKey, TValue> SetBrokers(Action<IBrokerListFacet> assembler);
        IProducerFacet<TKey, TValue> SetSerializers(Action<ISerializationFacet<TKey, TValue>> assembler);
        IProducerFacet<TKey, TValue> SetCustom(Action<ICustomFacet> assembler);
        IProducerFacet<TKey, TValue> SetFinalizers(Action<IProducerFinalizerListFacet<TKey, TValue>> assembler);
    }
}
