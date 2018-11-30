using AspNetToolkit.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity {
	public static class IdentityResultExtensions {
		public static IdentityResult Assert<E>(this IdentityResult res) where E : IdentityException, new() {
			if (res.Succeeded)
				return res;

			var e = new E();
			e.SetErrors(res);
			throw e;
		}

		public static async Task<IdentityResult> Assert<E>(this Task<IdentityResult> res) where E : IdentityException, new() {
			return (await res).Assert<E>();
		}

		public static IdentityResult Assert(this IdentityResult res) {
			return Assert<IdentityException>(res);
		}

		public static async Task<IdentityResult> Assert(this Task<IdentityResult> res) {
			return (await res).Assert();
		}
	}
}
