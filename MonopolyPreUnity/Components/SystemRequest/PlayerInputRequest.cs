using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components.SystemRequest
{
    class PlayerInputRequest : IEntityComponent
    {
        public int PlayerId { get; set; }
        public IRequest Request { get; set; }

        public PlayerInputRequest(int playerId, IRequest request)
        {
            PlayerId = playerId;
            Request = request;
        }
    }
}
