using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HotSeatScenario
{
    class ConsoleUI
    {
        #region Dependencies
        private readonly TileManager _tileManager;
        #endregion

        #region Input
        public T InputValue<T>() where T : IConvertible
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    return (T)Convert.ChangeType(input, typeof(T));
                }
                catch
                {
                    Console.WriteLine("Invalid input. Try again");
                }
            }
        }

        public T InputValue<T>(Func<T, bool> pred, string errorMessage) where T : IConvertible
        {
            while (true)
            {
                var val = InputValue<T>();
                if (pred(val))
                    return val;
                Console.WriteLine(errorMessage + ". Try again");
            }
        }

        public T InputValue<T>(IEnumerable<T> possibleValues) where T : IConvertible =>
            InputValue<T>(x => possibleValues.Contains(x), "Value not in range");

        public int InputValueIndex<T>(IEnumerable<T> values) =>
            InputValue<int>(x => 0 <= x && x < values.Count(), "Index out of range");
        #endregion

        #region Output
        public string CommandName(MonopolyCommand command)
        {
            var commandName = command.ToString();
            var lastDotIndex = commandName.LastIndexOf('.');
            return commandName.Substring(lastDotIndex == -1 ? 0 : lastDotIndex);
        }
        public void WriteCommands(List<MonopolyCommand> commands)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {CommandName(commands[i])}");
            }
        }
        #endregion

        #region Specific
        public MonopolyCommand ChooseCommand(List<MonopolyCommand> commands)
        {
            Console.WriteLine("Choose command:");
            WriteCommands(commands);
            return commands[InputValueIndex(commands)];
        }
        #endregion
    }
}
