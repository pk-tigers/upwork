using System;
namespace UpWork.Common.Models
{
	public class PaginatedResult<T>
	{
		public IEnumerable<T> Data { get; init; }
		public int Count { get; init; }

		public PaginatedResult(IEnumerable<T> data, int count)
		{
			Data = data;
			Count = count;
		}
	}
}

