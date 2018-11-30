using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetToolkit.Filtering {
	public interface IFilterItemStrategy<T> {
		Expression<Func<T, bool>> GetPredicate(FilterItem filterItem);
	}
}
