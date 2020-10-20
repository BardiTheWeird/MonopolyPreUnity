using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class InJailManager
    {
        #region Dependencies
        private readonly RequestManager _requestManager;
        private readonly PlayerManager _playerManager;
        private readonly Dice _dice;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <returns>true, if player got out of jail</returns>
        public (bool, MonopolyCommand) InJailMove(Player player)
        {
            // possibly 1
            if(player.TurnsInPrison == 0)
            {
                Request<MonopolyCommand> request;
                if (player.JailCards > 0)
                {
                    request = new Request<MonopolyCommand>(MonopolyRequest.MoveJailChoice,
                        new List<MonopolyCommand>()
                        {
                        MonopolyCommand.JailPayMoney,
                        MonopolyCommand.JailUseCard,
                        MonopolyCommand.JailUseDice
                        });
                } else
                {
                    request = new Request<MonopolyCommand>(MonopolyRequest.MoveJailChoice,
                        new List<MonopolyCommand>()
                        {
                        MonopolyCommand.JailPayMoney,
                        MonopolyCommand.JailUseDice
                        });
                }
                
                var command =  _requestManager.SendRequest(player.Id, request);
                
                // TODO: fix hardcode
                if(command == MonopolyCommand.JailPayMoney)
                {
                    _playerManager.PlayerCashCharge(player.Id, 50);
                    player.TurnsInPrison = null;

                    return (true, MonopolyCommand.JailPayMoney);
                }
                else if(command == MonopolyCommand.JailUseCard)
                {
                    player.JailCards -= 1;
                    player.TurnsInPrison = null;

                    return (true, MonopolyCommand.JailUseCard);
                }
            }
            else if(player.TurnsInPrison == 1 || player.TurnsInPrison == 2)
            {
                _dice.Throw();

                if(_dice.Die1 == _dice.Die2)
                {
                    player.TurnsInPrison = null;
                    return (true, MonopolyCommand.JailUseDice);
                }
            }

            else if(player.TurnsInPrison >= 3)
            {
                // TODO: fix hardcode
                _playerManager.PlayerCashCharge(player.Id, 50);
                player.TurnsInPrison = null;
                return (true, MonopolyCommand.JailPayMoney);
            }

            player.TurnsInPrison++;
            return (false, MonopolyCommand.StayInJail);
        }

        #region Constructor
        public InJailManager(RequestManager requestManager, PlayerManager playerManager, GameData gameData)
        {
            _requestManager = requestManager;
            _playerManager = playerManager;
            _dice = gameData.DiceValues;
        }
        #endregion
    }
}
