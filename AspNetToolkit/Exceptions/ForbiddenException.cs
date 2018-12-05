using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetToolkit.Exceptions {
	public class ForbiddenException : ApiException {
		public ForbiddenException() : base("Forbidden") { }
	}
}
