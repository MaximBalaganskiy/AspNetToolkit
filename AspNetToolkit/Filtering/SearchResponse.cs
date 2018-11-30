using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.Filtering {
	public class SearchResponse<T> {
		public List<T> Items { get; set; }
		public int? PageCount { get; set; }
		public int Count { get; set; }
	}
}
