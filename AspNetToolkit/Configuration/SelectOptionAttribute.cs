using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetToolkit.Configuration {
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class SelectOptionAttribute : Attribute {
		public SelectOptionAttribute(object key, object value) {
			Key = key;
			Value = value;
		}

		public object Key { get; set; }
		public object Value { get; set; }
	}
}
