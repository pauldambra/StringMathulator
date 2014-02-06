using System.Collections.Generic;

namespace StringMathulator
{
    internal struct CalcConfig
    {
        internal string[] Separator { get; set; }
        internal IEnumerable<string> CalcInput { get; set; }
    }
}