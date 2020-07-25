using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace App.Examples
{
    public class Example1 : IExample
    {
        private readonly IOptionsSnapshot<Settings> _options;

        public Example1(IOptionsSnapshot<Settings> options)
        {
            _options = options;
        }

        public string Description => "Hardcoded channel (using Read)";

        public Task RunAsync()
        {
            var channel = new TrashChannel<int>();
            var producer = Producer(channel);
            var consumer = Consumer(channel);
            return Task.WhenAll(producer, consumer);
        }

        private async Task Producer(ITrashWriter<int> writer)
        {
            var delay = _options.Value.ProducerDelay;
            var nbrItems = _options.Value.ItemsNumber;

            for(var item = 1; item <= nbrItems; item++)
            {
                writer.Write(item);
                ConsoleColor.Green.WriteLine($"Item produced: {item}");
                await Task.Delay(delay);
            }

            writer.Complete();
        }

        private async Task Consumer(ITrashReader<int> reader)
        {
            var delay = _options.Value.ConsumerDelay;
            while (!reader.IsComplete())
            {
                var item = await reader.ReadAsync();
                ConsoleColor.Blue.WriteLine($"Item consumed: {item}");
                await Task.Delay(delay);
            }
        }
    }
}
