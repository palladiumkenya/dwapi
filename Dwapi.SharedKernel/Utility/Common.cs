using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.SharedKernel.Utility
{
    public static class Common
    {
        public static int GetProgress(int count,int total)
        {
            return (int)Math.Round((double)(100 * count) / total);
        }

        public static int GetProgress(int count,int total,int share)
        {
            return (int)Math.Round((double)(share/100 * count) / total);
        }
    }
}
