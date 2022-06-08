using System;

namespace Dwapi.SharedKernel.Utility
{
    public static class Common
    {
        public static int GetProgress(int count, int total)
        {
            var res=(int) Math.Round((double) (100 * count) / total);
            if (res < 0)
                return 100;
            return  res;
        }
    }
}
