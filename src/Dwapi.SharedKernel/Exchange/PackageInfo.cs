namespace Dwapi.SharedKernel.Exchange
{
    public class PackageInfo
    {
        public int PageCount { get;  }
        public long TotalRecords { get;  }

        public int PageSize { get;  }
        public PackageInfo(int pageCount, long totalRecords, int pageSize)
        {
            PageCount = pageCount;
            TotalRecords = totalRecords;
            PageSize = pageSize;
        }
    }
}
