using MonopolyPreUnity.Components.SystemRequest;
using MonopolyPreUnity.Components.SystemRequest.Cash;
using MonopolyPreUnity.Components.SystemRequest.PlayerState;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Systems.PlayerState
{
    class PlayerDebtSystem : ISystem
    {
        private readonly Context _context;

        public void Execute()
        {
            var debt = _context.GetComponent<PlayerDebt>();
            if (debt == null)
                return;

            var paidOff = _context.GetComponent<PaidOffDebt>();
            if (paidOff == null)
            {
                if (!_context.ContainsComponent<PlayerInputRequest>() && _context.HSInputState().IsNull)
                    _context.Add(new PlayerInputRequest(debt.DebtorId, new PayOffDebtRequest(debt.DebtAmount)));
                return;
            }

            _context.Add(new ChargeCash(debt.DebtAmount, debt.DebtorId, debt.CreditorId, "paying off debt"));
            _context.Remove(debt);
            _context.Remove(paidOff);
        }

        public PlayerDebtSystem(Context context)
        {
            _context = context;
        }
    }
}
