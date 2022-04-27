using NUnit.Framework;
using GCDPF;


namespace GCDPFTest
{
    public class PolynomialExtremeTest
    {
        [Test]
        public void ExtremeCtor()
        {
            Polynomial polimon1 = new Polynomial();
            Polynomial polimon2 = new Polynomial(new int[] { 0 });

            Assert.AreEqual(polimon2, polimon1);
        }
        [Test]
        public void ExtremeMethods()
        {
            Polynomial polimon1 = new Polynomial();
            Polynomial polimon2 = new Polynomial(new int[] { 0, 0, 0, 0 });
            polimon1.Equals(polimon2);
            Polynomial polynomial3 = polimon1 + polimon2;
            Polynomial polynomial4 = polimon1 - polimon2;
            Polynomial polynomial5 = polimon1 * polimon2;
        }
        [Test]
        public void NullPolinom()
        {
            Polynomial polimon1 = new Polynomial();
            Polynomial polimon2 = new Polynomial(new int[] { 0, 0, 0, 0 });
            Assert.IsTrue(polimon1.IsNull);
            Assert.IsTrue(polimon2.IsNull);
        }
    }
    public class PolynomialToStringTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ExtremeToString1()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 0, 0, 0, 0, 0, 0 });
            string Actual = polinom1.ToString();
            string Expected = "0";
            Assert.AreEqual(Expected, Actual);
        }
        [Test]
        public void ExtremeToString2()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 0, 0, 15, 0, 0, 0 });
            string Actual = polinom1.ToString();
            string Expected = "15x^3";
            Assert.AreEqual(Expected, Actual);
        }
        [Test]
        public void ExtremeToString3()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 0, 2, 0, 1, 0, 0 });
            string Actual = polinom1.ToString();
            string Expected = "2x^4 + x^2";
            Assert.AreEqual(Expected, Actual);
        }
        [Test]
        public void RegularToString1()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 1, 0, 0, -5, 1, 0, 36, 0, 0, 11, -1 });
            string Actual = polinom1.ToString();
            string Expected = "x^10 - 5x^7 + x^6 + 36x^4 + 11x - 1";
            Assert.AreEqual(Expected, Actual);
        }
        [Test]
        public void RegularToString2()
        {
            Polynomial polinom1 = new Polynomial(new int[] { -1, 0, 5 });
            string Actual = polinom1.ToString();
            string Expected = "- x^2 + 5";
            Assert.AreEqual(Expected, Actual);
        }
        [Test]
        public void RegularToString3()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            string Actual = polinom1.ToString();
            string Expected = "x^9 + 2x^8 + 3x^7 + 4x^6 + 5x^5 + 6x^4 + 7x^3 + 8x^2 + 9x + 10";
            Assert.AreEqual(Expected, Actual);
        }
    }
    public class PolynomialAdditionalOperationsTest
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void HashCode()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 1, -4, 2, 6, -2, 5, 1, 2, 2 });
            Polynomial polinom2 = new Polynomial(new int[] { 1, -4, 2, 6, -2, 5, 1, 2, 2 });
            Assert.IsTrue(polinom1.GetHashCode() == polinom2.GetHashCode());
        }
        [Test]
        public void HashCodeCollision()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 1, -4, 2, 6, -2, 5, 1, 2, 2 });
            Polynomial polinom2 = new Polynomial(new int[] { 1, -4, 2, 6, -2, 5, 2, 1, 2 });
            Assert.AreNotEqual(polinom1.GetHashCode(), polinom2.GetHashCode());
        }
        [Test]
        public void Equals()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 2, 65, -2, 1234, 12, -312, 3, 124, 251, -251 });
            Polynomial polinom2 = new Polynomial(new int[] { 2, 65, -2, 1234, 12, -312, 3, 124, 251, -251 });
            Assert.IsTrue(polinom1.Equals(polinom2));
        }
        [Test]
        public void ToField()
        {
            Polynomial Actual = new Polynomial(new int[] { 53, 76, -21, 72, 22, -72, -20, 2, 12, 5, -1, -0, });
            Polynomial Expected = new Polynomial(new int[] { 1, 11, 5, 7, 9, 6, 6, 2, 12, 5, 12, 0 });
            int field = 13;
            Actual = Actual.ToField(field);
            Assert.AreEqual(Expected, Actual);
        }

    }
    public class PolynomialOperationsTest
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ExtremeSum()
        {
            Polynomial polinom1 = new Polynomial(new int[1]);
            Polynomial polinom2 = new Polynomial(new int[1]);
            Polynomial Actual = polinom1 + polinom2;
            Polynomial Expected = new Polynomial(new int[] { 0 });
            Assert.AreEqual(Expected, Actual);
        }
        [Test]
        public void ExtremeSub()
        {
            Polynomial polinom1 = new Polynomial(new int[1]);
            Polynomial polinom2 = new Polynomial(new int[1]);
            Polynomial Actual = polinom1 - polinom2;
            Polynomial Expected = new Polynomial(new int[] { 0 });
            Assert.AreEqual(Expected, Actual);
        }
        [Test]
        public void ExtremeMul()
        {
            Polynomial polinom1 = new Polynomial(new int[1]);
            Polynomial polinom2 = new Polynomial(new int[1]);
            Polynomial Actual = polinom1 * polinom2;
            Polynomial Expected = new Polynomial(new int[] { 0 });
            Assert.AreEqual(Expected, Actual);
        }

        [Test]
        public void RegularSum()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 2, 1, 3 });
            Polynomial polinom2 = new Polynomial(new int[] { 3, 6 });
            Polynomial Actual = polinom1 + polinom2;
            Polynomial Expected = new Polynomial(new int[] { 2, 4, 9 });
            Assert.AreEqual(Expected, Actual);
        }
        [Test]
        public void RegularSub()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 2, 1, 3 });
            Polynomial polinom2 = new Polynomial(new int[] { 3, 6 });
            Polynomial Actual = polinom1 - polinom2;
            Polynomial Expected = new Polynomial(new int[] { 2, -2, -3 });
            Assert.AreEqual(Expected, Actual);
        }
        [Test]
        public void RegularMul()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 2, 1, 3 });
            Polynomial polinom2 = new Polynomial(new int[] { 3, 6 });
            Polynomial Actual = polinom1 * polinom2;
            Polynomial Expected = new Polynomial(new int[] { 6, 15, 15, 18 });
            Assert.AreEqual(Expected, Actual);
        }

        [Test]
        public void TrimSum()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 0, 0, 1, 0 });
            Polynomial polinom2 = new Polynomial(new int[] { 1, 0 });
            Polynomial Actual = polinom1 + polinom2;
            Polynomial Expected = new Polynomial(new int[] { 2, 0 });
            Assert.AreEqual(Expected, Actual);
        }
        [Test]
        public void TrimSub()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 0, 0, 1, 0 });
            Polynomial polinom2 = new Polynomial(new int[] { 1, 0 });
            Polynomial Actual = polinom1 - polinom2;
            Polynomial Expected = new Polynomial(new int[] { 0 });
            Assert.AreEqual(Expected, Actual);
        }
        [Test]
        public void TrimMul()
        {
            Polynomial polinom1 = new Polynomial(new int[] { 0, 0, 1, 0 });
            Polynomial polinom2 = new Polynomial(new int[] { 1, 0 });
            Polynomial Actual = polinom1 * polinom2;
            Polynomial Expected = new Polynomial(new int[] { 1, 0, 0 });
            Assert.AreEqual(Expected, Actual);
        }

    }
}