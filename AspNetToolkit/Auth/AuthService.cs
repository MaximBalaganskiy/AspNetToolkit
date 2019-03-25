using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using AspNetToolkit.Exceptions;

namespace AspNetToolkit.Auth {
	public class AuthService<TUser, TRole> : IAuthService<TUser>
		where TUser : IdentityUser
		where TRole : IdentityRole {
		private readonly JwtBearerOptions _jwtOptions;
		private readonly UserManager<TUser> _userManager;
		private readonly RoleManager<TRole> _roleManager;

		public AuthService(IOptions<JwtBearerOptions> jwtOptions, UserManager<TUser> userManager, RoleManager<TRole> roleManager) {
			_jwtOptions = jwtOptions.Value;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<TUser> ValidateCredentials(string email, string password, string securityStamp, bool lockoutOnFailure) {
			var u = await _userManager.FindByNameAsync(email);
			if (u == null) {
				throw new InvalidCredentialsException();
			}
			if (securityStamp != null && securityStamp != u.SecurityStamp) {
				throw new InvalidCredentialsException();
			}
			if (_userManager.SupportsUserLockout && await _userManager.IsLockedOutAsync(u)) {
				throw new LockedOutException();
			}

			if (password != null && !await _userManager.CheckPasswordAsync(u, password)) {
				if (_userManager.SupportsUserLockout && lockoutOnFailure) {
					// If lockout is requested, increment access failed count which might lock out the user
					await _userManager.AccessFailedAsync(u);
				}
				throw new InvalidCredentialsException();
			}
			if (_userManager.SupportsUserLockout) {
				await _userManager.ResetAccessFailedCountAsync(u);
			}
			if (!await _userManager.IsEmailConfirmedAsync(u)) {
				throw new UnconfirmedEmailException();
			}
			return u;
		}

		protected virtual Task AddBasicClaims(TUser u, ClaimsIdentity ci) {
			ci.AddClaims(new[]
			{
				new Claim(Security.ClaimTypes.UserId, u.Id),
				new Claim(ClaimTypes.Email, u.Email),
				new Claim(Security.ClaimTypes.SecurityStamp, u.SecurityStamp)
			});
			return Task.CompletedTask;
		}

		protected virtual async Task AddUserAndRoleClaims(TUser u, ClaimsIdentity ci) {
			ci.AddClaims(await _userManager.GetClaimsAsync(u));
			var roles = await _userManager.GetRolesAsync(u);
			foreach (var r in roles) {
				ci.AddClaim(new Claim(ci.RoleClaimType, r));
				var role = await _roleManager.FindByNameAsync(r);
				ci.AddClaims(await _roleManager.GetClaimsAsync(role));
			}
		}

		public async Task<ClaimsPrincipal> CreatePrincipal(TUser u) {
			var ci = new ClaimsIdentity();
			await AddBasicClaims(u, ci);
			await AddUserAndRoleClaims(u, ci);
			return new ClaimsPrincipal(ci);
		}

		public async Task<ClaimsPrincipal> CreateRefreshPrincipal(TUser u) {
			var ci = new ClaimsIdentity();
			await AddBasicClaims(u, ci);
			ci.AddClaim(new Claim("scope", "refresh"));
			return new ClaimsPrincipal(ci);
		}

		public ClaimsPrincipal ValidateToken(string token, bool validateLifetime) {
			var handler = _jwtOptions.SecurityTokenValidators.OfType<JwtSecurityTokenHandler>().First();
			var ovp = _jwtOptions.TokenValidationParameters.Clone();
			ovp.ValidateLifetime = validateLifetime;
			return handler.ValidateToken(token, ovp, out var validatedToken);
		}

		public string GenerateToken(ClaimsPrincipal claimsPrincipal, DateTime expiryDate) {
			var handler = _jwtOptions.SecurityTokenValidators.OfType<JwtSecurityTokenHandler>().First();
			var securityToken = handler.CreateToken(new SecurityTokenDescriptor() {
				Issuer = _jwtOptions.TokenValidationParameters.ValidIssuer,
				Audience = _jwtOptions.TokenValidationParameters.ValidAudience,
				Subject = new ClaimsIdentity(claimsPrincipal.Identity),
				SigningCredentials = new SigningCredentials(_jwtOptions.TokenValidationParameters.IssuerSigningKey, SecurityAlgorithms.RsaSha256Signature),
				Expires = expiryDate
			});
			return handler.WriteToken(securityToken);
		}
	}
}
