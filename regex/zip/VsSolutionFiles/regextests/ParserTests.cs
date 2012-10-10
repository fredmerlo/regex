using System;
using NUnit.Framework;
using regex;

namespace regextests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void Parser_Match_Returns_Exception_On_Null_Input()
        {
            Assert.Throws<ArgumentNullException>(() => Parser.Instance(".").Matches(null));
        }

        [Test]
        public void Parser_Match_Retruns_True_On_Valid_Input()
        {
            Assert.True(Parser.Instance("abc").Matches("abc"));
            Assert.True(Parser.Instance("*").Matches("abc"));
            Assert.True(Parser.Instance("*abc").Matches("abc"));
            Assert.True(Parser.Instance("*abc").Matches("aaabbbbabc"));
            Assert.True(Parser.Instance("a*bc").Matches("aaabbbbabc"));
            Assert.True(Parser.Instance("a*bc").Matches("abc"));
            Assert.True(Parser.Instance("a*").Matches("abc"));
            Assert.True(Parser.Instance("a*").Matches("a"));
            Assert.True(Parser.Instance("a*").Matches("aa"));
            Assert.True(Parser.Instance("a*").Matches("abcdef"));
            Assert.True(Parser.Instance("*abc*").Matches("abc"));
            Assert.True(Parser.Instance("*****").Matches("abc"));
            Assert.True(Parser.Instance("...").Matches("abc"));
            Assert.True(Parser.Instance(".*").Matches("abc"));
            Assert.True(Parser.Instance(".bc*").Matches("abc"));
            Assert.True(Parser.Instance(".b*c*a").Matches("abca"));
            Assert.True(Parser.Instance("*").Matches(string.Empty));
        }

        [Test]
        public void Parser_Match_Returns_False_On_Invalid_Input()
        {
            Assert.False(Parser.Instance("abc").Matches("abcd"));
            Assert.False(Parser.Instance("*a").Matches("abcd"));
            Assert.False(Parser.Instance("a").Matches(string.Empty));
            Assert.False(Parser.Instance(".a*c").Matches("abc"));
            Assert.False(Parser.Instance("a.*b").Matches("abc"));
            Assert.False(Parser.Instance("..").Matches("abc"));
            Assert.False(Parser.Instance(string.Empty).Matches(string.Empty));
            Assert.False(Parser.Instance(string.Empty).Matches("abc"));
        }

        [Test]
        public void Parser_Match_Returns_True_On_Additional_Valid_Input()
        {
            Assert.True(Parser.Instance("a.*c").Matches("abc"));   
        }

        [Test]
        public void Parser_Instance_Returns_False_On_Additional_Invalid_Input()
        {
            Assert.False(Parser.Instance("*.").Matches("abc"));   
            Assert.False(Parser.Instance(".").Matches(string.Empty));   
        }

    }
}
