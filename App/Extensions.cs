using System;
using System.Threading;

namespace App
{
    public static class Extensions
    {
        private static readonly Lock Locker = new();

        public static void WriteLine(this ConsoleColor color, object value)
        {
            lock (Locker)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(value);
                Console.ResetColor();
            }
        }
    }
}
