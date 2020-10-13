using MonopolyPreUnity.Managers;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Classes
{
    class Request<TInput>
    {
        #region Properties
        public MonopolyRequest RequestType { get; }
        public List<TInput> Choices { get; }
        public string Description { get; }
        #endregion

        #region Constructors
        public Request(MonopolyRequest requestType, List<TInput> choices)
        {
            RequestType = requestType;
            Choices = choices;
        }

        public Request(MonopolyRequest requestType)
        {
            RequestType = requestType;
            Choices = null;
        }
        #endregion
    }
}
