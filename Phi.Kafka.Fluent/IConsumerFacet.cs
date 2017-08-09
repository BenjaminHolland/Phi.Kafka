using System;
namespace Phi.Kafka.Fluent
{
    public interface IConsumerFacet<TKey, TValue>
    {
        IConsumerFacet<TKey, TValue> SetIdentity(Action<IIdentityFacet> assembler);
        IConsumerFacet<TKey, TValue> SetAutoCommit(Action<IAutoCommitFacet> assembler);
        IConsumerFacet<TKey, TValue> SetBrokers(Action<IBrokerListFacet> assembler);
        IConsumerFacet<TKey, TValue> SetDeserializers(Action<IDeserializationFacet<TKey, TValue>> assembler);
        IConsumerFacet<TKey, TValue> SetCustom(Action<ICustomFacet> assembler);
        IConsumerFacet<TKey, TValue> SetDebug(Action<IDebugFacet> assembler);
        IConsumerFacet<TKey, TValue> SetFinalizers(Action<IConsumerFinalizerListFacet<TKey, TValue>> assembler);
    }
}
