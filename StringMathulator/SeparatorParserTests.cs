using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace StringMathulator
{
    public class SeparatorParserTests
    {
        [Test]
        public void CanGetSingleSeparator()
        {
            SeparatorParser.Parse(";").Should().BeEquivalentTo(new []{";"});
        }

        [Test]
        public void CanGetMultiCharacterSeparator()
        {
            SeparatorParser.Parse("[***]").Should().BeEquivalentTo(new[] { "***" });
        }

        [Test]
        public void CanGetMultipleSeparators()
        {
            SeparatorParser.Parse("[;][,]").Should().BeEquivalentTo(new[] { ";", "," });
        }

        [Test]
        public void CanGetMoreThanOneMultiCharacterSeparators()
        {
            SeparatorParser.Parse("[;;][***]").Should().BeEquivalentTo(new[] { ";;", "***" });
        }
    }
}
