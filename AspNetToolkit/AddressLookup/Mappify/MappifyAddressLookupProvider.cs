using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using Pbl = AspNetToolkit.AddressLookup;
using AutoMapper;
using AspNetToolkit.Exceptions;

namespace AspNetToolkit.AddressLookup.Mappify {
	public class MappifyAddressLookupProvider : IAddressLookupProvider {
		private readonly MappifySettings _settings;
		private readonly IMapper _mapper;

		public MappifyAddressLookupProvider(IOptions<AddressLookupSettings> settings, IMapper mapper) {
			_settings = settings.Value.Mappify;
			_mapper = mapper;
		}

		public async Task<List<Pbl.Address>> Autocomplete(string streetAddress) {
			var request = new AutocompleteRequest {
				apiKey = _settings.ApiKey,
				streetAddress = streetAddress
			};
			var client = new HttpClient { BaseAddress = new Uri(_settings.Url) };
			var response = await client.PostAsJsonAsync("address/autocomplete", request);
			if (!response.IsSuccessStatusCode) {
				throw new ApiException(await response.Content.ReadAsStringAsync());
			}
			var content = await response.Content.ReadAsAsync<AutocompleteResponse>();
			return _mapper.Map<List<Pbl.Address>>(content.Result);
		}
	}
}
