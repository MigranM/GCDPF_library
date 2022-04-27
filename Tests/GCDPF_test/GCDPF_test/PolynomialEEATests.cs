using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCDPF;

namespace GCDPF_test
{
    public class PolynomialEEATests
    {

        [Test]
        public void EEAExtremeTest()
        {
            int a = 0, b = 0, x, y, d;

            Polynomial.EEA(a, b, out d, out x, out y);

            Assert.AreEqual(1, x);
            Assert.AreEqual(0, y);
            Assert.AreEqual(0, d);
        }


        [Test]
        public void EEAPExtremeTest()
        {
            Polynomial a = new Polynomial(new int[1] { 1 });
            Polynomial b = new Polynomial(new int[1] { 0 });

            Polynomial d = new Polynomial();
            Polynomial x = new Polynomial();
            Polynomial y = new Polynomial();

            int field = 3;

            Polynomial.EEAP(a, b, out d, out x, out y, field);

            Assert.AreEqual(new Polynomial(new int[] { 1 }), d);
            Assert.AreEqual(new Polynomial(new int[] { 1 }), x);
            Assert.AreEqual(new Polynomial(new int[] { 0 }), y);

            a = new Polynomial(new int[1] { 0 });
            b = new Polynomial(new int[1] { 1 });

            d = new Polynomial();
            x = new Polynomial();
            y = new Polynomial();


            Polynomial.EEAP(a, b, out d, out x, out y, field);

            Assert.AreEqual(new Polynomial(new int[] { 1 }), d);
            Assert.AreEqual(new Polynomial(new int[] { 0 }), x);
            Assert.AreEqual(new Polynomial(new int[] { 1 }), y);
        }

        [Test]
        public void EEAPExceptionTest()
        {
            Polynomial a = new Polynomial(new int[1] { 0 });
            Polynomial b = new Polynomial(new int[1] { 0 });

            Polynomial d = new Polynomial();
            Polynomial x = new Polynomial();
            Polynomial y = new Polynomial();

            int field = 3;

            Assert.Throws<ArgumentException>(() => Polynomial.EEAP(a, b, out d, out x, out y, field));
        }

        [TestFixture]
        public class EEAPTestCase
        {
            public Polynomial a { get; private set; }
            public Polynomial b { get; private set; }

            public Polynomial _d;
            public Polynomial d { get => _d; private set
                {
                    _d = value;
                }
            }
            public Polynomial _x;
            public Polynomial x
            {
                get => _x; private set
                {
                    _x = value;
                }
            }
            public Polynomial _y;
            public Polynomial y
            {
                get => _y; private set
                {
                    _y = value;
                }
            }

            public Polynomial excpected_d { get; private set; }
            public Polynomial excpected_x { get; private set; }
            public Polynomial excpected_y { get; private set; }

            public int field { get; private set; }

            public bool needReduce { get; private set; }

            public EEAPTestCase(
                Polynomial a, 
                Polynomial b, 
                Polynomial d, 
                Polynomial x, 
                Polynomial y, 
                int field, 
                bool needReduce,
                Polynomial excpected_d,
                Polynomial excpected_x,
                Polynomial excpected_y
                )
            {
                this.a = a;
                this.b = b;
                this.d = d;
                this.x = x;
                this.y = y;
                this.field = field;
                this.needReduce = needReduce;
                this.excpected_d = excpected_d;
                this.excpected_x = excpected_x;
                this.excpected_y = excpected_y;
                Polynomial.EEAP(a, b, out d, out x, out y, field, needReduce);
            }

            private static IEnumerable<EEAPTestCase> EEAPDataToTest
            {
                get
                {
                    yield return new EEAPTestCase
                        (
                            new Polynomial(new int[] { 1, 0, 2 }),
                            new Polynomial(new int[] { 3, 1 }),
                            new Polynomial(),
                            new Polynomial(),
                            new Polynomial(),
                            5,
                            true,
                            new Polynomial(new int[] { 1 }),
                            new Polynomial(new int[] { 1 }),
                            new Polynomial(new int[] { 3, 4 })
                        );
                    yield return new EEAPTestCase
                        (
                            new Polynomial(new int[] { 3, 7, 8, 3, 6, 2 }),
                            new Polynomial(new int[] { 4, 4, 6, 2, 4 }),
                            new Polynomial(),
                            new Polynomial(),
                            new Polynomial(),
                            11,
                            true,
                            new Polynomial(new int[] { 1 }),
                            new Polynomial(new int[] { 3, 1, 1, 0 }),
                            new Polynomial(new int[] { 6, 10, 0, 9, 3 })
                        );
                    yield return new EEAPTestCase
                        (
                            new Polynomial(new int[] { 168, 124 }),
                            new Polynomial(new int[] { 186, 0, 186 }),
                            new Polynomial(),
                            new Polynomial(),
                            new Polynomial(),
                            1559,
                            true,
                            new Polynomial(new int[] { 1 }),
                            new Polynomial(new int[] { 1382 }),
                            new Polynomial(new int[] { 363, 957 })
                        );
                }
            }

        }
        [Test, TestCaseSource(typeof(EEAPTestCase), "EEAPDataToTest")]
        public void EEAPTest(EEAPTestCase testCase)
        {
            Polynomial.EEAP(testCase.a, testCase.b, out testCase._d, out testCase._x, out testCase._y, testCase.field, testCase.needReduce);
            Assert.AreEqual(testCase.excpected_d, testCase.d);
            Assert.AreEqual(testCase.excpected_x, testCase.x);
            Assert.AreEqual(testCase.excpected_y, testCase.y);
        }
    }
}
