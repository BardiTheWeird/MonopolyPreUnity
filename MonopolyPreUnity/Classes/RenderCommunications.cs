using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class RenderCommunications : INotifyPropertyChanged
    {
        #region fields
        bool _playersMoved;
        bool _houseChanged;
        #endregion

        #region properties
        public bool PlayersMoved
        {
            get => _playersMoved;
            set
            {
                if (value == _playersMoved)
                    return;
                _playersMoved = value;
                RaisePropertyChanged(nameof(PlayersMoved));
            }
        }

        public bool HouseChanged
        {
            get => _houseChanged;
            set
            {
                if (value == _houseChanged)
                    return;
                _houseChanged = value;
                RaisePropertyChanged(nameof(HouseChanged));
            }
        }
        #endregion

        #region propertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        #endregion
    }
}
