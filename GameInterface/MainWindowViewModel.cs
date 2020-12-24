using MonopolyPreUnity;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Initialization;
using siof.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Input;

namespace GameInterface
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region static fields
        static string CheatCodeFormat = @"(\w+)\s+" + "\"" + @"([A-z0-9\s\p{L}]+)" + "\"" + @"\s+" + "\"" + @"([A-z0-9\s]+)" + "\"";
        #endregion

        #region Properties
        string _inputText = "";
        public string InputText
        {
            get => _inputText;
            set
            {
                if (value != _inputText)
                {
                    _inputText = value;
                    RaisePropertyChanged(nameof(InputText));
                }
            }
        }
        #endregion

        #region Context
        Context _context;
        public Context Context
        {
            get => _context;
            set
            {
                if (value != _context)
                {
                    _context = value;
                    RaisePropertyChanged(nameof(Context));
                }
            }
        }
        #endregion

        #region SystemBag
        SystemsBag SysBag { get; set; }
        #endregion

        #region Commands
        public ICommand SendInputCommand { get; set; }
        public ICommand UseCheatCode { get; set; }
        #endregion

        #region backgroundWorker
        BackgroundWorker _backgroundWorker { get; set; }
        #endregion

        #region ctor
        public MainWindowViewModel()
        {
            // initialize
            //Context = MockContext.CreateMockDataSmallTest();
            Context = MockContext.CreateDefaultMapContext();
            SysBag = new SystemsBag(Context.CreateDiContainer().GetAllSystems());

            SendInputCommand = new RelayCommand(x =>
            {
                lock (Context)
                    Context.InputString = InputText;
                InputText = "";
            }, x => InputText.Length > 0 && Context.ContainsComponentInterface<IHSRequest>());
            UseCheatCode = new RelayCommand(x => 
            {
                ExecuteCheatCode(InputText);
                InputText = "";
            }, x => IsCheatCodeFormat(InputText));

            _backgroundWorker = new BackgroundWorker { WorkerSupportsCancellation = true };
            _backgroundWorker.DoWork += (x, y) => MonopolyEntry.RunSystemsContinuousAsync(SysBag);
            _backgroundWorker.RunWorkerAsync();
        }
        #endregion

        #region cheat codes
        bool IsCheatCodeFormat(string input) =>
            Regex.IsMatch(input, CheatCodeFormat);

        void ExecuteCheatCode(string input)
        {
            foreach (var line in input.Split('\n'))
            {
                var codeMatch = Regex.Match(line, CheatCodeFormat);
                var command = codeMatch.Groups[1].Value;
                var parameter1 = codeMatch.Groups[2].Value;
                var parameter2 = codeMatch.Groups[3].Value;

                Context.Add(new PrintLine($"Cheat code to be executed: {command} {parameter1} {parameter2}",
                    OutputStream.GameLog));

                string errorMessage = null;
                var executed = false;

                switch (command.ToLower())
                {
                    case "givecash":
                        var player = NameToPlayer(parameter1);
                        if (player == null)
                        {
                            errorMessage = $"No player named {parameter1} found";
                            break;
                        }
                        if (!int.TryParse(parameter2, out var amount))
                        {
                            errorMessage = $"Couldn't convert {parameter2} to integer";
                            break;
                        }

                        Context.Add(new GiveCash(amount, player.Id, "cheetah"));
                        executed = true;
                        break;

                    case "chargecash":
                        player = NameToPlayer(parameter1);
                        if (player == null)
                        {
                            errorMessage = $"No player named {parameter1} found";
                            break;
                        }
                        if (!int.TryParse(parameter2, out amount))
                        {
                            errorMessage = $"Couldn't convert {parameter2} to integer";
                            break;
                        }

                        Context.Add(new ChargeCash(amount, player.Id, message: "cheetah"));
                        Context.HSInputState().Nullify();
                        Context.Add(new ClearOutput());
                        Context.RemoveInterface<IHSRequest>();
                        executed = true;
                        break;

                    case "transferproperty":
                        var tileId = NameToPropertyId(parameter1);
                        if (!tileId.HasValue)
                        {
                            errorMessage = $"No property named {parameter1} found";
                            break;
                        }
                        player = NameToPlayer(parameter2);
                        if (player == null)
                        {
                            errorMessage = $"No player named {parameter2} found";
                        }

                        Context.Add(new PropertyTransferRequest(tileId.Value, player.Id));
                        executed = true;
                        break;

                    case "buildhouses":
                        var propId = NameToPropertyId(parameter1);
                        if (!propId.HasValue)
                        {
                            errorMessage = $"No property named {parameter1} found";
                            break;
                        }
                        var dev = Context.GetTileComponent<PropertyDevelopment>(propId.Value);
                        if (dev == null)
                        {
                            errorMessage = $"{parameter1} doesn't contain a PropertyDevelopmentComponent";
                            break;
                        }
                        if (!int.TryParse(parameter2, out var additionalHouses))
                        {
                            errorMessage = $"{parameter2} is not an integer";
                            break;
                        }
                        dev.HousesBuilt = Math.Min(dev.HousesBuilt + additionalHouses, dev.HouseCap);

                        executed = true;
                        Context.Add(new PrintFormattedLine($"{additionalHouses} were built on |tile:{propId.Value}|; " +
                            $"total houses built: {dev.HousesBuilt}", OutputStream.GameLog));
                        break;

                    case "printplayersleft":
                        _context.Add(new ClearOutput());
                        var choice = Convert.ToInt32(parameter1) == 0;
                        if (choice)
                            _context.Add(new PrintLine($"{_context.TurnInfo().PlayersLeft} players left", OutputStream.HSInputLog));
                        else
                            _context.Add(new PrintGameStatus());
                        break;
                }

                if (!executed)
                {
                    errorMessage = errorMessage ?? $"Command {command} not found";
                    _context.Add(new PrintLine($"Code couldn't be executed: {errorMessage}", OutputStream.GameLog));
                }
                else
                    _context.Add(new PrintLine($"Code executed", OutputStream.GameLog));
            }
        }

        Player NameToPlayer(string name) =>
            _context.GetAllPlayers().FirstOrDefault(p => p.DisplayName == name);

        int? NameToPropertyId(string name)
        {
            var tile = Context.GetComponent<Tile>(t => t.Name == name);
            if (tile == null)
                return null;

            if (!tile.ContainsComponent(typeof(Property), Context))
                return null;

            return tile.Id;
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
