using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetToolkit.Db {
	public interface IUserContext {
		string DefaultUserName { get; set; }
		string GetUserName();
	}
}
