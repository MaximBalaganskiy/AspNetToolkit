using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.Db {
	public class Setting : Entity {
		public string Id { get; set; }
		public string Value { get; set; }
	}
}
