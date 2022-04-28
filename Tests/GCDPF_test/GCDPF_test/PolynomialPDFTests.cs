using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using GCDPF;

namespace GCDPFTest
{
    public class PolynomialPDFTest
    {
        [TestFixture]
        public class PDFTestCase
        {
            public Polynomial dividend { get; private set; }
            public Polynomial divider { get; private set; }

            public int field { get; private set; }

            public Polynomial quotient { get; private set; }
            public Polynomial remainder { get; private set; }

            public PDFTestCase(Polynomial dividend, Polynomial divider, int field, (Polynomial quotient, Polynomial remainder) result)
            {
                this.dividend = dividend;
                this.divider = divider;
                this.field = field;
                this.quotient = result.quotient;
                this.remainder = result.remainder;
            }

            private static IEnumerable<PDFTestCase> PDFDataToTest
            {
                get
                {
                    yield return new PDFTestCase
                        (
                            new Polynomial(new int[] { 3, 2, 1 }),
                            new Polynomial(new int[] { 6 }),
                            7,
                            (
                                new Polynomial(new int[] { 4, 5, 6 }),
                                new Polynomial(new int[] { 0 })
                            )
                        );
                    yield return new PDFTestCase
                        (
                            new Polynomial(new int[] { 1, 2, 1 }),
                            new Polynomial(new int[] { 1, 2 }),
                            5,
                            (
                                new Polynomial(new int[] { 1, 0 }),
                                new Polynomial(new int[] { 1 })
                            )
                        );
                    yield return new PDFTestCase
                        (
                            new Polynomial(new int[] { 1, 3, 2 }),
                            new Polynomial(new int[] { 2, 3 }),
                            5,
                            (
                                new Polynomial(new int[] { 3, 2 }),
                                new Polynomial(new int[] { 1 })
                            )
                        );
                    yield return new PDFTestCase
                        (
                            new Polynomial(new int[] { 6, 0, -2, 7 }),
                            new Polynomial(new int[] { 4, 3, 0 }),
                            11,
                            (
                                new Polynomial(new int[] { 7, 3 }),
                                new Polynomial(new int[] { 7 })
                            )
                        );
                    yield return new PDFTestCase
                        (
                            new Polynomial(new int[] { 12, -4, 8, 6, 0, 0, 4, -13, 7, 0, 2 }),
                            new Polynomial(new int[] { -2, 0, 5, 11, 5, 1, 8 }),
                            17,
                            (
                                new Polynomial(new int[] { 11, 2, 15, 3, 8 }),
                                new Polynomial(new int[] { 3, 4, 6, 14, 2, 6 })
                            )
                        );
                    yield return new PDFTestCase
                        (
                            new Polynomial(new int[] { 12, -4, 8, 6, 0, 0, 4, -13, 7, 0, 2 }),
                            new Polynomial(new int[] { -2, 0, 5, 11, 5, 1, 8 }),
                            17,
                            (
                                new Polynomial(new int[] { 11, 2, 15, 3, 8 }),
                                new Polynomial(new int[] { 3, 4, 6, 14, 2, 6 })
                            )
                        );

                }
            }
        }
        [Test, TestCaseSource(typeof(PDFTestCase), "PDFDataToTest")]
        public void PDFTest(PDFTestCase testCase)
        {
            var result = Polynomial.PDF(testCase.dividend, testCase.divider, testCase.field);
            Assert.AreEqual(testCase.quotient, result.quotient);
            Assert.AreEqual(testCase.remainder, result.remainder);
        }

        [Test]
        public void PDFExtremeTest()
        {
            Polynomial a = new Polynomial();
            Polynomial b = new Polynomial(new int[1] { 1 });
            Polynomial quotient = new Polynomial();
            Polynomial remainder = new Polynomial();
            Assert.AreEqual((quotient, remainder), Polynomial.PDF(a, b, 10));
        }
        [Test]
        public void PDFExceptionTest()
        {
            Polynomial a = new Polynomial();
            Polynomial b = new Polynomial(new int[1]);
            Assert.Catch(typeof(DivideByZeroException), () => Polynomial.PDF(b, a, 10));
        }

    }

}
