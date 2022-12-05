using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GoFish
{
    class ComparerDesk : IComparer<Card>
    {
        public int Compare([AllowNull] Card x, [AllowNull] Card y)
        {
           if (x.Value > y.Value)
           {
               return 1;
           }
           else if (x.Value < y.Value)
           {
               return -1;
           }
           return 0;
        }
    }
}
