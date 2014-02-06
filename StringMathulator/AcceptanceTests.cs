using System;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace StringMathulator
{
    [TestFixture]
    public class AcceptanceTests
    {
        readonly StringCalculator _calculator = new StringCalculator();

        [Test]
        public void AnEmptyStringReturnsZero()
        {
            _calculator.Add("").Should().Be(0);
        }

        [Test]
        [TestCase("1", 1)]
        [TestCase("2", 2)]
        public void ASingleNumberIsReturned(string input, int asExpected)
        {
            _calculator.Add(input).Should().Be(asExpected);
        }

        [Test]
        [TestCase("1,1", 2)]
        [TestCase("9,2", 11)]
        public void TwoNumbersAreAddedTogether(string input, int asExpected)
        {
            _calculator.Add(input).Should().Be(asExpected);
        }

        [Test]
        [TestCase("1,1,5,2,6,7", 22)]
        [TestCase("9,2,3,4,5", 23)]
        public void MultipleNumbersCanBeAdded(string input, int asExpected)
        {
            _calculator.Add(input).Should().Be(asExpected);
        }

        [Test]
        public void CanUseNewLinesAsSeparators()
        {
            _calculator.Add("1\n2,3").Should().Be(6);
        }

        [Test]
        public void CanSpecifySeparators()
        {
            _calculator.Add("//;\n1;2").Should().Be(3);
        }

        [Test]
        public void NegativesThrowAnException()
        {
            Action action = () => _calculator.Add("-1");
            action.ShouldThrow<Exception>().WithMessage("negatives not allowed -1");
        }

        [Test]
        public void MultipleNegativesThrowAnException()
        {
            Action action = () => _calculator.Add("-1,1,-2");
            action.ShouldThrow<Exception>().WithMessage("negatives not allowed -1,-2");
        }

        [Test]
        public void NegativesWithCustomSeparatorsThrowAnException()
        {
            Action action = () => _calculator.Add("//;\n-1;1;-2");
            action.ShouldThrow<Exception>().WithMessage("negatives not allowed -1,-2");
        }

        [Test]
        public void NumbersOverOneThousandAreIgnored()
        {
            _calculator.Add("2,1000").Should().Be(1002);
            _calculator.Add("2,1001").Should().Be(2);
        }

        [Test]
        public void CanUseMultiCharacterSeparators()
        {
            _calculator.Add("//[***]\n1***2***3").Should().Be(6);
        }

        [Test]
        public void CanHaveMultipleSeparators()
        {
            _calculator.Add("//[*][%]\n1*2%3").Should().Be(6);
        }

        [Test]
        public void CanHaveMultipleMultiCharacterSeparators()
        {
            _calculator.Add("//[***][%%]\n1***2%%3").Should().Be(6);
        }
    }
}
