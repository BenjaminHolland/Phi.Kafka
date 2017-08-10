# Phi.Kafka: A Better Kafka Interface

[![Build status](https://ci.appveyor.com/api/projects/status/1wnqvidk02q04e5q?svg=true)](https://ci.appveyor.com/project/BenjaminHolland/phi-kafka)

This project aims to provide several libraries that will ease using the official Confluent.Kafka library. These libraries are as follows:

## [Phi.Kafka.Fluent](https://www.nuget.org/packages/Phi.Kafka.Fluent/)
**Objective**:
Provides fluent configuration of Confluent.Kafka strongly typed consumers and producers.

**Reasoning**: No one likes `IEnumerable<KeyValuePair<string,object>>`. [librdkafka](https://github.com/edenhill/librdkafka), which is what Confluent.Kafka wraps around,  has a [significant](https://github.com/edenhill/librdkafka/blob/master/CONFIGURATION.md) number of configuration settings, more than you'd probably like to try to memorize or have to look up every time you want to build a consumer or producer. Further, many of the settings are specific to either the Consumer/Producer side of things. Providing specific names allows for easier usage and much better discoverability.

In addition, the boilerplate code for creating a consumer/producer is just a bit on the ugly side. Using a single dictionary is fine, but then keeping separate configuration "zones" means you have to start adding whitespace or `#region`s to separate them. Using multiple dictionaries and merging them is fine, but inelegant. The provided fluent syntax should solve both the aesthetic problem of having a bunch of blocky code, and the conceptual problem of separating unrelated configuration concerns.

Finally, I just think continuation-style configuration is cool.

## [Phi.Kafka.Active](https://www.nuget.org/packages/Phi.Kafka.Active/)
**Objective**:
Provides integration with System.Reactive and System.Interactive, allowing client code to easily treat Kafka consumers as observables enumerables.

**Reasoning**: Confluent.Kafka provides a couple of...less than intuitive methods for retrieving messages. In addition to the problems of working with polled systems in general, I found it a bit funky to deal with for any non-trivial application. Providing the messages via standard C# sequence idioms means less time thinking about what thread your call to Poll is running on and more time thinking about what you actually want to do with your data. 

Providing messages as either an `IEnumerable<Message<TKey,TValue>>` or an `IObservable<Message<TKey,TValue>>` gives pretty good coverage of the possible use cases. *Phi.Active.Kafka* provides these in two flavors, **Active** and **Passive**. **Active** sequences run an automatic polling loop. **Passive** sequences require/allow you to handle the polling yourself, or use one of the overloads to feed in requests for polling.[^1]


## [Phi.Kafka.Fluent.Active](https://www.nuget.org/packages/Phi.Kafka.Fluent.Active/)

**Objective**:
A set of extensions to Phi.Kafka.Fluent that expose the integrations provided by Phi.Kafka.Active during fluent configuration.

**Reasoning**:
This part of the library is provided mostly for completeness and separability. It's a bit goofy to provide finalizers during configuration for one part of the library and not the other, but there may be someone who wants to use the fluent syntax wrap the consumer themselves. This should be possible without pulling in Rx and Ix.

### Notes
Like the Confluent.Kafka library, this library should be considered in flux. While the general gist of each library is more or less stable, specifics should all be regarded as unstable. 

[^1]: This will be explained in documentation that hasn't been written yet. Best check out the source.
