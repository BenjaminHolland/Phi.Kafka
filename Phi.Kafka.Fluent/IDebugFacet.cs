namespace Phi.Kafka.Fluent
{
    public interface IDebugFacet
    {
        IDebugFacet AppendContext(string context);
        IDebugFacet SetLevel(int level);

    }
}
