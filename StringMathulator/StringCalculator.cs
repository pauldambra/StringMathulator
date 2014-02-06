using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StringMathulator
{
    using System.Reflection;

    public class StringCalculator
    {
        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        //properties exposed as fields to allow passing as ref
        struct StringsToAdd
        {
            internal string Longest;
            internal string Shortest;
        }

        public static string Add(string left, string right)
        {
            left = Reverse(left);
            right = Reverse(right);

            var stringsToAdd = GetStringsToAdd(left, right);

            var accumulator = "";
            var carryForward = 0;

            while (stringsToAdd.Longest.Length > 0)
            {
                var first = GetAndRemoveFirstCharacter(ref stringsToAdd.Longest);
                var second = GetAndRemoveFirstCharacter(ref stringsToAdd.Shortest);
                var thisTotal = int.Parse(first) + int.Parse(second) + carryForward;
                if (thisTotal < 10)
                {
                    accumulator += thisTotal.ToString(CultureInfo.InvariantCulture);
                    carryForward = 0;
                    continue;
                }
                accumulator += thisTotal % 10;
                carryForward = thisTotal /= 10;
            }
            return Reverse(accumulator);
        }

        private static StringsToAdd GetStringsToAdd(string left, string right)
        {
            return left.Length >= right.Length
                ? new StringsToAdd { Longest = left, Shortest = right }
                : new StringsToAdd { Longest = right, Shortest = left };
        }

        private static string GetAndRemoveFirstCharacter(ref string s)
        {
            if (s.Length == 0)
            {
                return "0";
            }

            var first = s.Substring(0, 1);
            s = s.Remove(0, 1);
            return first;
        }
    }
}