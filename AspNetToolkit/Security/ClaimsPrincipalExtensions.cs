namespace System.Security.Claims {
	public static class ClaimsPrincipalExtensions {
		public static string GetDisplayName(this ClaimsPrincipal user) {
			var name = user.FindFirst(ClaimTypes.Name) ?? user.FindFirst(ClaimTypes.Email);
			return name?.Value;
		}

		public static string GetEmail(this ClaimsPrincipal user) {
			var name = user.FindFirst(ClaimTypes.Email);
			return name?.Value;
		}

		public static string GetSecurityStamp(this ClaimsPrincipal user) {
			var name = user.FindFirst(AspNetToolkit.Security.ClaimTypes.SecurityStamp);
			return name?.Value;
		}

		public static string GetId(this ClaimsPrincipal user) {
			return user.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}
