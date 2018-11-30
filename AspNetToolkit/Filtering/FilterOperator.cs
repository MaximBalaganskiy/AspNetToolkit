using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AspNetToolkit.Filtering {
	public enum FilterOperator {
		Like,
		NotLike,
		Is,
		IsNot,
		LessThan,
		GreaterThan,
		IsBefore,
		IsAfter
	}
}
