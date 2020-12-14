using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetToolkit.Filtering {
	public class SearchRequest {
		public List<FilterItem> FilterItems { get; set; }
		public int? Page { get; set; }
		public int? PageSize { get; set; }

		public async Task<IQueryable<T>> PaginateQuery<T, R>(IQueryable<T> query, SearchResponse<R> response) {
			if (Page == null) {
				if (PageSize != null && PageSize > 0) {
					response.Count = await query.CountAsync();
					response.PageCount = (int)Math.Ceiling(response.Count / (double)PageSize);
					if (response.PageCount == 0) {
						response.PageCount = 1;
					}
					query = query.Skip(0).Take(PageSize.Value);
				}
				else {
					response.PageCount = 1;
				}
			}
			else {
				query = query.Skip((Page.Value - 1) * PageSize.Value).Take(PageSize.Value);
			}
			return query;
		}

		public async Task<SearchResponse<T>> CreateResponse<T>(IQueryable<T> query, IEnumerable<IFilterItemStrategy<T>> filterItemStrategies) {
			var predicate = await FilterItems?.GetPredicate(filterItemStrategies);
			if (predicate != null) {
				query = query.Where(predicate);
			}
			var response = new SearchResponse<T>();
			query = await PaginateQuery(query, response);
			response.Items = await query.ToListAsync();
			return response;
		}

	}
}
