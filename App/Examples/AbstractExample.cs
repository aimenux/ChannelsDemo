using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Examples
{
    public abstract class AbstractExample : IExample
    {
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());

        public abstract string Description { get; }
        public abstract Task RunAsync();

        protected string GenerateRandomString(int length = 30)
        {
            const string chars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var charsArray = Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)])
                .ToArray();
            return new string(charsArray);
        }
    }
}
