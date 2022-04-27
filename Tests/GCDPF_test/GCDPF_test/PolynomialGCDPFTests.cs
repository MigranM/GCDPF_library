using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using GCDPF;

namespace GCDPFTest
{

    public class PolynomialGCDFPTest
    {
        [TestFixture]
        public class GCDFTestCase
        {
            public Polynomial a { get; private set; }
            public Polynomial b { get; private set; }

            public Polynomial GCD { get; private set; }

            public int field { get; private set; }

            public GCDFTestCase(Polynomial a, Polynomial b, int field, Polynomial result)
            {
                this.a = a;
                this.b = b;
                this.field = field;
                this.GCD = result;
            }

            private static IEnumerable<GCDFTestCase> GCDFDataToTest
            {
                get
                {
                    yield return new GCDFTestCase
                        (
                            new Polynomial(new int[] { 3, 0, 2 }),
                            new Polynomial(new int[] { 2, 1 }),
                            3,
                            new Polynomial(new int[] { 1 }).Simplify(3)
                        );
                    yield return new GCDFTestCase
                        (
                            new Polynomial(new int[] { 2, 0, 1 }),
                            new Polynomial(new int[] { 2 }),
                            3,
                            new Polynomial(new int[] { 1 }).Simplify(3)
                        );
                    yield return new GCDFTestCase
                        (
                            new Polynomial(new int[] { 5 }),
                            new Polynomial(new int[] { 4 }),
                            3,
                            new Polynomial(new int[] { 1 }).Simplify(13)
                        );
                    yield return new GCDFTestCase
                        (
                            new Polynomial(new int[] { 3, 0, 1 }),
                            new Polynomial(new int[] { 2, 1 }),
                            7,
                            new Polynomial(new int[] { 2, 1 }).Simplify(7)
                        );
                    yield return new GCDFTestCase
                        (
                            new Polynomial(new int[] { 2, 5, 7, 10, 6 }),
                            new Polynomial(new int[] { 1, 2, 3, 4, 2 }),
                            5,
                            new Polynomial(new int[] { 1, 1, 2, 2 })
                        );
                    yield return new GCDFTestCase
                        (
                            new Polynomial(new int[] { 3, 3, 3, 3, 2, 1, 0, 2, 3, 1, 2, 1, 0, 1, 2, 3 }),
                            new Polynomial(new int[] { 4, 2, 0, 2, 0, 4, 1, 4, 2, 0, 4, 2, 4, 1, 1, 4, 4, 1 }),
                            5,
                            new Polynomial(new int[] { 4, 3, 2, 2, 3, 1, 0, 0, 2, 4, 4 }).Simplify(5)
                        );
                }
            }

        }
        [Test, TestCaseSource(typeof(GCDFTestCase), "GCDFDataToTest")]
        public void GCDFTest(GCDFTestCase testCase)
        {
            var result = PolumonialGCDF.GCDF(testCase.a, testCase.b, testCase.field);
            Assert.AreEqual(testCase.GCD, result);
        }
    }

}
