﻿using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace App.Examples
{
    public class Example3 : AbstractExample
    {
        private readonly IOptionsSnapshot<Settings> _options;

        public Example3(IOptionsSnapshot<Settings> options)
        {
            _options = options;
        }

        public override string Description => "Built-in unbounded channel (using WaitToReadAsync)";

        public override Task RunAsync()
        {
            var channel = Channel.CreateUnbounded<string>();
            var producer = Producer(channel);
            var consumer = Consumer(channel);
            return Task.WhenAll(producer, consumer);
        }

        private async Task Producer(ChannelWriter<string> writer)
        {
            var delay = _options.Value.ProducerDelay;
            var nbrItems = _options.Value.ItemsNumber;

            for(var index = 1; index <= nbrItems; index++)
            {
                var item = $"{index}-{GenerateRandomString()}";
                await writer.WriteAsync(item);
                ConsoleColor.Green.WriteLine($"Item produced: {item}");
                await Task.Delay(delay);
            }

            writer.Complete();
        }

        private async Task Consumer(ChannelReader<string> reader)
        {
            var delay = _options.Value.ConsumerDelay;
            while (await reader.WaitToReadAsync())
            {
                while (reader.TryRead(out var item))
                {
                    ConsoleColor.Blue.WriteLine($"Item consumed: {item}");
                    await Task.Delay(delay);
                }
            }
        }
    }
}
