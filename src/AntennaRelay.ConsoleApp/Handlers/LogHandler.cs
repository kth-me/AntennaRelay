using System;

namespace AntennaRelay.ConsoleApp.Handlers
{
    public class LogHandler
    {
        public void Neutral(string message) => LogLogic(message, ConsoleColor.Gray);

        public void Good(string message) => LogLogic(message, ConsoleColor.Green);

        public void Bad(string message) => LogLogic(message, ConsoleColor.Red);

        public void Info(string message) => LogLogic(message, ConsoleColor.Cyan);

        public void Alert(string message) => LogLogic(message, ConsoleColor.Yellow);

        public void Update(string message) => LogLogic(message, ConsoleColor.Magenta);

        private static void LogLogic(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm:ss}]");
            Console.ResetColor();
            Console.WriteLine($" {message}");
        }
    }
}