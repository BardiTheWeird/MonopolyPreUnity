using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    static class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }
        public static void Log(int playerId, string message)
        {
            Console.WriteLine($"Player {playerId} " + message);
        }
    }
}
