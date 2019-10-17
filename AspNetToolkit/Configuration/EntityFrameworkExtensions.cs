using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetToolkit.Configuration {
	public static class EntityFrameworkExtensions {
		public static IConfigurationBuilder AddEntityFrameworkConfig<T>(this IConfigurationBuilder builder, ISettingsContextFactory<T> factory)
			where T : class, ISetting {
			return builder.Add(new EFConfigurationSource<T>(factory));
		}
	}
}
