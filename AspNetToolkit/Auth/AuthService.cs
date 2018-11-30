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
	public class AuthService<TUser, TRole> : IAuthService
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

		public virtual Task<ClaimsIdentity> CreateClaimsIdentity(TUser u) {
			var ci = new ClaimsIdentity();
			ci.AddClaims(new[]
			{
				new Claim(ClaimTypes.NameIdentifier, u.Id),
				new Claim(ClaimTypes.Email, u.Email),
				new Claim(Security.ClaimTypes.SecurityStamp, u.SecurityStamp)
			});
			return Task.FromResult(ci);
		}

		public async Task<ClaimsPrincipal> CreatePrincipal(string email, string password, string securityStamp) {
			var u = await _userManager.FindByNameAsync(email);
			if (u == null)
				throw new InvalidCredentialsException();
			if (securityStamp != null && securityStamp != u.SecurityStamp)
				throw new InvalidCredentialsException();
			if (password != null && !await _userManager.CheckPasswordAsync(u, password))
				throw new InvalidCredentialsException();
			if (!await _userManager.IsEmailConfirmedAsync(u))
				throw new UnconfirmedEmailException();

			var ci = await CreateClaimsIdentity(u);
			ci.AddClaims(await _userManager.GetClaimsAsync(u));
			var roles = await _userManager.GetRolesAsync(u);
			foreach (var r in roles) {
				ci.AddClaim(new Claim(ci.RoleClaimType, r));
				var role = await _roleManager.FindByNameAsync(r);
				ci.AddClaims(await _roleManager.GetClaimsAsync(role));
			}
			return new ClaimsPrincipal(ci);
		}

		public string GetUserEmailFromToken(string refreshToken, string expiredToken) {
			var handler = _jwtOptions.SecurityTokenValidators.OfType<JwtSecurityTokenHandler>().First();
			var ovp = _jwtOptions.TokenValidationParameters.Clone();
			ovp.ValidateLifetime = false;
			handler.ValidateToken(expiredToken, ovp, out var validatedOldToken);
			var claimsPrincipal = handler.ValidateToken(refreshToken, _jwtOptions.TokenValidationParameters, out var validatedToken);
			return claimsPrincipal.GetEmail();
		}

		public ClaimsPrincipal GetClaimsPrincipalFromToken(string refreshToken, string expiredToken) {
			var handler = _jwtOptions.SecurityTokenValidators.OfType<JwtSecurityTokenHandler>().First();
			var ovp = _jwtOptions.TokenValidationParameters.Clone();
			ovp.ValidateLifetime = false;
			handler.ValidateToken(expiredToken, ovp, out var validatedOldToken);
			return handler.ValidateToken(refreshToken, _jwtOptions.TokenValidationParameters, out var validatedToken);
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
