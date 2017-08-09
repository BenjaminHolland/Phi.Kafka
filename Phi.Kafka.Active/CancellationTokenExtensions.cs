using System;
using System.Reactive.Linq;
using System.Reactive;
using System.Threading;

namespace Phi.Kafka.Active
{
    public static class CancellationTokenExtensions
    {
        public static IObservable<Unit> ToObservable(this CancellationToken cancellationToken)
        {
            return Observable.Create<Unit>(obs => cancellationToken.Register(() => { obs.OnNext(Unit.Default); obs.OnCompleted(); }));
        }
    }
}
