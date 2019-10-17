using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetToolkit.Configuration {
	public class EFConfigurationSource<T> : IConfigurationSource where T : class, ISetting {
		private readonly ISettingsContextFactory<T> _factory;

		public static EFConfigurationProvider<T> Provider;

		public EFConfigurationSource(ISettingsContextFactory<T> factory) {
			_factory = factory;
		}

		public IConfigurationProvider Build(IConfigurationBuilder builder) {
			// this is a hack but the only way to get to the provider from a controller to trigger a refresh
			Provider = new EFConfigurationProvider<T>(_factory);
			return Provider;
		}
	}
}
