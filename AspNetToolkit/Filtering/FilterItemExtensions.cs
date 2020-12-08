using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AspNetToolkit.Filtering {
	public static class FilterItemExtensions {
		public static Expression<Func<T, bool>> GetPredicate<T>(this List<FilterItem> filterItems, IEnumerable<IFilterItemStrategy<T>> strategies) {
			Expression<Func<T, bool>> predicate = null;

			// filter items with the same Guid and operators Equals or Like should be ORed instead of ANDed
			// this will allow searches similar to "Parameter Name like Crypt OR Parameter Name like Giard"
			var groups = from filterItem in filterItems
						 group filterItem by new {
							 filterItem.Name,
							 IsOred = filterItem.Operator == FilterOperator.Is || filterItem.Operator == FilterOperator.Like
						 };

			foreach (var fig in groups) {
				Expression<Func<T, bool>> groupPredicate = null;
				var strategy = strategies.FirstOrDefault(x => x.GetType().Name.Replace("FilterItemStrategy", "") == fig.Key.Name);
				if (strategy != null) {
					foreach (var fi in fig) {
						var fiPredicate = strategy.GetPredicate(fi);
						if (fiPredicate != null) {
							groupPredicate = groupPredicate == null
								? fiPredicate
								: (fig.Key.IsOred
									? groupPredicate.Or(fiPredicate)
									: groupPredicate.And(fiPredicate));
						}
					}
					if (groupPredicate != null) {
						predicate = predicate == null ? groupPredicate : predicate.And(groupPredicate);
					}
				}
			}
			return predicate;
		}
	}
}
