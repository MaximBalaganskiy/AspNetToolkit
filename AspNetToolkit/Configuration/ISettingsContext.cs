using AspNetToolkit.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.Configuration {
	public interface ISettingsContext : IDisposable {
		DbSet<Setting> Settings { get; set; }
	}
}
