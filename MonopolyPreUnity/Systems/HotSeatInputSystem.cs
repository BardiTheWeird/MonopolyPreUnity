using MonopolyPreUnity.Components.SystemRequest.HotSeatInput;
using MonopolyPreUnity.Components.SystemRequest.HotSeatInput.Choice;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class HotSeatInputSystem : ISystem
    {
        private readonly Context _context;
        private readonly InputParser _inputParser;

        public void Execute()
        {
            var request = _context.GetComponentInterface<IHotSeatRequest>();
            if (request == null || _context.InputString == "")
                return;

            bool success = false;
            switch (request)
            {
                case HotSeatCommandChoiceRequest commandChoiceRequest:
                    success = TryGetCommand(commandChoiceRequest);
                    break;
            }

            if (success)
                _context.Remove(request);

            _context.InputString = "";
        }

        bool TryGetCommand(HotSeatCommandChoiceRequest commandChoiceRequest)
        {
            var commands = commandChoiceRequest.Commands;
            if (!_inputParser.TryParseIndex(commands, out var i))
                return false;
            _context.Add(new HotSeatCommandChoice(commands[i], commandChoiceRequest.PlayerId));
            return true;
        }

        #region ctor
        public HotSeatInputSystem(Context context, InputParser inputParser)
        {
            _context = context;
            _inputParser = inputParser;
        }
        #endregion
    }
}
