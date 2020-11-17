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

        TradeChoosePlayer,
        TradeChooseAction,
        TradeChangeAssets,
        TradeChooseCash,
        TradeChooseJailCards,
        TradeChooseProperties,

        TradeValidation,

        Debt,
        DebtChooseProperty,
        DebtChoosePropertyAction,
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
        public object MiscInfo { get; set; }
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
            MiscInfo = null;
        }

        public void Set(HSState state, int playerId, object miscInfo = null)
        {
            CurState = state;
            PlayerId = playerId;
            MiscInfo = miscInfo;
        }
        #endregion

        #region ctor
        public HSInputState(HSState? curState, int curPlayerId, object miscInfo = null)
        {
            CurState = curState;
            PlayerId = curPlayerId;
            MiscInfo = miscInfo;
        }

        public HSInputState()
        {
        }
        #endregion
    }
}
