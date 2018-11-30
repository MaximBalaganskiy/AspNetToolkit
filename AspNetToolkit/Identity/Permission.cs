using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetToolkit.Identity {
	public class Permission<T> where T : struct, IConvertible {
		public T Code { get; set; }
		public string Name { get; set; }
	}
}
