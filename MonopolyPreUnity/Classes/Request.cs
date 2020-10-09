using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class Request<TChoices, TExpectedInput>
    {
        public MonopolyCommand RequestType { get; }
        public TChoices Choices { get; }
        public TExpectedInput Input { get; set; }

        public Request(int receiverId, MonopolyCommand requestType, TChoices choices)
        {
            RequestType = requestType;
            Choices = choices;
        }
    }
}
