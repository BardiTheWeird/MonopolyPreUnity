using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class TradeManager
    {
        private readonly int _initiatorPlayerId;
        private readonly int _RecepientPlayerId;
        private LinkedList <PropertyComponent> _initiatorOffer;
        private LinkedList<PropertyComponent> _receiverOffer;
        private int _initiatorCash = 0;
        private int _receiverCash = 0;



        public TradeManager(int initiatorPlayerId, int recepientPlayerId)
        {
            _initiatorPlayerId = initiatorPlayerId;
            _RecepientPlayerId = recepientPlayerId;
        }

        public void AddPropertyForInitiator(PropertyComponent propertyComponent)
        {
            _initiatorOffer.
        }

        public void AddPropertyForIninitiator


        public void ValidateTransfer()
        {
            throw new NotImplementedException();
        }

        public void CompleteTransfer()
        {
            throw new NotImplementedException();

        }
    }
}
