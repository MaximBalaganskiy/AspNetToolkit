using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Pbl = AspNetToolkit.AddressLookup;

namespace AspNetToolkit.AddressLookup.Mappify {
	public class AutoMapperProfile : Profile {
		public AutoMapperProfile() {
			CreateMap<Address, Pbl.Address>();
		}
	}
}
