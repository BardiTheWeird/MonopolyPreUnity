using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class HSInputSystem : ISystem
    {
        private readonly Context _context;
        private readonly InputParser _inputParser;

        public void Execute()
        {
            var request = _context.GetComponentInterface<IHSRequest>();
            if (request == null || _context.InputString == "")
                return;

            _context.Logger.AppendLine($"\n>> {_context.InputString}\n\n");

            bool success = false;
            switch (request)
            {
                case HSCommandChoiceRequest commandChoiceRequest:
                    success = TryGetCommand(commandChoiceRequest);
                    break;
            }

            if (success)
                _context.Remove(request);

            _context.InputString = "";
        }

        bool TryGetCommand(HSCommandChoiceRequest commandChoiceRequest)
        {
            var commands = commandChoiceRequest.Commands;
            if (!_inputParser.TryParseIndex(commands, out var i))
                return false;
            _context.Add(new HSCommandChoice(commands[i], commandChoiceRequest.PlayerId));
            return true;
        }

        #region ctor
        public HSInputSystem(Context context, InputParser inputParser)
        {
            _context = context;
            _inputParser = inputParser;
        }
        #endregion
    }
}
