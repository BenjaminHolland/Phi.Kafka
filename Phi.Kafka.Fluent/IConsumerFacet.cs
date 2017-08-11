using System;
namespace Phi.Kafka.Fluent
{
    public interface IConsumerFacet<TKey, TValue>
    {
        /// <summary>
        /// Set Consumer Identity
        /// </summary>
        /// <param name="assembler">An action that will configure the identity of this consumer</param>
        /// <returns></returns>
        /// <remarks>This assembler is required</remarks>
        IConsumerFacet<TKey, TValue> SetIdentity(Action<IIdentityFacet> assembler);
        /// <summary>
        /// Set Auto Commit
        /// </summary>
        /// <param name="assembler">An action that will configure the auto commit settings.</param>
        /// <returns></returns>
        IConsumerFacet<TKey, TValue> SetAutoCommit(Action<IAutoCommitFacet> assembler);
        /// <summary>
        /// Set Broker List
        /// </summary>
        /// <param name="assembler">An action that will configure the initial brokers to connect to</param>
        /// <returns></returns>
        /// <remarks>
        /// This assembler is required.
        /// </remarks>
        IConsumerFacet<TKey, TValue> SetBrokers(Action<IBrokerListFacet> assembler);
        /// <summary>
        /// Set Deserialization Processors
        /// </summary>
        /// <param name="assembler">An action that will set the deserializers.</param>
        /// <returns></returns>
        /// <remarks>
        /// This assembler is required.
        /// <para>
        /// The assembler must set a deserializer for all TValue types.
        /// The assembler must set a deserializer for all TKey types unless TKey is Confluent.Kafka.Null
        /// </para>
        /// </remarks>
        IConsumerFacet<TKey, TValue> SetDeserializers(Action<IDeserializationFacet<TKey, TValue>> assembler);
        /// <summary>
        /// Set Custom Settings
        /// </summary>
        /// <param name="assembler">An action that will set the custom settings</param>
        /// <returns></returns>
        /// <remarks>
        /// Custom settings are assigned last during construction, and overwrite pre-existing settings when duplicates are found. Care should be taken to ensure you do not leave the consumer in a strange state.
        /// <para>
        /// This behavior is not final. Please post an issue if you have thoughts on what the "correct" behavior should be.
        /// </para>
        /// </remarks>
        IConsumerFacet<TKey, TValue> SetCustom(Action<ICustomFacet> assembler);
        /// <summary>
        /// Set Debug Settings
        /// </summary>
        /// <param name="assembler">An action that will debug related configuration settings</param>
        /// <returns></returns>
        IConsumerFacet<TKey, TValue> SetDebug(Action<IDebugFacet> assembler);
        /// <summary>
        /// Set Finalization Actions
        /// </summary>
        /// <param name="assembler">An action that will add a list of post-construction actions to run</param>
        /// <returns></returns>
        /// <remarks>
        /// While it's possible to use your own custom delegates to finalize, a number of premade finalizers with "correct" behavior are available from any <see cref="IConsumerFinalizers{TKey, TValue}"/> instance.
        /// </remarks>
        IConsumerFacet<TKey, TValue> SetFinalizers(Action<IConsumerFinalizerListFacet<TKey, TValue>> assembler);
    }
}
