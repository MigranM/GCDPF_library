using NUnit.Framework;
using System;
using GCDPF;

namespace GCDPFTest
{
    public class Integer32ExtTests
    {
        [TestCase(5, 11, ExpectedResult = 9)]
        [TestCase(8, 13, ExpectedResult = 5)]
        [TestCase(15, 23, ExpectedResult = 20)]
        [TestCase(456, 937, ExpectedResult = 787)]
        [TestCase(1357, 2179, ExpectedResult = 782)]
        [TestCase(5645, 9887, ExpectedResult = 599)]
        public int ReverseMulTest(int number, int field) => number.ReverseMul(field);

        [Test]
        public void ReverseNulArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { 0.ReverseMul(0); });
            Assert.Throws<ArgumentException>(() => { 0.ReverseMul(3); });
            Assert.Throws<ArgumentException>(() => { 4.ReverseMul(20); });
            Assert.Throws<ArgumentException>(() => { 5.ReverseMul(20); });
            Assert.Throws<ArgumentException>(() => { 25.ReverseMul(100); });
        }

        [TestCase(1, ExpectedResult = true)]
        [TestCase(2, ExpectedResult = true)]
        [TestCase(3, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(11, ExpectedResult = true)]
        [TestCase(33, ExpectedResult = false)]
        [TestCase(6719, ExpectedResult = true)]
        [TestCase(8929, ExpectedResult = true)]
        [TestCase(9973, ExpectedResult = true)]
        [TestCase(779869, ExpectedResult = true)]
        [TestCase(779870, ExpectedResult = false)]
        [TestCase(2001449, ExpectedResult = true)]
        [TestCase(2333497, ExpectedResult = true)]
        [TestCase(3842903, ExpectedResult = true)]
        public bool PrimeTest(int number) => number.IsPrime();
    }
}
