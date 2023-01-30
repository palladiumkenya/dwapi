using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.SharedKernel.Utility
{
    public class LiveSeek
    {
        private List<LiveCursor> _liveCursors = new List<LiveCursor>();
        private long _size;
        public IReadOnlyList<LiveCursor> LiveCursors => _liveCursors;
        public long PageCount => _liveCursors.Count;
        public long PageSize => _size;

        private LiveSeek()
        {
        }

        public LiveSeek(long size, long lastRow, long firstRow = 1)
        {
            _size = size;
            if (size == 0 || size < 0)
                throw new ArgumentException("only positive values allowed");

            int index = 1;
            for (long theBegin = firstRow; theBegin <= lastRow; theBegin = theBegin + size)
            {
                long theEnd = theBegin + (size - 1);
                theEnd = theEnd > lastRow ? -1 : theEnd;
                var si = new LiveCursor(index, theBegin, theEnd);
                _liveCursors.Add(si);
                index++;
            }
        }
    }

    public class LiveCursor
    {
        public long BeginRow { get; }
        public long EndRow { get; }
        public long Index { get; }

        public bool IsLastPage => EndRow == -1;

        private LiveCursor() { }

        public LiveCursor(long index, long beginRow, long endRow)
        {
            if (index == 0 || endRow == 0 || beginRow == 0)
                throw new ArgumentException("only positive values allowed");

            if (index < 0 || endRow < 0 || beginRow < 0)
                throw new ArgumentException("only positive values allowed");

            Index = index;
            BeginRow = beginRow;
            EndRow = endRow;
        }

        public override string ToString()
        {
            string end = EndRow > 0 ? $"- {EndRow}" : $">";
            return $"{Index} | {BeginRow} {end}";
        }
    }
}
