using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetToolkit.Configuration {
	public class SettingInfoAttribute : Attribute {
		public SettingInfoAttribute() { }

		public SettingInfoAttribute(string name) {
			Name = name;
		}

		public string Name { get; set; }
		public string Description { get; set; }
		public ClientEditor ClientEditor { get; set; }
	}
}
