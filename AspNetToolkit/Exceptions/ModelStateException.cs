using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace AspNetToolkit.Exceptions {
	[Serializable]
	public class ModelStateException : ApiException {
		public ModelStateException(ModelStateDictionary modelState) : base("See Data for details") {
			foreach (var v in modelState) {
				foreach (var e in v.Value.Errors)
					Data.Add(v.Key, e.ErrorMessage);
			}

		}

	}
}
