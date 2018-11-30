using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetToolkit.AddressLookup {
	public interface IAddressLookupProvider {
		Task<List<Address>> Autocomplete(string streetAddress);
	}
}
