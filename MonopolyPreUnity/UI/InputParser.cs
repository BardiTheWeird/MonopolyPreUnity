using Microsoft.VisualBasic.CompilerServices;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyPreUnity.UI
{
    class InputParser
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        #region generic input
        public bool TryParse<T>(out T outVal) where T : IConvertible
        {
            outVal = default;
            try
            {
                var input = _context.InputString.Trim();
                outVal = (T)Convert.ChangeType(input, typeof(T));
                return true;
            }
            catch
            {
                _context.Add(new PrintLine("Invalid input. Try again"));
                return false;
            }
        }

        public bool TryParse<T>(Func<T, bool> pred, out T val, string errorMessage) where T : IConvertible
        {
            if (!TryParse(out val))
                return false;

            if (!pred(val))
            {
                _context.Add(new PrintLine(errorMessage + ". Try again"));
                return false;
            }
            return true;
        }

        public bool TryParseIndex<T>(IEnumerable<T> values, out int val, bool canCancel = false)
        {
            if (TryParse(x => (1 <= x && x <= values.Count()) || (canCancel && x == -1), out val, "Index out of range"))
            {
                val = val == -1 ? -1 : val - 1;
                return true;
            }
            return false;
        }
        #endregion

        #region ctor
        public InputParser(Context context)
        {
            _context = context;
        }
        #endregion
    }
}
