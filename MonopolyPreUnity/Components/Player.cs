using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace MonopolyPreUnity.Components
{
    class Player : IIdentifiable, INotifyPropertyChanged
    {
        #region fields
        int _cash;
        int _jailCards;
        int? _turnsInJail;
        #endregion

        #region properties
        public int Id { get; }
        public string DisplayName { get; set; }
        public int Cash 
        {
            get => _cash;
            set
            {
                if (value == _cash)
                    return;
                _cash = value;
                RaisePropertyChanged(nameof(Cash));
            } 
        }
        public int JailCards 
        {
            get => _jailCards;
            set
            {
                if (value == _jailCards)
                    return;
                _jailCards = value;
                RaisePropertyChanged(nameof(JailCards));
            } 
        }
        public int? TurnsInJail 
        {
            get => _turnsInJail;
            set
            {
                if (value == _turnsInJail)
                    return;
                _turnsInJail = value;
                RaisePropertyChanged(nameof(TurnsInJail));
            }
        }
        public int CurTileId { get; set; }
        public bool CanMove { get; set; }
        public bool RolledJailDiceThisTurn { get; set; }
        public HashSet<int> Properties { get; }
        #endregion

        #region propChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion

        #region ctor
        public Player(int id, string displayName, int cash,
            HashSet<int> properties, int jailCards, int? turnsInPrison, int curTileId, bool rolledJailDiceThisTurn)
        {
            Id = id;
            DisplayName = displayName;
            Cash = cash;
            Properties = properties;
            JailCards = jailCards;
            TurnsInJail = turnsInPrison;
            CanMove = true;
            CurTileId = curTileId;
            RolledJailDiceThisTurn = rolledJailDiceThisTurn;
        }

        public Player(int id, string displayName, HashSet<int> props = null)
        {
            Id = id;
            DisplayName = displayName;
            Properties = props ?? new HashSet<int>();
        }
        #endregion
    }
}
