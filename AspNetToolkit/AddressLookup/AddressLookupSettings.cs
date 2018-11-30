using AspNetToolkit.AddressLookup.Mappify;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.AddressLookup {
	public class AddressLookupSettings {
		public string Provider { get; set; }
		public MappifySettings Mappify { get; set; }
	}
}
