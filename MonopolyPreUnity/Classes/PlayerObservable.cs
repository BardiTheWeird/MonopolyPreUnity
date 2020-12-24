using MonopolyPreUnity.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class PlayerObservable : INotifyPropertyChanged
    {
        #region properties
        public Player Player { get; set; }
        public bool _isCurTurn;
        public bool IsCurTurn 
        { 
            get => _isCurTurn;
            set 
            {
                if (value == _isCurTurn)
                    return;
                _isCurTurn = value;
                RaisePropertyChanged(nameof(IsCurTurn));
            } 
        }
        #endregion

        #region propChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion

        #region ctor
        public PlayerObservable(Player player, bool isCurTurn = false)
        {
            Player = player;
            IsCurTurn = isCurTurn;
        }
        #endregion
    }
}
