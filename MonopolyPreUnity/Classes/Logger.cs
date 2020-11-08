using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Tracing;
using System.Diagnostics;

namespace MonopolyPreUnity.Classes
{
    static class Logger
    {
        public static void Log(string message)
        {
            Debug.WriteLine(message);
        }
        public static void Log(int playerId, string message)
        {
            Debug.WriteLine($"Player {playerId} " + message);
        }
    }
}
