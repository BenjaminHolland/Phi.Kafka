using System;
using System.Collections.Generic;
using System.Linq;
using Confluent.Kafka;
using System.Reactive.Linq;
using System.Reactive;
using System.Threading;

namespace Phi.Kafka.Active
{
    public static class Enumerables
    {


        public static IEnumerable<Message<TKey, TValue>> EnumerateMessagesPassive<TKey, TValue>(this Consumer<TKey, TValue> @this) => 
            @this.ObserveMessagesPassive(Observable.Never<Unit>()).Next();

        public static IEnumerable<Message<TKey, TValue>> EnumerateMessagesPassive<TKey, TValue>(this Consumer<TKey, TValue> @this, CancellationToken disconnect) =>
            @this.ObserveMessagesPassive(disconnect).Next();

        public static IEnumerable<Message<TKey, TValue>> EnumerateMessagesPassive<TKey, TValue>(this Consumer<TKey, TValue> @this, IObservable<Unit> disconnect) =>
            @this.ObserveMessagesPassive(disconnect).Next();

        public static IEnumerable<Message<TKey, TValue>> EnumerateMessagesActive<TKey, TValue>(this Consumer<TKey, TValue> @this)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            IEnumerator<Message<TKey,TValue>> CreateEnumerator()
            {
                while (@this.Consume(out Message<TKey, TValue> message, Timeout.Infinite))
                    yield return message;
            }
            return EnumerableEx.Create(CreateEnumerator);
        }

    }
}
