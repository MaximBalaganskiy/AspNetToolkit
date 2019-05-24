using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AspNetToolkit.Db {
	public class HttpUserContext : IUserContext {
		private readonly IHttpContextAccessor _httpContextAccessor;

		public HttpUserContext(IHttpContextAccessor httpContextAccessor) {
			_httpContextAccessor = httpContextAccessor;
		}

		public string DefaultUserName { get; set; } = "System";

		public string GetUserName() {
			return _httpContextAccessor?.HttpContext?.User?.GetDisplayName() ?? DefaultUserName;
		}
	}
}
