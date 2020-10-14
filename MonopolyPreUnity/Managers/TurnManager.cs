using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class TurnManager
    {
        #region dependencies
        #endregion
        public List<int> TurnOrder { get; private set; }
        public int CurrentPlayer { get; private set; }
        TurnManager(List<int> turnOrderList)
        {
            TurnOrder = turnOrderList; 
        }
    }
}
