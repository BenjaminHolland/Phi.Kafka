using System;
using System.Collections.Generic;
using Confluent.Kafka;
using Phi.Kafka.Fluent.Resources;
using System.Linq;
namespace Phi.Kafka.Fluent
{
    internal class DebugFacet : IDebugFacet
    {
        private const int DEFAULT_LOG_LEVEL = 6;
        private readonly ISet<string> _contexts = new HashSet<string>();
        private int _level = DEFAULT_LOG_LEVEL;
        private string _threadName;
        IDebugFacet IDebugFacet.AppendContext(string context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (!Library.DebugContexts.Contains(context)) throw new ArgumentException(String.Format(ErrorMessages.InvalidDebugContextMessage, context));
            _contexts.Add(context);
            return this;
        }

        IDebugFacet IDebugFacet.SetLevel(int level)
        {
            _level = level;
            return this;
        }

        public IEnumerable<KeyValuePair<string, object>> ValidateAndBuild()
        {
            if (_contexts.Any())
            {
                yield return new KeyValuePair<string, object>("debug", String.Join(",", _contexts));
                yield return new KeyValuePair<string, object>("log_level", _level);
            }
        }


    }
}
