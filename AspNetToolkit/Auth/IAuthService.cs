using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetToolkit.Auth {
	public interface IAuthService {
		Task<ClaimsPrincipal> CreatePrincipal(string email, string password, string securityStamp);

		string GetUserEmailFromToken(string refreshToken, string expiredToken);
		ClaimsPrincipal GetClaimsPrincipalFromToken(string refreshToken, string expiredToken);

		string GenerateToken(ClaimsPrincipal claimsPrincipal, DateTime expiryDate);
	}
}
