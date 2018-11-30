using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.AddressLookup {
	public static partial class IServiceCollectionExtensions {
		public static IServiceCollection AddAddressLookup(this IServiceCollection services) {
			services
				.AddTransient<IAddressLookupProvider, Mappify.MappifyAddressLookupProvider>();
			return services;
		}
	}
}
