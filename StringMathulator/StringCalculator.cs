using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StringMathulator
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
            {
                return 0;
            }

            if (numbers.Length == 1)
            {
                return int.Parse(numbers);
            }

            var preparedInput = PrepareInput(numbers);

            var inputAsNumbers = ConvertToNumbers(preparedInput).ToArray();

            CheckForNegatives(inputAsNumbers);

            return inputAsNumbers.Where(n => n <= 1000).Sum();
        }

        private static IEnumerable<int> ConvertToNumbers(CalcConfig calcConfig)
        {
            var allNumbers = new List<int>();
            foreach (var numberLine in calcConfig.CalcInput)
            {
                allNumbers.AddRange(numberLine.Split(calcConfig.Separator, StringSplitOptions.None).Select(n => Convert.ToInt32(n)));
            }
            return allNumbers;
        }

        private static void CheckForNegatives(int[] number)
        {
            if (!number.Any(n => n < 0)) return;

            var negatives = number.Where(s => s < 0).Select(n=>n.ToString(CultureInfo.InvariantCulture)).Aggregate((l, r) => l + "," + r);
            throw new Exception("negatives not allowed " + negatives);
        }

        private static CalcConfig PrepareInput(string numbers)
        {
            var numbersLines = numbers.Split('\n');
            if (numbersLines.Length == 0)
            {
                return new CalcConfig();
            }

            var separatorLine = numbersLines[0];
            if (separatorLine.StartsWith("//") && separatorLine.Length > 2)
            {
                return new CalcConfig
                    {
                        Separator = SeparatorParser.Parse(separatorLine.Substring(2)),
                        CalcInput = numbersLines.Skip(1)
                    };
            }
            
            return new CalcConfig
                {
                    Separator = new[] {","},
                    CalcInput = numbersLines
                };
        }
    }
}