using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AspNetToolkit.Filtering {
	public enum FilterOperator {
		Like = 0,
		NotLike = 1,
		Is = 2,
		IsNot = 3,
		LessThan = 4,
		GreaterThan = 5,
		IsBefore = 6,
		IsAfter,
		Between
		IsAfter = 7,
		IsOnOrAfter = 8,
		IsBeforeOrOn = 9,
		LessThanOrEqual = 10,
		GreaterThanOrEqual = 11
	}
}
