using Microsoft.VisualBasic.CompilerServices;
using MonopolyPreUnity.Components.SystemRequest.Output;
using MonopolyPreUnity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MonopolyPreUnity.UI
{
    class InputParser
    {
        #region Dependencies
        private readonly Context _context;
        #endregion

        #region generic input
        public bool TryParse<T>(out T outVal, string input = null, bool printError = true) where T : IConvertible
        {
            outVal = default;
            try
            {
                input = input ?? _context.InputString.Trim();
                outVal = (T)Convert.ChangeType(input, typeof(T));
                return true;
            }
            catch
            {
                if (printError)
                    _context.Add(new PrintLine("Invalid input. Try again", OutputStream.HSInputLog));
                return false;
            }
        }

        public bool TryParse<T>(Func<T, bool> pred, out T val, string errorMessage, string input = null, bool printError = true) where T : IConvertible
        {
            if (!TryParse(out val, input, printError))
                return false;

            if (!pred(val))
            {
                if (printError)
                    _context.Add(new PrintLine(errorMessage + ". Try again", OutputStream.HSInputLog));
                return false;
            }
            return true;
        }

        public bool TryParseIndex<T>(IEnumerable<T> values, out int val, bool canCancel = false, string input = null, bool printError = true)
        {
            if (TryParse(x => (1 <= x && x <= values.Count()) || (canCancel && x == -1), 
                out val, "Index out of range", input, printError))
            {
                val = val == -1 ? -1 : val - 1;
                return true;
            }
            return false;
        }

        public bool TryParseIndexMultiple<T>(IEnumerable<T> values, out HashSet<int> outValues, string input = null)
        {
            input = input ?? _context.InputString;
            var nums = Regex.Matches(input, @"-?[0-9]+")
                .Cast<Match>()
                .Select(match => match.Value);

            outValues = new HashSet<int>();
            var failedNums = new List<string>();
            var success = true;
            foreach (var numString in nums)
            {
                if (TryParseIndex(values, out var value, false, numString, printError: false))
                    outValues.Add(value);
                else
                {
                    failedNums.Add(numString);
                    success = false;
                }
            }

            if (!success)
                _context.Add(new PrintLine("Problematic values: " + string.Join(" ", failedNums), OutputStream.HSInputLog));

            return success;
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
