# Phi.Kafka: A Better Kafka Interface

This project aims to provide several libraries that will ease using the official Confluent.Kafka library. These libraries are as follows:

## Phi.Kafka.Fluent
Provides fluent configuration of Confluent.Kafka strongly typed consumers and producers. 

## Phi.Kafka.Active
Provides integration with System.Reactive and System.Interactive, allowing client code to easily treat Kafka consumers as observables enumerables.


## Phi.Kafka.Fluent.Active: COMING VERY SOON
A set of extensions to Phi.Kafka.Fluent that expose the integrations provided by Phi.Kafka.Active during fluent configuration.

### Notes
Like the Confluent.Kafka library, this library should be considered in flux. While the general gist of each library is more or less stable, specifics should all be regarded as unstable. 
