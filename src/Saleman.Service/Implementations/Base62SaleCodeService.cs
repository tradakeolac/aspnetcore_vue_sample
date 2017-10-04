namespace Saleman.Service.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Base62SaleCodeService : ISaleCodeService
    {
        private readonly static string ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly static int BASE = ALPHABET.Length;

        public long Decode(string saleCode)
        {
            return ToBase10(saleCode.Reverse().ToArray());
        }

        public string GenerateCode(long saleId)
        {
            var sb = new StringBuilder("");
            if (saleId == 0)
            {
                return "a";
            }
            while (saleId > 0)
            {
                saleId = FromBase10(saleId, sb);
            }
            return sb.ToString().Reverse().ToString();
        }


        private static long FromBase10(long i, StringBuilder sb)
        {
            int rem = (int)(i % BASE);
            sb.Append(ALPHABET.ElementAt(rem));
            return i / BASE;
        }

        private static int ToBase10(int n, int pow)
        {
            return n * (int)Math.Pow(BASE, pow);
        }

        private static int ToBase10(char[] chars)
        {
            int n = 0;
            for (int i = chars.Length - 1; i >= 0; i--)
            {
                n += ToBase10(ALPHABET.IndexOf(chars[i]), i);
            }
            return n;
        }
    }
}
