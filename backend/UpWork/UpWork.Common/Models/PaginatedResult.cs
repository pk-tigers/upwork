using System;
namespace UpWork.Common.Models
{
    public class PaginatedResult<T>
    {
        public IEnumerable<T> Data { get; init; }
        public int Count { get; init; }
        public int Page { get; set; }

        public PaginatedResult(IEnumerable<T> data, int count, int take = 10)
        {
            Data = data;
            Count = count;
            Page = (int)Math.Ceiling((decimal)count / (decimal)take);
        }
    }
}

