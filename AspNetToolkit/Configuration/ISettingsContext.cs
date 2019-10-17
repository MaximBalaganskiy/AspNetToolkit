using AspNetToolkit.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.Configuration {
	public interface ISettingsContext<T> : IDisposable where T: class, ISetting {
		DbSet<T> Settings { get; set; }
	}
}
