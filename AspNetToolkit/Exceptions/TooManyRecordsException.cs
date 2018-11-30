using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.Exceptions {
	[Serializable]
	public class TooManyRecordsException : ApiException {
		public TooManyRecordsException(string message) : base(message) {

		}
	}
}
