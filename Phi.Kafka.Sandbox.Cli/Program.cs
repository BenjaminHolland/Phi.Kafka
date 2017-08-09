using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phi.Kafka.Fluent;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System.Threading;
using Phi.Kafka.Fluent.Active;
using System.Reactive.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Concurrency;

namespace Phi.Kafka.Sandbox.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopSignal = new CancellationTokenSource();

            var stopToken = stopSignal.Token;

            var manualStopSignal = Task.Run(() =>
            {
                Console.ReadKey();

            }).ToObservable();

            IConsumerFactory<Null, string> consumerFactory = DefaultConsumerFactory<Null, string>.Instance;
            IConsumerFinalizers<Null, string> consumerFinalizers = ConsumerFinalizers<Null, string>.Instance;
            IObservable<Message<Null, string>> messageStream = null;
            
            var messageConsumer = consumerFactory.Create(consumer => consumer
                  .SetIdentity(identity => identity
                      .SetGroup("Phi.Groups.Strings")
                      .SetName("Phi.Clients.Strings.1"))
                  .SetBrokers(brokers => brokers
                      .AppendHost("192.168.1.11:9092"))
                  .SetDeserializers(deserializers => deserializers
                      .SetKeyDeserializer(null)
                      .SetValueDeserializer(new StringDeserializer(Encoding.UTF8)))
                  .SetDebug(debug => debug
                      .AppendContext("all"))
                  .SetFinalizers(finalizers => finalizers
                      .AppendFinalizer(consumerFinalizers.Subscribe("Phi.Topics.Strings",Offset.Beginning))
                      .AppendFinalizer(consumerFinalizers.ObserveMessagesActive(manualStopSignal,result=>messageStream=result))));

            IProducerFactory<Null, string> producerFactory = DefaultProducerFactory<Null, string>.Instance;
            IProducerFinalizers<Null, string> producerFinalizers = ProducerFinalizers<Null, string>.Instance;
            var messageProducer = producerFactory.Create(producer => producer
                  .SetIdentity(identity => identity
                      .SetGroup("Phi.Groups.Strings")
                      .SetName("Phi.Servers.Strings"))
                  .SetBrokers(brokers => brokers
                      .AppendHost("192.168.1.11:9092"))
                  .SetSerializers(serializers => serializers
                      .SetKeySerializer(null)
                      .SetValueSerializer(new StringSerializer(Encoding.UTF8)))
                  .SetDebug(debug => debug
                      .AppendContext("all")));

         
            
            var producerTask = Task.Run(async () =>
            {
                int ticker = 0;
                while (!stopToken.IsCancellationRequested)
                {
                    await Task.Delay(1000);
                    await messageProducer.ProduceAsync("Phi.Topics.Strings", null, "Hello World!");
                    if (((ticker ++) % 5) == 0)
                    {
                  
                       
                    }
                }
            });
            
            messageStream.Subscribe(msg =>
            {
                Console.WriteLine();
                Console.WriteLine(msg.Value);
                Console.WriteLine();
            });



            manualStopSignal.GetAwaiter().GetResult();
            producerTask.GetAwaiter().GetResult();

            messageProducer.Dispose();
            messageConsumer.Dispose();
            stopSignal.Dispose();
        }
    }
}
