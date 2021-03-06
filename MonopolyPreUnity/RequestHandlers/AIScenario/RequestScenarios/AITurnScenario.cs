﻿using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.Jail;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components.Trade;
using System.Diagnostics;

namespace MonopolyPreUnity.RequestHandlers.AIScenario.RequestScenarios
{
    class AITurnScenario : IAIRequestScenario
    {
        private readonly Context _context;

        public void RunScenario(IRequest request, Player player, AiInfo aiInfo)
        {
            Debug.WriteLine($"Running the AITurnScenario for {player.DisplayName}");
            var jailCommands = _context.GetAvailableJailCommands(player);
            if (jailCommands.Count > 0)
            {
                if (jailCommands.Contains(MonopolyCommand.UseJailCard))
                    _context.AddCommand(MonopolyCommand.UseJailCard, player);
                else
                {
                    if (jailCommands.Count == 1)
                        _context.AddCommand(jailCommands[0], player);
                    else
                    {
                        var jailFine = _context.GameConfig().JailFine;
                        var weights = new List<(MonopolyCommand, int)>
                        {
                            (MonopolyCommand.JailRollDice, 6),
                            (MonopolyCommand.PayJailFine, (int)Math.Pow(2, player.Cash / jailFine))
                        };
                        _context.AddCommand(weights.ChaosChoice(aiInfo.ChaosFactor), player);
                    }
                }
                return;
            }

            if (player.CanMove)
            {
                _context.AddCommand(MonopolyCommand.MakeMove, player);
                return;
            }

            PropertyActions(player, aiInfo);
            if (ProposedTrade(player, aiInfo))
                return;

            Debug.WriteLine("Got to AddCommand(EndTurn)");
            _context.AddCommand(MonopolyCommand.EndTurn, player);
            aiInfo.Nullify();
        }

        void PropertyActions(Player player, AiInfo aiInfo)
        {
            if (aiInfo.DidPropertyActionsThisTurn)
                return;

            var availableCash = player.Cash;
            var propIds = player.Properties;
            var config = _context.GameConfig();

            // unmortgage
            var mortgagedPropIds = propIds
                .Where(x => _context.GetTileComponent<Property>(x).IsMortgaged)
                .OrderByDescending(x => _context.GetTileComponent<Property>(x).BasePrice);

            if (mortgagedPropIds.Count() > 0)
            {
                foreach (var propId in mortgagedPropIds)
                {
                    var prop = _context.GetTileComponent<Property>(propId);
                    var unmortgageFee = (int)(prop.BasePrice * config.MortgageFee * (1 + config.MortgageCommission));

                    if (unmortgageFee > availableCash)
                        continue;

                    var weights = new List<(MonopolyCommand, int)>
                    {
                        (MonopolyCommand.UnmortgageProperty, unmortgageFee.PriceCashPow(availableCash)),
                        (MonopolyCommand.CancelAction, 15)
                    };
                    var choice = weights.ChaosChoice(aiInfo.ChaosFactor);
                    if (choice == MonopolyCommand.CancelAction)
                        continue;

                    _context.UnMortgage(player, propId);
                    availableCash -= unmortgageFee;
                }
            }

            // buyHouses
            var boughtHouses = false;
            do
            {
                boughtHouses = false;
                foreach (var propId in propIds)
                {
                    if (!_context.CanBuildHouse(player, propId))
                        continue;

                    var dev = _context.GetTileComponent<PropertyDevelopment>(propId);
                    if (dev.HouseBuyPrice > availableCash)
                        continue;

                    var weights = new List<(MonopolyCommand, int)>
                    {
                        (MonopolyCommand.BuyHouse, dev.HouseBuyPrice.PriceCashPow(availableCash)),
                        (MonopolyCommand.CancelAction, 15)
                    };
                    if (weights.ChaosChoice(aiInfo.ChaosFactor) == MonopolyCommand.CancelAction)
                        continue;

                    _context.BuyHouse(player, propId);
                    availableCash -= dev.HouseBuyPrice;
                    boughtHouses = true;
                }
            } while (boughtHouses);

            aiInfo.DidPropertyActionsThisTurn = true;
        }

        bool ProposedTrade(Player player, AiInfo aiInfo)
        {
            var setsMissingAProperty = _context.SetsMissingProperties(player);
            if (setsMissingAProperty.Count == 0)
                return false;

            var possibleTraders = _context.GetAllPlayers()
                .Where(p => p.Id != player.Id)
                .Where(p => !aiInfo.TradeCooldowns.ContainsKey(p.Id));

            foreach (var trader in possibleTraders)
            {
                var interestingTradables = _context.TradableProperties(trader.Id)
                    .Where(propId => setsMissingAProperty.Contains(_context.GetTileComponent<Property>(propId).SetId))
                    .ToList();
                if (interestingTradables.Count == 0)
                    continue;

                var receiverAssetsSum = interestingTradables.Sum(x => _context.GetTileComponent<Property>(x).BasePrice);
                if (0.75f * receiverAssetsSum > player.Cash)
                    continue;

                var receiverAssets = new PlayerAssets(trader.Id, interestingTradables, 0, 0);
                var weights = new List<(int, int)>(8);
                weights.Add((-1, 70));

                for (float coeff = 0.75f; coeff < 1.1f; coeff += 0.05f)
                {
                    var moneyGiven = receiverAssetsSum * coeff;
                    if (moneyGiven > player.Cash)
                        break;

                    var weight = (int)((coeff - 1) * 120);
                    weight += (int)(player.Cash / moneyGiven * 25);

                    weights.Add(((int)(moneyGiven), weight));
                }
                var choice = weights.ChaosChoice(aiInfo.ChaosFactor);
                if (choice == -1)
                    continue;

                var initiatorAssets = new PlayerAssets(player.Id, null, choice, 0);
                var offer = new TradeOffer(initiatorAssets, receiverAssets);

                _context.Add(offer);
                aiInfo.TradeCooldowns.Add(trader.Id, 4);

                return true;
            }

            return false;
        }

        public AITurnScenario(Context context) =>
            _context = context;
    }
}
