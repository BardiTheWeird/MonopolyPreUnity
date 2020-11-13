using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest.HSInput
{
    enum HSState
    {
        TurnChoice,
        BuyAuctionChoice,

        PropManageChooseProperty,
        PropManageChooseAction,

        AuctionBidChoice,
    }

    class HSInputState : IEntityComponent, INotifyPropertyChanged
    {
        #region properties
        HSState? _curState;
        public HSState? CurState 
        { 
            get => _curState; 
            set
            {
                if (value == _curState)
                    return;
                _curState = value;
                RaisePropertyChanged(nameof(CurState));
                Debug.WriteLine($"New HSInputState: {CurState}");
            }
        }
        public int? PlayerId { get; set; }
        public bool IsNull => CurState == null;
        #endregion

        #region property changed
        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion

        #region methods
        public void Nullify()
        {
            CurState = null;
            PlayerId = null;
        }

        public void Set(HSState state, int playerId)
        {
            CurState = state;
            PlayerId = playerId;
        }
        #endregion

        #region ctor
        public HSInputState(HSState? curState, int curPlayerId)
        {
            CurState = curState;
            PlayerId = curPlayerId;
        }

        public HSInputState()
        {
        }
        #endregion
    }
}
