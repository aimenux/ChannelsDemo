﻿using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace App.Examples
{
    public class Example4 : IExample
    {
        private readonly IOptionsSnapshot<Settings> _options;

        public Example4(IOptionsSnapshot<Settings> options)
        {
            _options = options;
        }

        public string Description => "Built-in unbounded channel (using ReadAllAsync)";

        public Task RunAsync()
        {
            var channel = Channel.CreateUnbounded<int>();
            var producer = Producer(channel);
            var consumer = Consumer(channel);
            return Task.WhenAll(producer, consumer);
        }

        private async Task Producer(ChannelWriter<int> writer)
        {
            var delay = _options.Value.ProducerDelay;
            var nbrItems = _options.Value.ItemsNumber;

            for(var item = 1; item <= nbrItems; item++)
            {
                await writer.WriteAsync(item);
                ConsoleColor.Green.WriteLine($"Item produced: {item}");
                await Task.Delay(delay);
            }

            writer.Complete();
        }

        private async Task Consumer(ChannelReader<int> reader)
        {
            var delay = _options.Value.ConsumerDelay;
            await foreach (var item in reader.ReadAllAsync())
            {
                ConsoleColor.Blue.WriteLine($"Item consumed: {item}");
                await Task.Delay(delay);
            }
        }
    }
}
