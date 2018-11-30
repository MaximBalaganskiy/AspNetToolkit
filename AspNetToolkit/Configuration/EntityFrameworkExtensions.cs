using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetToolkit.Configuration {
	public static class EntityFrameworkExtensions {
		public static IConfigurationBuilder AddEntityFrameworkConfig(
			this IConfigurationBuilder builder, ISettingsContextFactory factory) {
			return builder.Add(new EFConfigurationSource(factory));
		}
	}
}
