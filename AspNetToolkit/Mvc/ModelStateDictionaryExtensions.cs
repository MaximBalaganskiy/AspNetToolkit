using AspNetToolkit.Exceptions;

namespace Microsoft.AspNetCore.Mvc.ModelBinding {
	public static class ModelStateDictionaryExtensions {
		public static void Assert(this ModelStateDictionary modelState) {
			if (!modelState.IsValid)
				throw new ModelStateException(modelState);
		}
	}
}
