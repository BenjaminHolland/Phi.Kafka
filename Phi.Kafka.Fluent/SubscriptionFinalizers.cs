using System;
using System.Collections.Generic;
using Confluent.Kafka;
using System.Linq;
namespace Phi.Kafka.Fluent
{
    public static class SubscriptionFinalizers
    {
        public static Action<Consumer<TKey,TValue>> Subscribe<TKey,TValue>(this IConsumerFinalizers<TKey,TValue> shim,string topic)
        {
            return consumer =>
            {
                consumer.Subscribe(topic);
            };
        }
        public static Action<Consumer<TKey,TValue>> Subscribe<TKey,TValue>(this IConsumerFinalizers<TKey,TValue> shim,string topic,Offset initialPosition)
        {
            return consumer =>
            {
                void InterceptAssignments(object sender, List<TopicPartition> assignments)
                {
                    consumer.OnPartitionsAssigned -= InterceptAssignments;
                    var alteredAssignments = from tp in assignments
                                             select new TopicPartitionOffset(tp, tp.Topic.Equals(topic) ? initialPosition : Offset.Invalid);
                    consumer.Assign(alteredAssignments);
                }
                consumer.OnPartitionsAssigned += InterceptAssignments;
                consumer.Subscribe(topic);
            };
        }

    }
}
