using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocHelper.Utilities
{
    public static class Utilities
    {
        public static long Lcm(long n1, long n2)
        {
            long c1 = n1;
            long c2 = n2;

            while (c1 != c2)
            {
                if (c1 < c2)
                    c1 += n1;
                else 
                    c2 += n2;
            }

            return c1;
        }
    }
}
