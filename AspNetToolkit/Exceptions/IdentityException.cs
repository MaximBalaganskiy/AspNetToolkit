using Microsoft.AspNetCore.Identity;
using System;

namespace AspNetToolkit.Exceptions {
	[Serializable]
	public class IdentityException : ApiException {
		public void SetErrors(IdentityResult result) {
			foreach (var e in result.Errors)
				Data.Add(e.Code, e.Description);
		}
	}
}
