using Autofac.Features.Indexed;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Choice;
using MonopolyPreUnity.Components.SystemRequest.HSInput.Request;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Systems.HSInput;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MonopolyPreUnity.Systems
{
    class HSInputSystem : ISystem
    {
        private readonly Context _context;
        private readonly InputParser _inputParser;
        private readonly IIndex<HSState, IHSStateBehavior> _index; 

        public void Execute()
        {
            var state = _context.HSInputState();
            if (state.CurState == null)
                return;

            ParseInput();

            // execute an appropriate HSInputSystem
            _index[state.CurState.Value].Run(state);
        }

        #region parsing input
        void ParseInput()
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
                case HSPropertyChoiceRequest propertyChoiceRequest:
                    success = TryGetPropertyId(propertyChoiceRequest);
                    break;
                case HSIntRequest intRequest:
                    success = TryGetInt(intRequest);
                    break;
            }

            if (success)
            {
                _context.Remove(request);
                Debug.WriteLine($"Request of type {request.GetType().ShortTypeString()} was removed");
            }

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

        bool TryGetPropertyId(HSPropertyChoiceRequest propertyChoiceRequest)
        {
            var props = propertyChoiceRequest.Properties;
            if (!_inputParser.TryParseIndex(props, out var index, canCancel: true))
                return false;

            var propId = index == -1 ? null : (int?)props[index];
            _context.Add(new HSPropertyChoice(propertyChoiceRequest.PlayerId, propId));
            return true;
        }

        bool TryGetInt(HSIntRequest intRequest)
        {
            if (!_inputParser.TryParse<int>(x => intRequest.LowerBound <= x && x <= intRequest.UpperBound,
                out var val, $"Value should be betwen {intRequest.LowerBound} and {intRequest.UpperBound}"))
                return false;

            _context.Add(new HSIntChoice(intRequest.PlayerId, val));
            Debug.WriteLine("Int choice was added");
            return true;
        }
        #endregion

        #region ctor
        public HSInputSystem(Context context, InputParser inputParser, IIndex<HSState, IHSStateBehavior> index)
        {
            _context = context;
            _inputParser = inputParser;
            _index = index;
        }
        #endregion
    }
}
