using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace App.Examples
{
    public class Example1 : AbstractExample
    {
        private readonly IOptionsSnapshot<Settings> _options;

        public Example1(IOptionsSnapshot<Settings> options)
        {
            _options = options;
        }

        public override string Description => "Hardcoded channel (using Read)";

        public override Task RunAsync()
        {
            var channel = new TrashChannel<string>();
            var producer = Producer(channel);
            var consumer = Consumer(channel);
            return Task.WhenAll(producer, consumer);
        }

        private async Task Producer(ITrashWriter<string> writer)
        {
            var delay = _options.Value.ProducerDelay;
            var nbrItems = _options.Value.ItemsNumber;

            for(var index = 1; index <= nbrItems; index++)
            {
                var item = $"{index}-{GenerateRandomString()}";
                writer.Write(item);
                ConsoleColor.Green.WriteLine($"Item produced: {index}");
                await Task.Delay(delay);
            }

            writer.Complete();
        }

        private async Task Consumer(ITrashReader<string> reader)
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
