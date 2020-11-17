using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.Requests;
using MonopolyPreUnity.UI;
using MonopolyPreUnity.Utitlity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.RequestHandlers.HSScenario
{
    class HSDebtScenario : IHSRequestScenario
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        public void RunScenario(IRequest requestIn, Player player)
        {
            _context.HSInputState().Set(HSState.Debt, player.Id);
        }

        #region ctor
        public HSDebtScenario(Context context) =>
            _context = context;
        #endregion
    }
}
