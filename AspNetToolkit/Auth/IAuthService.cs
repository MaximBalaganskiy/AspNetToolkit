using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetToolkit.Auth {
	public interface IAuthService<TUser> where TUser : IdentityUser {
		Task<TUser> ValidateCredentials(string email, string password, string securityStamp, bool lockoutOnFailure);
		Task<ClaimsPrincipal> CreatePrincipal(TUser u, Claim[] claims = null);
		Task<ClaimsPrincipal> CreateRefreshPrincipal(TUser u);
		ClaimsPrincipal ValidateToken(string token, bool validateLifetime);
		string GenerateToken(ClaimsPrincipal claimsPrincipal, DateTime expiryDate);
	}
}
