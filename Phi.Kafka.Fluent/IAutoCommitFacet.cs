using System;
namespace Phi.Kafka.Fluent
{
    public interface IAutoCommitFacet
    {
        IAutoCommitFacet Disable();
        IAutoCommitFacet SetInterval(TimeSpan interval);
    }
}
