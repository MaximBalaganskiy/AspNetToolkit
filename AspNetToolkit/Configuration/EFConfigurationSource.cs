using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetToolkit.Configuration {
	public class EFConfigurationSource : IConfigurationSource {
		private readonly ISettingsContextFactory _factory;

		public static EFConfigurationProvider Provider;

		public EFConfigurationSource(ISettingsContextFactory factory) {
			_factory = factory;
		}

		public IConfigurationProvider Build(IConfigurationBuilder builder) {
			// this is a hack but the only way to get to the provider from a controller to trigger a refresh
			Provider = new EFConfigurationProvider(_factory);
			return Provider;
		}
	}
}
