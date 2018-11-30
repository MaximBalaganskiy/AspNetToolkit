using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetToolkit.Configuration {
	public class SettingInfo {
		public string Key { get; set; }
		public object Value { get; set; }
		public string TypeName { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<SelectOption> Options { get; set; }
		public ClientEditor ClientEditor { get; set; }
		public List<SettingInfo> Children { get; set; }

		public void HydrateSettingInfo(object o) {
			var type = o.GetType();
			TypeName = type.Name;
			var props = type.GetProperties();
			if (type.IsClass && type != typeof(string) && props.Count() > 0) {
				Children = new List<SettingInfo>();
			}
			else {
				Value = o;
			}
			foreach (var p in props) {
				var sia = p.GetCustomAttributes(false).OfType<SettingInfoAttribute>().FirstOrDefault();
				var child = new SettingInfo {
					Key = Key + ":" + p.Name,
					Name = sia?.Name ?? p.Name,
					Description = sia?.Description,
					ClientEditor = sia?.ClientEditor ?? ClientEditor.String
				};
				if (sia?.ClientEditor == ClientEditor.Select) {
					child.Options = p.GetCustomAttributes(false).OfType<SelectOptionAttribute>().Select(x => new SelectOption { Key = x.Key, Value = x.Value }).ToList();
				}
				if (Children != null) {
					Children.Add(child);
					child.HydrateSettingInfo(p.GetValue(o));
				}
			}
		}
	}
}
