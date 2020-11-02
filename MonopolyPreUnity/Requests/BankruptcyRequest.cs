using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Requests
{
    class BankruptcyRequest : IRequest
    {
        public int DebtToBePaid { get; set; }

        public BankruptcyRequest(int debtToBePaid)
        {
            DebtToBePaid = debtToBePaid;
        }
    }
}
