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
    }
}
