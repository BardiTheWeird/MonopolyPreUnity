using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class TurnManager
    {
        #region dependencies
        public readonly PlayerManager _playerManager;
        #endregion
        public List<int> TurnOrderIdList { get; private set; }
        public int CurrentPlayerId { get; private set; }
        
        public bool RemovePlayer(int playerId) =>
            TurnOrderIdList.Remove(playerId);

        public void DetermineTurnOrder()
        {
            List<int> playerIdList = _playerManager.GetAllPlayerId();
            
            foreach(int playerId in playerIdList)
            {
                
            }
        }
        public TurnManager(List<int> turnOrderList)
        {
            TurnOrderIdList = turnOrderList; 
        }
    }
}
