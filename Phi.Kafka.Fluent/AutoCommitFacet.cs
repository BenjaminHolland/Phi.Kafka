using System;
using System.Collections.Generic;
using Phi.Kafka.Fluent.Resources;
namespace Phi.Kafka.Fluent
{
    internal class AutoCommitFacet : IAutoCommitFacet
    {
        private static readonly TimeSpan MAX_INTERVAL = TimeSpan.FromMilliseconds(86400000);
        private static readonly TimeSpan MIN_INTERVAL = TimeSpan.Zero;
        private static readonly TimeSpan DEFAULT_INTERVAL = TimeSpan.FromMilliseconds(5000);
        private bool _isEnabled = true;
        private TimeSpan _interval = DEFAULT_INTERVAL;
        IAutoCommitFacet IAutoCommitFacet.Disable()
        {
            _isEnabled = false;
            return this;
        }

        IAutoCommitFacet IAutoCommitFacet.SetInterval(TimeSpan interval)
        {
            if (interval < MIN_INTERVAL || interval > MAX_INTERVAL) throw new ArgumentOutOfRangeException(nameof(interval));
            if (!_isEnabled) throw new InvalidOperationException(ErrorMessages.AutoCommitDisabledMessage);
            _interval = interval;
            return this;
        }
        public IEnumerable<KeyValuePair<string, object>> ValidateAndBuild()
        {
            if (_isEnabled)
            {
                yield return new KeyValuePair<string, object>("enable.auto.commit", false);
            }
            else
            {
                yield return new KeyValuePair<string, object>("auto.commit.interval", (int)_interval.TotalMilliseconds);
            }
        }
    }
}
