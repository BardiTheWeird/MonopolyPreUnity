using MonopolyPreUnity.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Managers
{
    class RequestManager
    {
        PlayerManager _playerManager;

        public TExpectedInput SendRequest<TChoices, TExpectedInput>(int receiverId, Request<TChoices, TExpectedInput> request)
        {
            throw new NotImplementedException();
        }
    }
}
