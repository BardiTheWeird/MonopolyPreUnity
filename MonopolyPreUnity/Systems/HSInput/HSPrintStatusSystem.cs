using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.Systems.HSInput
{
    class HSPrintStatusSystem : ISystem
    {
        private readonly Context _context;
        private readonly MonopolyCommand[] _appropriateCommands =
        {
            MonopolyCommand.PrintPlayerStatus,
            MonopolyCommand.PrintGameStatus,
            MonopolyCommand.PrintMap,
        };

        public void Execute()
        {
            var commandChoice = _context.GetComponent<HSCommandChoice>();
            if (commandChoice != null && _appropriateCommands.Contains(commandChoice.Command))
            {
                switch (commandChoice.Command)
                {
                    case MonopolyCommand.PrintPlayerStatus:
                        _context.Add(new PrintPlayerStatus(commandChoice.PlayerId));
                        break;
                    case MonopolyCommand.PrintGameStatus:
                        _context.Add(new PrintGameStatus());
                        break;
                    case MonopolyCommand.PrintMap:
                        _context.Add(new PrintMap());
                        break;
                }
                _context.Remove<HSCommandChoice>(c => c.Command == commandChoice.Command);
            }
        }

        #region ctor
        public HSPrintStatusSystem(Context context) =>
            _context = context;
        #endregion
    }
}
