namespace Phi.Kafka.Fluent
{
    public interface IBrokerListFacet
    {
        /// <summary>
        /// Add A Host
        /// </summary>
        /// <param name="host">The host of the port.</param>
        /// <returns></returns>
        /// <remarks>No validation other than null checks are done on this argument. GIGO.</remarks>
        IBrokerListFacet AppendHost(string host);
    }
}
