using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using System.Threading;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore {
	public static class EntityFrameworkHelper {
		/// <summary>
		/// Adds an  entry to the context without adding its' children
		/// </summary>
		/// <param name="context">DbContext</param>
		/// <param name="entry">Entry to add</param>
		/// <returns></returns>
		public static DbContext AddEntry<TEntity>(this DbContext context, TEntity entry) where TEntity : class {
			context.Entry(entry).State = EntityState.Added;
			return context;
		}

		/// <summary>
		/// Adds entries to the context without adding their children
		/// </summary>
		/// <param name="context">DbContext</param>
		/// <param name="entries">Entries to add</param>
		/// <returns></returns>
		public static DbContext AddEntries(this DbContext context, params object[] entries) {
			foreach (var e in entries)
				context.Entry(e).State = EntityState.Added;
			return context;
		}
	}
}
