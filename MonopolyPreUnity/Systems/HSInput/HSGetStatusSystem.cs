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
    class HSGetStatusSystem : ISystem
    {
        private readonly Context _context;
        private readonly MonopolyCommand[] _appropriateCommands =
        {
            MonopolyCommand.GetPlayerStatus,
            MonopolyCommand.GetGameStatus,
        };

        public void Execute()
        {
            var commandChoice = _context.GetComponent<HSCommandChoice>();
            if (commandChoice != null && _appropriateCommands.Contains(commandChoice.Command))
            {
                switch (commandChoice.Command)
                {
                    case MonopolyCommand.GetPlayerStatus:
                        _context.Add(new PrintPlayerStatus(commandChoice.PlayerId));
                        break;
                    case MonopolyCommand.GetGameStatus:
                        _context.Add(new PrintGameStatus());
                        break;
                }
                _context.Remove<HSCommandChoice>(c => c.Command == commandChoice.Command);
            }
        }

        #region ctor
        public HSGetStatusSystem(Context context) =>
            _context = context;
        #endregion
    }
}
