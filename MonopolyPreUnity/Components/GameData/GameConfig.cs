using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MonopolyPreUnity.Components
{
    public class GameConfig : IEntityComponent
    {
        #region properties
        public int CashPerLap { get; set; }
        public int MaxTurnsInJail { get; set; }
        public int JailFine { get; set; }
        public int MaxDicePairThrows { get; set; }
        public float MortgageFee { get; set; }
        public float MortgageCommission { get; set; }
        public int DieSides { get; set; }
        #endregion

        #region ctor
        public GameConfig(int cashPerLap, int maxTurnsInJail,
            int maxDicePairThrows, float mortgageFee,
            float mortgageComission, int jailFine, int dieSides)
        {
            CashPerLap = cashPerLap;
            MaxTurnsInJail = maxTurnsInJail;
            MaxDicePairThrows = maxDicePairThrows;
            MortgageFee = mortgageFee;
            MortgageCommission = mortgageComission;
            JailFine = jailFine;
            DieSides = dieSides;
        }

        public GameConfig()
        {
        }
        #endregion
    }
}
