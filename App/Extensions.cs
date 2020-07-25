using System;

namespace App
{
    public static class Extensions
    {
        private static readonly object Locker = new object();

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
