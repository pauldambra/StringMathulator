using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringMathulator
{
    static class SeparatorParser
    {
        public static string[] Parse(string s)
        {
            if (s.Length == 1)
            {
                return new[] { s };    
            }
            const string pattern = @"(?<=\[)(.*?)(?=\])";
            var matches = Regex.Matches(s, pattern);

            var separators = new string[matches.Count];
            for (var i = 0; i < matches.Count; i++)
            {
                separators[i] = matches[i].Groups[1].Value;
            }
            return separators;
        }
    }
}