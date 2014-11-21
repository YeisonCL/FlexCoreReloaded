using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreLogic.general
{
    public class Utils
    {
        public static int getMaxPage(float pCount, int pShowCount)
        {
            float div = pCount / pShowCount;
            if (div % 1 > 0)
            {
                div++;
            }
            return (int)div;
        }
    }
}
