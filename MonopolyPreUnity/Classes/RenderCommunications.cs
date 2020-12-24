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
        bool _curTileViewLock;
        string _curDescription;
        int _tileToLockOnId;
        bool _auctionInfoChanged;
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
        public bool CurTileViewLock
        {
            get => _curTileViewLock;
            set
            {
                if (value == _curTileViewLock)
                    return;
                _curTileViewLock = value;
                RaisePropertyChanged(nameof(CurTileViewLock));
            }
        }
        public string CurDescription
        {
            get => _curDescription;
            set
            {
                if (value == _curDescription)
                    return;
                _curDescription = value;
                RaisePropertyChanged(nameof(CurDescription));
            }
        }
        public int TileToLockOnId
        {
            get => _tileToLockOnId;
            set
            {
                if (value == _tileToLockOnId)
                    return;
                _tileToLockOnId = value;
                RaisePropertyChanged(nameof(TileToLockOnId));
            }
        }
        public bool AuctionInfoChanged
        {
            get => _auctionInfoChanged;
            set
            {
                if (value == _auctionInfoChanged)
                    return;
                _auctionInfoChanged = value;
                RaisePropertyChanged(nameof(AuctionInfoChanged));
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
