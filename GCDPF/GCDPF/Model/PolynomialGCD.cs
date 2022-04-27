using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCDPF
{
    public static class PolumonialGCDF
    {
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

        public static Polynomial Simplify(this Polynomial a, int field)
        {
            if (a.Values[a.Degree] != 1)
            {
                int b_const = a.Values[a.Degree].ReverseMul(field);
                a = a * new Polynomial(new int[] { b_const });
                a = a.ToField(field);
            }
            return a;
        }
    }
}
