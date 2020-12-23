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
        #endregion

        public void Nullify()
        {
            PlayersMoved = false;
        }

        public bool IsNull =>
            PlayersMoved == false;

        #region propertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        #endregion
    }
}
