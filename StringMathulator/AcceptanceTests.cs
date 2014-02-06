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
        [Test]
        public void CanAdd()
        {
            StringCalculator.Add("123", "3459").Should().Be("3582");
        }

        [Test]
        public void CanAddUnits()
        {
            StringCalculator.Add("1", "3").Should().Be("4");
        }
    }
}
