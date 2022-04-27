using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCDPF
{
    public struct Polynomial : IEquatable<Polynomial>
    {
        private int[] _values;
        public int Degree => Values.Length - 1;
        public int Length => Values.Length;

        public int[] Values
        {
            get
            {
                return _values ?? new int[] { 0 };
            }
        }

        public Polynomial(int[] values)
        {
            _values = values.Reverse().ToArray();
        }

        public override string ToString()
        {
            string s = "";
            for (int i = Degree; i >= 0; i--)
            {
                int k = Values[i];
                if (k != 0)
                {
                    s += (k > 0 ? $" + {k}" : $" - {-k}") + $"x^{i}";
                }
            }
            s += " ";
            s = s.Replace("^1 ", " ");
            s = s.Replace("x^0", "");
            s = s.Replace(" 1x", " x");
            s = s.Trim(' ', '+', ' ');
            return s != "" ? s : "0";
        }

        public static Polynomial operator +(Polynomial a, Polynomial b) => Addition(a, b);
        public static Polynomial operator -(Polynomial a, Polynomial b) => Subtraction(a, b);
        public static Polynomial operator *(Polynomial a, Polynomial b) => Multiplication(a, b);

        public static bool operator ==(Polynomial left, Polynomial right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Polynomial left, Polynomial right)
        {
            return !(left == right);
        }

        private static Polynomial Addition(Polynomial a, Polynomial b)
        {

            Polynomial Result;
            if (a.Degree < b.Degree)
            {
                Result = b;
                for (int i = 0; i < a.Length; i++)
                {
                    Result.Values[i] += a.Values[i];
                }
            }
            else
            {
                Result = a;
                for (int i = 0; i < b.Length; i++)
                {
                    Result.Values[i] += b.Values[i];
                }
            }
            Result.Trim();
            return Result;
        }

        private static Polynomial Subtraction(Polynomial a, Polynomial b)
        {
            int Result_length = Math.Max(a.Length, b.Length);
            Polynomial Result = new Polynomial(new int[Result_length]);

            for (int i = 0; i < b.Length; i++)
            {
                Result.Values[i] = -b.Values[i];
            }
            for (int i = 0; i < a.Length; i++)
            {
                Result.Values[i] += a.Values[i];
            }

            Result.Trim();
            return Result;
        }

        private static Polynomial Multiplication(Polynomial a, Polynomial b)
        {
            int Result_length = a.Degree + b.Degree + 1;
            Polynomial Result = new Polynomial(new int[Result_length]);
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    Result.Values[i + j] += a.Values[i] * b.Values[j];
                }
            }
            Result.Trim();
            return Result;
        }



        public bool IsNull
        {
            get
            {
                Trim();
                return Length == 1 && Values[0] == 0;
            }
        }


        public Polynomial ToField(int field)
        {
            Polynomial p = new Polynomial(new int[Length]);
            for (int i = 0; i < Length; i++)
            {
                p.Values[i] = Values[i] % field;
                if (p.Values[i] < 0)
                {
                    p.Values[i] += field;
                }
            }
            p.Trim();
            return p;
        }

        private void Trim()
        {
            while (Length > 1 && Values[Length - 1] == 0)
            {
                Array.Resize(ref _values, Length - 1);
            }
        }

        public override bool Equals(object obj)
        {
            bool isSame = true;
            Polynomial pol = (Polynomial)obj;
            this.Trim();
            pol.Trim();
            if (this.Length == pol.Length)
            {
                for (int i = 0; i < this.Length; i++)
                {
                    if (this.Values[i] != pol.Values[i])
                    {
                        isSame = false;
                        break;
                    }
                }
            }
            else isSame = false;

            return isSame;
        }

        public bool Equals(Polynomial other)
        {
            bool isSame = true;
            this.Trim();
            other.Trim();
            if (this.Length == other.Length)
            {
                for (int i = 0; i < this.Length; i++)
                {
                    if (this.Values[i] != other.Values[i])
                    {
                        isSame = false;
                        break;
                    }
                }
            }
            else isSame = false;

            return isSame;
        }

        public override int GetHashCode()
        {
            var HashCode = 1861411795;
            int shift = 0;
            for (int i = 0; i < Values.Length; i++)
            {
                shift = (shift + 11) % 21;
                HashCode ^= (Values[i] + 1024) << shift;
            }
            return HashCode;
        }

        //Polynomial division over finite field (PDF)
        public static (Polynomial quotient, Polynomial remainder) PDF(Polynomial a, Polynomial b, int field)
        {
            if (b.IsNull) throw new DivideByZeroException("divisor was 0");
            if (a.Degree < b.Degree)
            {
                return (new Polynomial(), a);
            }

            a = a.ToField(field);
            b = b.ToField(field);

            Polynomial subtractor;

            int quotientLength = a.Length - b.Length + 1;
            Polynomial quotient = new Polynomial(new int[quotientLength]);
            Polynomial factor;

            a = a.ToField(field);

            while ((a.Length > b.Length - 1) && (!a.IsNull))
            {
                b = b.ToField(field);
                int SubstractorFactor = a.Values[a.Degree] * b.Values[b.Degree].ReverseMul(field) % field;
                if (SubstractorFactor < 0) SubstractorFactor += field;
                quotient.Values[a.Length - b.Length] = SubstractorFactor;


                int factorLength = a.Length - b.Length + 1;
                factor = new Polynomial(new int[factorLength]);
                factor.Values[factor.Length - 1] = quotient.Values[a.Length - b.Length];
                subtractor = b * factor;

                a -= subtractor;
                a = a.ToField(field);
            }
            quotient = quotient.ToField(field);

            return (quotient, a);
        }

        //Greatest Common Divisor over Finite Fielad (GCDF)
        public static Polynomial GCDF(Polynomial a, Polynomial b, int field)
        {
            Polynomial tmp;
            a = a.ToField(field);
            b = b.ToField(field);
            if (a.Length < b.Length)
            {
                tmp = a;
                a = b;
                b = tmp;
            }
            a = PDF(a, b, field).remainder;
            while (a.Length > 1 && b.Length > 1)
            {
                if (a.Degree > b.Degree)
                {
                    a = PDF(a, b, field).remainder;
                }
                else
                {
                    b = PDF(b, a, field).remainder;
                }
            }
            Polynomial result = (a + b).Simplify(field);
            return result;
        }

        public Polynomial Simplify( int field)
        {
            if (this.Values[this.Degree] != 1)
            {
                int b_const = this.Values[this.Degree].ReverseMul(field);
                this = this * new Polynomial(new int[] { b_const });
                this = this.ToField(field);
            }
            return this;
        }

        private static void EEAPUnreduced(Polynomial a, Polynomial b, out Polynomial d, out Polynomial x, out Polynomial y, int field)//Extended Euclidean algorithm in finite field
        {
            Polynomial s;
            if (b.IsNull)
            {
                d = a;
                x = new Polynomial(new int[] { a.Values[0] });
                y = new Polynomial(new int[] { 0 });

                return;
            }
            EEAPUnreduced(b, PDF(a, b, field).remainder, out d, out x, out y, field);

            s = y;
            y = x - (y * PDF(a, b, field).quotient.ToField(field));
            x = s;
        }

        /// <summary>
        /// Расширенный алгоритм Евклида в поле
        /// Возвращает НОД в приведенном виде
        /// </summary>
        public static void EEAP(Polynomial a, Polynomial b, out Polynomial d, out Polynomial x, out Polynomial y, int field, bool needReduce = true) //Extended Euclidean algorithm in finite field (reduced)
        {
            Polynomial s;
            Polynomial ax;
            Polynomial by;
            int d_const;
            int xy_const;
            Polynomial tmp;
            if (a.Length < b.Length)
            {
                tmp = a;
                a = b;
                b = tmp;
            }

            EEAPUnreduced(a, b, out d, out x, out y, field);
            if (d.Values[d.Degree] != 1 && needReduce)
            {
                d_const = d.Values[d.Degree].ReverseMul(field);
                d = d * new Polynomial(new int[] { d_const });
                d = d.ToField(field);
            }
            ax = a * x;
            by = b * y;
            s = (ax + by).ToField(field);

            xy_const = s.Values[s.Degree].ReverseMul(field);

            x = x * new Polynomial(new int[] { xy_const });
            x = x.ToField(field);
            y = y * new Polynomial(new int[] { xy_const });
            y = y.ToField(field);
        }

        /// <summary>
        /// Расширенный алгоритм Евклида
        /// </summary>
        /// <param name="d">НОД</param>
        /// <param name="x">Множитель сообтошения Безу для a</param>
        /// <param name="y">Множитель сообтошения Безу для b</param>
        public static void EEA(int a, int b, out int d, out int x, out int y)
        {
            if (b == 0)
            {
                d = a;
                x = 1;
                y = 0;
                return;
            }
            EEA(b, a % b, out d, out x, out y);
            int tmp;
            tmp = y;
            y = x - (a / b) * (y);
            x = tmp;
        }


    }
}
