using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Requests
{
    class BankruptcyRequest : IRequest
    {
        public int DebtAmount { get; set; }

        public BankruptcyRequest(int debtAmount)
        {
            DebtAmount = debtAmount;
        }
    }
}
