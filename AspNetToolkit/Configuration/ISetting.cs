using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.Configuration {
	public interface ISetting {
		string Id { get; set; }
		string Value { get; set; }
	}
}
