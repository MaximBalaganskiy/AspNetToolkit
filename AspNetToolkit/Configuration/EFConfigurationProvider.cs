using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetToolkit.Configuration {
	public class EFConfigurationProvider<T> : ConfigurationProvider where T : class, ISetting {
		private readonly ISettingsContextFactory<T> _factory;

		public EFConfigurationProvider(ISettingsContextFactory<T> factory) {
			_factory = factory;
		}

		// Load config data from EF DB.
		public override void Load() {
			using var context = _factory.Create();
			try {
				Data = context.Settings.ToDictionary(c => c.Id, c => c.Value);
			}
			catch {
				// database or table does not exist, load empty settings
				Data = new Dictionary<string, string>();
			}
			OnReload();
		}
	}
}
