using System;
namespace Phi.Kafka.Fluent
{
    public interface IConsumerFacet
    {
        IConsumerFacet SetIdentity(Action<IIdentityFacet> assembler);
        IConsumerFacet SetAutoCommit(Action<IAutoCommitFacet> assembler);
        IConsumerFacet SetBrokers(Action<IBrokerListFacet> assmebler);
        IConsumerFacet SetCustom(Action<ICustomFacet> assembler);
        IConsumerFacet SetDebug(Action<IDebugFacet> assembler);
        IConsumerFacet SetFinalizers(Action<IConsumerFinalizerListFacet> assembler);

    }
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
