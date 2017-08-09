using System;
using System.Collections.Generic;
using Phi.Kafka.Fluent.Resources;
using System.Linq;

namespace Phi.Kafka.Fluent
{
    internal class BrokerListFacet : IBrokerListFacet
    {
        private readonly ISet<string> _brokers = new HashSet<string>();

        IBrokerListFacet IBrokerListFacet.AppendHost(string host)
        {
            if (host == null) throw new ArgumentNullException(nameof(host));
            _brokers.Add(host);
            return this;
        }
        public IEnumerable<KeyValuePair<string, object>> ValidateAndBuild()
        {
            if (!_brokers.Any()) throw new InvalidOperationException(ErrorMessages.NoBrokersMessage);
            yield return new KeyValuePair<string, object>("bootstrap.servers", String.Join(",", _brokers));
        }
    }
}
