using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCDPF
{
    public static class Int32Extension
    {
        public static int ReverseMul(this Int32 b, int field)
        {
            if (field == 0) throw new ArgumentException("Field can not be zero");
            if (b == 0) throw new ArgumentException("Can not find reverse element for zero");

            int rev_b;
            int d;
            EEa(field, b, out d, out int s, out rev_b);

            if (rev_b < 0) rev_b += field;
            if (rev_b * b % field != 1) throw new ArgumentException("Given field and number was not relative prime");
            return rev_b;
        }

        public static bool IsPrime(this Int32 number)
        {
            int upperBorder = ((int)Math.Sqrt(number)) + 1;
            bool isPrime = true;
            if (number == 0 ||
                number == 1 ||
                number == 2 ||
                number == 3)
                return true;
            for (int i = 3; i < upperBorder; i++)
            {
                if (number % i == 0)
                    isPrime = false;
            }
            return isPrime;
        }

        private static void EEa(int a, int b, out int d, out int x, out int y)
        {
            if (b == 0)
            {
                d = a;
                x = 1;
                y = 0;
                return;
            }
            EEa(b, a % b, out d, out x, out y);
            int tmp;
            tmp = y;
            y = x - (a / b) * (y);
            x = tmp;
        }
    }
}
