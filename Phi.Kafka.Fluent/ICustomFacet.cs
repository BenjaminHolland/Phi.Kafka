using System.Collections.Generic;
namespace Phi.Kafka.Fluent
{
    public interface ICustomFacet
    {
        ICustomFacet AddOrUpdate(IEnumerable<KeyValuePair<string, object>> settings);
        ICustomFacet AddOrUpdate(KeyValuePair<string, object> setting);
        ICustomFacet AddOrUpdate(string name, object value);
    }
}
