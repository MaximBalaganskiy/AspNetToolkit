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
			foreach (var fig in filterItems.GroupBy(x => x.Name)) {
				Expression<Func<T, bool>> groupPredicate = null;
				var strategy = strategies.FirstOrDefault(x => x.GetType().Name.Replace("FilterItemStrategy", "") == fig.Key);
				if (strategy != null) {
					foreach (var fi in fig) {
						var fiPredicate = strategy.GetPredicate(fi);
						if (fiPredicate != null) {
							groupPredicate = groupPredicate == null ? fiPredicate : groupPredicate.Or(fiPredicate);
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
