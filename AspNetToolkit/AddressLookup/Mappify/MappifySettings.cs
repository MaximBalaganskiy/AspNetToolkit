using AspNetToolkit.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.AddressLookup.Mappify {
	public class MappifySettings {
		public string Url { get; set; }

		[SettingInfo("Api Key", ClientEditor = ClientEditor.Password)]
		public string ApiKey { get; set; }
	}
}
