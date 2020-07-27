using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace App.Examples
{
    public class Example6 : AbstractExample
    {
        private readonly IOptionsSnapshot<Settings> _options;

        public Example6(IOptionsSnapshot<Settings> options)
        {
            _options = options;
        }

        public override string Description => "Built-in bounded channel (using ReadAllAsync)";

        public override Task RunAsync()
        {
            var size = _options.Value.BoundedChannelSize;
            var channel = Channel.CreateBounded<string>(size);
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
            await foreach (var item in reader.ReadAllAsync())
            {
                ConsoleColor.Blue.WriteLine($"Item consumed: {item}");
                await Task.Delay(delay);
            }
        }
    }
}
