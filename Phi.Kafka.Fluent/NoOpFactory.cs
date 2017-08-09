using System;
using System.Collections.Generic;
namespace Phi.Kafka.Fluent
{
    internal static class NoOpFactory
    {
        private static readonly IDictionary<Type, object> _cache = new Dictionary<Type, object>();
        public static Action<T> NoOp<T>() => (Action<T>)(_cache.ContainsKey(typeof(T)) ? _cache[typeof(T)] : _cache[typeof(T)] = new Action<T>(_ => { }));
    }
}
