using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Interfaces;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class ActionManager
    {
        #region Dependencies
        private readonly PlayerManager _playerManager;
        private readonly TileManager _tileManager;
        private readonly MapManager _mapManager;
        #endregion

        // hopefully someday with behaviors instead of this unholy shite
        // Dictionary<MonopolyActionType, Action<int, IMonopolyAction>> _actionDict;

        #region Actions
        void ChangeBalanceAction(int playerId, IMonopolyAction action)
        {
            var balanceAction = action as MonopolyAction<int>;
            _playerManager.ChangeBalance(playerId, balanceAction.Argument1);
        }

        void GiftFromPlayersAction(int playerId, IMonopolyAction action)
        {
            var giftAction = action as MonopolyAction<int>;
            int sum = 0;
            foreach (int player in _playerManager.GetAllPlayerId().Where(x => x != playerId))
            {
                _playerManager.PlayerCashCharge(player, giftAction.Argument1);
                sum += giftAction.Argument1;
            }
            _playerManager.PlayerCashGive(playerId, sum);
        }

        void TaxPerHouseAction(int playerId, IMonopolyAction action)
        {
            var taxAction = action as MonopolyAction<int>;
            var playerProperty = _playerManager.GetPlayer(playerId).Properties;

            int sum = 0;
            foreach(int propertyId in playerProperty)
            {
                if (_tileManager.GetTileComponent<PropertyDevelopmentComponent>(propertyId, out var component))
                    sum += component.HousesBuilt * taxAction.Argument1;
            }

            _playerManager.PlayerCashCharge(playerId, sum);
        }

        void GoToJailAction(int playerId, IMonopolyAction action)
        {
            var jailAction = action as MonopolyAction;
            var player = _playerManager.GetPlayer(playerId);

            _mapManager.MoveToTile(playerId, _tileManager.GetTileWithComponent<JailComponent>(), false);
            player.TurnsInPrison = 0;
        }

        void GoToTileAction(int playerId, IMonopolyAction action)
        {
            var tileAction = action as MonopolyAction<Func<Tile, bool>>;
            _mapManager.MoveByFunc(playerId, tileAction.Argument1);
        }

        void GetAnItemAction(int playerId, IMonopolyAction action)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ExecuteAction

        // hopefully someday
        /*public void ExecuteAction(int playerId, IMonopolyAction action) =>
            _actionDict[action.ActionType](playerId, action);*/

        // Will go fucking away. There will be something akin to what PlayerLanded Behaviors have
        public void ExecuteAction(int playerId, IMonopolyAction action)
        {
            Logger.Log($"actionType = {action.ActionType:g}");
            switch (action.ActionType)
            {
                case MonopolyActionType.ChangeBalance:
                    ChangeBalanceAction(playerId, action);
                    break;

                case MonopolyActionType.GiftFromPlayers:
                    GiftFromPlayersAction(playerId, action);
                    break;

                case MonopolyActionType.TaxPerHouse:
                    TaxPerHouseAction(playerId, action);
                    break;

                case MonopolyActionType.GoToJail:
                    GoToJailAction(playerId, action);
                    break;

                case MonopolyActionType.GoToTile:
                    GoToTileAction(playerId, action);
                    break;

                case MonopolyActionType.GetAnItem:
                    GetAnItemAction(playerId, action);
                    break;
            }
        }
        #endregion

        #region Constructor
        public ActionManager(PlayerManager playerManager, TileManager tileManager, MapManager mapManager)
        {
            _playerManager = playerManager;
            _tileManager = tileManager;
            _mapManager = mapManager;
        }
        #endregion
    }
}
