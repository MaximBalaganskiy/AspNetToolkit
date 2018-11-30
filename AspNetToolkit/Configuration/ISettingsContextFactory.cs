using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.Configuration {
	public interface ISettingsContextFactory {
		ISettingsContext Create();
	}
}
