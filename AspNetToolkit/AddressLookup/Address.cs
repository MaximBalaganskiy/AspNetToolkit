using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.AddressLookup {
	public class Address {
		public string BuildingName { get; set; }
		public string FlatNumberPrefix { get; set; }
		public int? FlatNumber { get; set; }
		public string FlatNumberSuffix { get; set; }
		public int? LevelNumber { get; set; }
		public int? NumberFirst { get; set; }
		public int? NumberLast { get; set; }
		public string StreetName { get; set; }
		public string StreetType { get; set; }
		public string Suburb { get; set; }
		public string State { get; set; }
		public string PostCode { get; set; }
		public Location Location { get; set; }
		public bool Primary { get; set; }
		public string StreetAddress { get; set; }
	}
}
