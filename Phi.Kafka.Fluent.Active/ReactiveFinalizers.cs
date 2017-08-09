using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phi.Kafka.Active;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;

namespace Phi.Kafka.Fluent.Active
{
    public static class ReactiveFinalizers
    {
        public static Action<Consumer<TKey, TValue>> ObserveMessagesActive<TKey, TValue>(this IConsumerFinalizers<TKey, TValue> shim, IObservable<Unit> disconnect, Action<IObservable<Message<TKey, TValue>>> setResult) =>
            consumer => setResult(consumer.ObserveMessagesActive(disconnect));

        public static Action<Consumer<TKey, TValue>> ObserveMessagesPassive<TKey, TValue>(this IConsumerFinalizers<TKey, TValue> shim, IObservable<Unit> disconnect, Action<IObservable<Message<TKey, TValue>>> setResult) =>
            consumer => setResult(consumer.ObserveMessagesPassive(disconnect));

        public static Action<Consumer<TKey, TValue>> ObserveMessagesPassive<TKey, TValue>(this IConsumerFinalizers<TKey, TValue> shim, CancellationToken disconnect, Action<IObservable<Message<TKey, TValue>>> setResult) =>
            shim.ObserveMessagesPassive(disconnect.ToObservable(), setResult);

        public static Action<Consumer<TKey, TValue>> ObserveMessagesPassive<TKey, TValue>(this IConsumerFinalizers<TKey, TValue> shim, Action<IObservable<Message<TKey, TValue>>> setResult) =>
            shim.ObserveMessagesPassive(Observable.Never<Unit>(), setResult);
    }
}
