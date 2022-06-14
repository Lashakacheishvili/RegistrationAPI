using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceModels
{
    public class BaseListingResponse<T>
    {
        public int TotalCount { get; protected set; }
        public IEnumerable<T> List { get; protected set; }
        public BaseListingResponse(int totalCount, IEnumerable<T> list)
        {
            List = list;
            TotalCount = totalCount;
        }
    }
}
