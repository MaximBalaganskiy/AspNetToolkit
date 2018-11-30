using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.AddressLookup.Mappify {
	internal class AutocompleteResponse {
		public string Type { get; set; }
		public List<Address> Result { get; set; }
		public decimal Confidence { get; set; }
	}
}
