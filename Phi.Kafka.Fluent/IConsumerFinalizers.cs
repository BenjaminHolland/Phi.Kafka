namespace Phi.Kafka.Fluent
{
    /// <summary>
    /// Consumer Finalizer Shim
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <remarks>
    /// This is simply an extension point. Actual finalizer factory methods are provided as extensions methods to this interface.
    /// To get an instance of this interface, use <see cref="ConsumerFinalizers{TKey, TValue}.Instance"/>
    /// </remarks>
    public interface IConsumerFinalizers<TKey,TValue>
    {

    }
}
