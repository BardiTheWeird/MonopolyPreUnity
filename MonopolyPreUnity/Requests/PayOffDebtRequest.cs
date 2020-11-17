using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Requests
{
    class PayOffDebtRequest : IRequest
    {
        public int DebtAmount { get; set; }

        public PayOffDebtRequest(int debtAmount)
        {
            DebtAmount = debtAmount;
        }
    }
}
