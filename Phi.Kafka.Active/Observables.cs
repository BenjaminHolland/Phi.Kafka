using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Collections.Concurrent;
using System.Reactive;
using System.Threading;
using System.Reactive.Concurrency;

namespace Phi.Kafka.Active
{
    public static class Observables
    {
        /// <summary>
        /// Start a manual message pump.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="disconnect">Signals the pump should stop responding to elements with Poll dispatches.</param>
        /// <param name="nextPollDuration">An observable that feeds poll times into the pump.</param>
        /// <param name="loopScheduler">Specifies the scheduler that should be used to schedule polls on</param>
        /// <returns></returns>
        public static IObservable<Unit> RunManualMessagePump<TKey,TValue>(this Consumer<TKey,TValue> @this,IObservable<Unit> disconnect,IObservable<TimeSpan> nextPollDuration, IScheduler loopScheduler)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            disconnect = disconnect ?? Observable.Never<Unit>();
            if (nextPollDuration == null) throw new ArgumentNullException(nameof(nextPollDuration));
            loopScheduler = loopScheduler ?? TaskPoolScheduler.Default;
            return nextPollDuration.TakeUntil(disconnect).Select(ts => Observable.Start(() => @this.Poll(ts), loopScheduler)).Concat();
        }
        /// <summary>
        /// Start an automatic message pump
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="disconnect">Signals the pump should stop polling.</param>
        /// <param name="pollTimeout">The time duration of each Poll iteration.</param>
        /// <param name="loopScheduler">The scheduler on which to schedule poll calls.</param>
        /// <returns></returns>
        public static IObservable<Unit> RunAutomaticMessagePump<TKey,TValue>(this Consumer<TKey,TValue> @this,IObservable<Unit> disconnect,TimeSpan pollTimeout,IScheduler loopScheduler)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            disconnect = disconnect ?? Observable.Never<Unit>();
            if (pollTimeout < TimeSpan.Zero && (pollTimeout != Timeout.InfiniteTimeSpan)) throw new ArgumentOutOfRangeException(nameof(pollTimeout));
            loopScheduler = loopScheduler ?? TaskPoolScheduler.Default;
            return Observable.Defer(() => Observable.Start(() => @this.Poll(TimeSpan.FromSeconds(1)))).Repeat().TakeUntil(disconnect);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="disconnect"></param>
        /// <returns></returns>
        public static IObservable<Message<TKey, TValue>> ObserveMessagesActive<TKey, TValue>(this Consumer<TKey, TValue> @this, IObservable<Unit> disconnect)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            disconnect = disconnect ?? Observable.Never<Unit>();

            var messagesPassive = @this.ObserveMessagesPassive(disconnect);
            var pump = Observable.Defer(() => Observable.Start(() => { Console.WriteLine("Polling"); @this.Poll(TimeSpan.FromSeconds(1)); })).Repeat().TakeUntil(disconnect);
            return Observable.Create<Message<TKey, TValue>>(obs =>
            {
                var composite = new CompositeDisposable(2);
                composite.Add(messagesPassive.Subscribe(obs));
                composite.Add(pump.Subscribe(_=> { }));
                return composite;
            });
        }
                                                         
        public static IObservable<Message<TKey, TValue>> ObserveMessagesPassive<TKey, TValue>(this Consumer<TKey, TValue> @this, IObservable<Unit> disconnect)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            return Observable
                .FromEventPattern<Message<TKey, TValue>>(@this, nameof(@this.OnMessage))
                .TakeUntil(disconnect)
                .Select(evt => evt.EventArgs);
        }
        public static IObservable<Message<TKey, TValue>> ObserveMessagesPassive<TKey, TValue>(this Consumer<TKey, TValue> @this) =>
            @this.ObserveMessagesPassive(Observable.Never<Unit>());
        public static IObservable<Message<TKey, TValue>> ObserveMessagesPassive<TKey, TValue>(this Consumer<TKey, TValue> @this, CancellationToken disconnect) =>
            @this.ObserveMessagesPassive(disconnect.ToObservable());
    }
}
