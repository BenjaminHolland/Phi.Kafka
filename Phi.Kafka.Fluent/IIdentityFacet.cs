namespace Phi.Kafka.Fluent
{
    public interface IIdentityFacet
    {
        IIdentityFacet SetGroup(string groupId);
        IIdentityFacet SetName(string clientId);
    }
}
