using System;
using System.Collections.Generic;
namespace Phi.Kafka.Fluent
{
    internal class IdentityFacet : IIdentityFacet
    {
        private string _groupId;
        private string _clientId;
        IIdentityFacet IIdentityFacet.SetGroup(string groupId)
        {
            _groupId = groupId ?? throw new ArgumentNullException(nameof(groupId));
            return this;
        }

        IIdentityFacet IIdentityFacet.SetName(string clientId)
        {
            _clientId = clientId;
            return this;
        }
        public IEnumerable<KeyValuePair<string, object>> ValidateAndBuild()
        {
            yield return new KeyValuePair<string, object>("group.id", _groupId);
            if (_clientId != null)
            {
                yield return new KeyValuePair<string, object>("client.id", _clientId);
            }
        }
    }
}
