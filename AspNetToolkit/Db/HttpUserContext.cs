using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
