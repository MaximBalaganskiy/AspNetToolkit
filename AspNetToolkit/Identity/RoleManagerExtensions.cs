using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity {
	static public class RoleManagerExtensions {
		static public async Task CheckAndAddClaim<TRole, TCode>(this RoleManager<TRole> roleManager, TRole role, TCode permissionCode)
			where TRole : IdentityRole
			where TCode : struct, IConvertible {
			if (!(await roleManager.GetClaimsAsync(role)).Any(x => x.Value == permissionCode.ToString())) {
				await roleManager.AddClaimAsync(role, new Claim(AspNetToolkit.Security.ClaimTypes.Permission, permissionCode.ToString()));
			}
		}

		static public async Task<TRole> CheckAndCreateRole<TRole>(this RoleManager<TRole> roleManager, TRole role)
			where TRole : IdentityRole {
			var r = await roleManager.FindByNameAsync(role.Name);
			if (r == null) {
				await roleManager.CreateAsync(role);
				r = role;
			}
			return r;
		}

	}
}
