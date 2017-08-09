using System;
using System.Collections.Generic;
namespace Phi.Kafka.Fluent
{
    internal class CustomFacet : ICustomFacet
    {
        private readonly IDictionary<string, object> _settings = new Dictionary<string, object>();

        ICustomFacet ICustomFacet.AddOrUpdate(IEnumerable<KeyValuePair<string, object>> settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            foreach (var kvp in settings)
            {
                _settings[kvp.Key] = kvp.Value;
            }
            return this;
        }

        ICustomFacet ICustomFacet.AddOrUpdate(KeyValuePair<string, object> setting)
        {
            _settings[setting.Key] = setting.Value;
            return this;

        }

        ICustomFacet ICustomFacet.AddOrUpdate(string name, object value)
        {
            _settings[name] = value;
            return this;
        }
        public IEnumerable<KeyValuePair<string, object>> ValidateAndBuild() => _settings;
    }
}
