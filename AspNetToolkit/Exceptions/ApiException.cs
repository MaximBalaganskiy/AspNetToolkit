using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.Exceptions {
	[Serializable]
	public class ApiException : Exception {
		public ApiException() {

		}

		public ApiException(string message, Exception innerException) : base(message, innerException) {

		}

		public ApiException(string message) : base(message) {

		}
	}
}
